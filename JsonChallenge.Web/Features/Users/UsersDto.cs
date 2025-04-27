using AutoMapper;
using JsonChallenge.Domain.Entities.Users;

namespace JsonChallenge.Web.Features.Users
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public int? Idade { get; set; }
        public int? Score { get; set; }
        public bool? Ativo { get; set; }
        public string? Pais { get; set; }
        public EquipeDto? Equipe { get; set; }
        public List<LogDto> Logs { get; set; } = [];
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<UserDto, User>().ReverseMap();
            }
        }
    }

    public class EquipeDto
    {
        public string? Nome { get; set; }
        public bool? Lider { get; set; }
        public List<ProjetoDto> Projetos { get; set; } = [];
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<EquipeDto, Equipe>().ReverseMap();
            }
        }
    }

    public class ProjetoDto
    {
        public string? Nome { get; set; }
        public bool? Concluido { get; set; }
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<ProjetoDto, Projeto>().ReverseMap();
            }
        }
    }

    public class LogDto
    {
        public DateTime? Data { get; set; }
        public string? Acao { get; set; }
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<LogDto, Log>().ReverseMap();
            }
        }
    }
}
