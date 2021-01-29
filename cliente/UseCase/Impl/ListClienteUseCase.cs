using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using ExInvest.Clientes.Repositories;
using ExInvest.Clientes.Dtos;

namespace ExInvest.Clientes.UseCases
{
    public class ListClienteUseCase : IListClienteUseCase
    {
        private readonly IClienteRepository _repository;
        private readonly IMapper _mapper;

        public ListClienteUseCase(IClienteRepository repository, IMapper mapper) 
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<IEnumerable<GetClienteResponse>> ListAll()
        {
            var clientes = await _repository.ListAll();
            var clientesToReturn = _mapper.Map<IEnumerable<GetClienteResponse>>(clientes);
            return await Task.FromResult(clientesToReturn);
        }
    }
}
