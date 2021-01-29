using System.Threading.Tasks;
using AutoMapper;
using ExInvest.Clientes.Repositories;
using ExInvest.Clientes.Dtos;
using ExInvest.Clientes.Models;

namespace ExInvest.Clientes.UseCases
{
    public class PostClienteUseCase : IPostClienteUseCase
    {
        private readonly IClienteRepository _repository;
        private readonly IMapper _mapper;

        public PostClienteUseCase(IClienteRepository repository, IMapper mapper) 
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task Create(PostClienteRequest request)
        {
            var clientesToCreate = _mapper.Map<Cliente>(request);
            await _repository.Create(clientesToCreate);
        }

    }
}