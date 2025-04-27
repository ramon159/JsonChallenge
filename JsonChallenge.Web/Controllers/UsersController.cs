using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JsonChallenge.Domain.Entities.Users;
using JsonChallenge.Web.Data;
using JsonChallenge.Web.Models;
using JsonChallenge.Web.Features.Users;
using AutoMapper;
using System.Text.Json;
using AutoMapper.QueryableExtensions;
using System.Diagnostics;

namespace JsonChallenge.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UsersController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper=mapper;
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostUsers(IFormFile file)
        {
            var sw = Stopwatch.StartNew();

            if (file == null || file.Length == 0)
                return BadRequest("Arquivo inválido");

            using var stream = new StreamReader(file.OpenReadStream());
            var json = await stream.ReadToEndAsync();

            var dto = JsonSerializer.Deserialize<List<UserDto>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var users = _mapper.Map<List<User>>(dto);
            await _context.Usuarios.AddRangeAsync(users);
            await _context.SaveChangesAsync();
            //await _context.Usuarios.BulkInsertOptimizedAsync(users);


            sw.Stop();

            return Ok(new ApiPostReponse() {
                Message = "Arquivo recebido com sucesso",
                Count = users.Count,
                ExecutationTimeMs = sw.ElapsedMilliseconds,
                TimeStamp = DateTime.UtcNow
            });
        }

        // GET: api/Users
        [HttpGet("Superusers")]
        public async Task<IActionResult> GetSuperUsers()
        {
            var sw = Stopwatch.StartNew();

            var result = await _context.Usuarios
                .Where(u => u.Score >= 900 & u.Ativo == true)
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            sw.Stop();

            return Ok(new ApiResponseWithData<List<UserDto>>() { 
                Data = result,
                ExecutationTimeMs = sw.ElapsedMilliseconds,
                TimeStamp = DateTime.UtcNow 
            });
        }

        // GET /top-countries
        [HttpGet("top-countries")]
        public async Task<IActionResult> GetTopCountrys()
        {
            var sw = Stopwatch.StartNew();

            var result = await _context.Usuarios
                .Where(u => u.Score >= 900 & u.Ativo == true)
                .GroupBy(u => u.Pais)
                .Select(g => new { Pais = g.Key, Count = g.Count() })
                .OrderByDescending(x => x.Count)
                .Take(5)
                .ToListAsync();

            sw.Stop();

            return Ok(new ApiResponseWithData<dynamic>()
            {
                Data = result,
                ExecutationTimeMs = sw.ElapsedMilliseconds,
                TimeStamp = DateTime.UtcNow
            });
        }

        [HttpGet("team-insights")]
        public async Task<IActionResult> GetTeamInsights()
        {
            var sw = Stopwatch.StartNew();

            var result = await _context.Usuarios
                .GroupBy(u => u.Equipe.Nome)
                .Select(g => new
                {
                    Team = g.Key,
                    TotalMembers = g.Count(),
                    TotalLeaders = g.Where(u => u.Equipe.Lider == true).Count(),
                    CompletedProjects = g.Sum(u => u.Equipe.Projetos.Count(p => p.Concluido == true)),
                    TotalProjects = g.Sum(u => u.Equipe.Projetos.Count()),
                    ActivePorcentage = ((double)g.Sum(u => u.Equipe.Projetos.Count(p => p.Concluido == true)) / g.Sum(u => u.Equipe.Projetos.Count())) * 100
                })
                .ToListAsync();

            sw.Stop();

            return Ok(new ApiResponseWithData<dynamic>()
            {
                Data = result,
                ExecutationTimeMs = sw.ElapsedMilliseconds,
                TimeStamp = DateTime.UtcNow
            });
        }
        //        "GET /active-users-per-day": {

        [HttpGet("active-users-per-day")]
        public async Task<IActionResult> GetActiveUsersPerDay()
        {
            var sw = Stopwatch.StartNew();

            var result = await _context.Usuarios
                .SelectMany(u => u.Logs)
                .Where(l => l.Acao == "login")
                .GroupBy(l => l.Data)
                .Select(g => new
                {
                    Date = g.Key,
                    Total = g.Count()
                })
                .OrderByDescending(l => l.Date)
                .ToListAsync();

            sw.Stop();

            return Ok(new ApiResponseWithData<dynamic>()
            {
                Data = result,
                ExecutationTimeMs = sw.ElapsedMilliseconds,
                TimeStamp = DateTime.UtcNow
            });
        }

    }
}
