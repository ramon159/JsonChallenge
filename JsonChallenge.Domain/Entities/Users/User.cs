
public class User
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Age { get; set; }
    public string Score { get; set; }
    public string Active { get; set; }
    public string Country { get; set; }
    public Team Team { get; set; }
    public Log[] Logs { get; set; }
}

public class Team
{
    public string Name { get; set; }
    public string Leader { get; set; }
    public Project[] Projects { get; set; }
}

public class Project
{
    public string Name { get; set; }
    public string Completed { get; set; }
}

public class Log
{
    public string Date { get; set; }
    public string Action { get; set; }
}

