using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ExInvest.Clientes.Repositories;
using ExInvest.Clientes.Dtos;

namespace ExInvest.Clientes.UseCases
{
    public class GetClienteUseCase : IGetClienteUseCase
    {
        private readonly IClienteRepository _repository;
        private readonly IMapper _mapper;

        public GetClienteUseCase(IClienteRepository repository, IMapper mapper) 
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<GetClienteResponse> FindById(int id)
        {
            var cliente = await _repository.FindById(id);
            var clienteToReturn = _mapper.Map<GetClienteResponse>(cliente);
            return await Task.FromResult(clienteToReturn);
        }
    }
}
