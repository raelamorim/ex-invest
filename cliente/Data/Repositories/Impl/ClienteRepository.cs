using System.Collections.Generic;
using System.Threading.Tasks;
using ExInvest.Clientes.Data;
using ExInvest.Clientes.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ExInvest.Clientes.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly DataContext _context;
        public ClienteRepository(DataContext context)
        {
            this._context = context;
        }

        public async Task Create(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task<Cliente> FindById(int id) => await _context.Clientes
            .FirstOrDefaultAsync(c => c.Id == id);
        public async Task<IEnumerable<Cliente>> ListAll() => await _context.Clientes
            .OrderBy(c => c.Id).ToListAsync();
    }
}