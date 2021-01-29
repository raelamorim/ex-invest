using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ExInvest.Clientes.UseCases;
using ExInvest.Clientes.Dtos;

namespace ExInvest.Clientes.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IGetClienteUseCase _getCliente;
        private readonly IListClienteUseCase _listCliente;
        private readonly IPostClienteUseCase _postCliente;

        public ClientesController(IGetClienteUseCase getCliente
                                , IListClienteUseCase listCliente
                                , IPostClienteUseCase postCliente)
        {
            _getCliente = getCliente;
            _listCliente = listCliente;
            _postCliente = postCliente;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCliente(int id)
        {
            var clienteToReturn = await _getCliente.FindById(id);
            return Ok(clienteToReturn);
        }

        [HttpGet]
        public async Task<IActionResult> GetClientes()
        {
            var clientesToReturn = await _listCliente.ListAll();
            return Ok(clientesToReturn);
        }
        
        [HttpPost]
        public async Task<IActionResult> PostCliente(PostClienteRequest request)
        {
            await _postCliente.Create(request);
            return Created("clientes/", request);
        }
    }
}