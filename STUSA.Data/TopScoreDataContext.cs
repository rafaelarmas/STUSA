using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace STUSA.Data
{
    public class TopScoreDataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public TopScoreDataContext(IConfiguration configuration)
        {
            Configuration = configuration;
            Database.OpenConnection();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(Configuration.GetConnectionString("STUSADatabase"));
        }

        public override void Dispose()
        {
            Database.CloseConnection();
            base.Dispose();
        }

        public DbSet<TopScoreWord> TopScoreWords { get; set; }
    }
}
