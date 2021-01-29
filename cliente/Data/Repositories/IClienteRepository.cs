using System.Collections.Generic;
using System.Threading.Tasks;
using ExInvest.Clientes.Models;

namespace ExInvest.Clientes.Repositories
{
    public interface IClienteRepository
    {
        Task<IEnumerable<Cliente>> ListAll();
        Task Create(Cliente cliente);
        Task<Cliente> FindById(int id);
    }
}