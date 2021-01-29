using System.Threading.Tasks;
using ExInvest.Clientes.Dtos;

namespace ExInvest.Clientes.UseCases
{
    public interface IPostClienteUseCase
    {
         Task Create(PostClienteRequest request);
    }
}