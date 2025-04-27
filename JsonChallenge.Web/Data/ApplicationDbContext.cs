using JsonChallenge.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Extensions;

namespace JsonChallenge.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public required DbSet<User> Usuarios { get; set; }
        public required DbSet<Equipe> Times { get; set; }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            //EntityFrameworkManager.ContextFactory = context => new ApplicationDbContext(options);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseInMemoryDatabase("memory");
        }
    }
}
