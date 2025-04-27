namespace JsonChallenge.Domain.Entities.Users;
public class User : BaseEntity
{
    public string? Nome { get; set; }
    public int? Idade { get; set; }
    public int? Score { get; set; }
    public bool? Ativo { get; set; }
    public string? Pais { get; set; }
    public Equipe? Equipe { get; set; }
    public List<Log> Logs { get; set; } = [];
}

public class Equipe : BaseEntity
{
    public string? Nome { get; set; }
    public bool? Lider { get; set; }
    public List<Projeto> Projetos { get; set; } = [];
}

public class Projeto : BaseEntity
{
    public string? Nome { get; set; }
    public bool? Concluido { get; set; }
}

public class Log : BaseEntity
{
    public DateTime? Data { get; set; }
    public string? Acao { get; set; }
}

