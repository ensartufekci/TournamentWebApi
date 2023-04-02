using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class TournamentContext : DbContext
    {
        public TournamentContext(DbContextOptions<TournamentContext> options)
            : base(options)
        {
        }

        public DbSet<Match> Match { get; set; }
        public DbSet<Team> Team { get; set; }
        
    
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // IConfigurationRoot configuration = new ConfigurationBuilder()
            //     .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            //     .AddJsonFile("appsettings.json")
            //     .Build();
            // optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            optionsBuilder.UseNpgsql("Server=127.0.0.1;Port=5432;Database=Tournament;User Id=postgres;Password=ensar123;");
        }
    }
}