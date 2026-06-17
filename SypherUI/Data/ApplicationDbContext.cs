using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.EntityFrameworkCore.Proxies;
using SypherUI.Models;

namespace SypherUI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<SypherUI.Models.Task> Tasks { get; set; }
        public DbSet<SypherUI.Models.Log> Logs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=database.db");
            optionsBuilder.UseLazyLoadingProxies();
        }
    }
}