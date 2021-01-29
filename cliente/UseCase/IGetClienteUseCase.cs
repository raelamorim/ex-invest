using System.Collections.Generic;
using System.Threading.Tasks;
using ExInvest.Clientes.Dtos;

namespace ExInvest.Clientes.UseCases
{
    public interface IGetClienteUseCase
    {
        Task<GetClienteResponse> FindById(int id);
    }
}
