using AutoMapper;
using JsonChallenge.Domain.Entities;
using JsonChallenge.Domain.Entities.Users;

namespace JsonChallenge.Web.Features.Users
{
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string? Nome { get; set; }
        public int? Idade { get; set; }
        public int? Score { get; set; }
        public bool? Ativo { get; set; }
        public string? Pais { get; set; }
        public EquipeViewModel? Equipe { get; set; }
        public List<LogViewModel> Logs { get; set; } = [];
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<UserViewModel, User>().ReverseMap();
                //CreateProjetoion<UserViewModel, User>();

            }
        }
    }

    public class EquipeViewModel
    {
        public string? Nome { get; set; }
        public bool? Lider { get; set; }
        public List<ProjetoViewModel> Projetos { get; set; } = [];
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<EquipeViewModel, Equipe>().ReverseMap();
            }
        }
    }

    public class ProjetoViewModel
    {
        public string? Nome { get; set; }
        public bool? Concluido { get; set; }
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<ProjetoViewModel, Projeto>().ReverseMap();
            }
        }
    }

    public class LogViewModel
    {
        public DateTime? Data { get; set; }
        public string? Acao { get; set; }
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<LogViewModel, Log>().ReverseMap();
            }
        }
    }
}

