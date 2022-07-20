using Microsoft.EntityFrameworkCore;
using ProjetoParaEstudo.Core.Data;
using ProjetoParaEstudo.Core.Messages;
using ProjetoParaEstudo.Domain.Entities;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ProjetoParaEstudo.Data
{
    public class DataContext : DbContext, IUnitOfWork
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public async Task<bool> Commit()
        {
            var sucesso = await base.SaveChangesAsync() > 0;

            return sucesso;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Ignore<Event>();
            base.OnModelCreating(modelBuilder);
        }
    }
}
