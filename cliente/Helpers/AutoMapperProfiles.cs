using AutoMapper;
using ExInvest.Clientes.Dtos;
using ExInvest.Clientes.Models;

namespace ExInvest.Clientes.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles() 
        {
            CreateMap<Cliente, GetClienteResponse>();
            CreateMap<PostClienteRequest, Cliente>();
        }
    }
}