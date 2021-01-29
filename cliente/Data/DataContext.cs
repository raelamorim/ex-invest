using ExInvest.Clientes.Models;
using Microsoft.EntityFrameworkCore;

namespace ExInvest.Clientes.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base (options) 
        {
            this.Database.EnsureCreated();
        }
        public DbSet<Cliente> Clientes { get; set; }
    }
}