using FinanceApp.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceApp.Api.Contexto
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }

        /// <summary>
        /// Configuração para criar o Model
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relecionamentos
            modelBuilder.Entity<Usuario>().HasMany(u => u.Receitas).WithOne(u => u.Usuario);
            modelBuilder.Entity<Usuario>().HasMany(u => u.Despesas).WithOne(u => u.Usuario);

            // Primary keys
            modelBuilder.Entity<Usuario>().HasKey(u => u.Id);
            modelBuilder.Entity<Despesa>().HasKey(d => d.Id);
            modelBuilder.Entity<Receita>().HasKey(r => r.Id);
            modelBuilder.Entity<Comprovante>().HasKey(c => c.Id);
            modelBuilder.Entity<Log>().HasKey(c => c.Id);

        }
        
        public DbSet<Usuario> Usuario { get; set; }
        
        public DbSet<Despesa> Despesa { get; set; }

        public DbSet<Receita> Receita { get; set; }

        public DbSet<Log> Log { get; set; }
    }
}
