using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using teste_carteira_virtual.Domain.Entities;

namespace teste_carteira_virtual.Infrastructure.Context
{
    public class DatabaseContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DatabaseContext(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_configuration.GetConnectionString("DefaultConnection"));

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>().HasKey(c => c.Key);
            modelBuilder.Entity<Client>().HasKey(c => c.Key);
            modelBuilder.Entity<Address>().HasKey(a => a.Key);

            base.OnModelCreating(modelBuilder);
        }
    }
}