using System.Collections.Generic;
using System.Threading.Tasks;
using ExInvest.Clientes.Dtos;

namespace ExInvest.Clientes.UseCases
{
    public interface IListClienteUseCase
    {
        Task<IEnumerable<GetClienteResponse>> ListAll();
    }
}
