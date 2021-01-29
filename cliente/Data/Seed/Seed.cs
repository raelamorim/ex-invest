using System.Linq;
using ExInvest.Clientes.Data;
using Bogus;
using Bogus.Extensions.Brazil;
using ExInvest.Clientes.Models;
using ExInvest.Clientes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EFCore.BulkExtensions;

namespace cliente.Data.Seed
{
    public class Seed
    {
        public static void SeedClientes(DataContext context)
        {
            if (!context.Clientes.Any())
            {  
                int qtdRegistrosSeed = Int32.Parse(Startup.StaticConfig
                    .GetSection("MySettings").GetSection("QtdRegistrosSeed").Value);

                var clientes = new List<Cliente>();

                for(int x = 0; x <= qtdRegistrosSeed; x++) 
                {
                    var faker = new Faker<Cliente>("pt_BR")
                        // .CustomInstantiator(f => new Cliente() { Id = x })
                        .CustomInstantiator(f => new Cliente() )
                        .RuleFor(c => c.Nome, f => f.Name.FullName())
                        .RuleFor(c => c.CPF, f => f.Person.Cpf())
                        .RuleFor(c => c.Agencia, f => f.Random.Int(1, 9999))
                        .RuleFor(c => c.Conta, f => f.Random.Int(1, 99999))
                        .RuleFor(c => c.DAC, f => f.Random.Int(1, 9))
                        ;
                    var cliente = faker.Generate();
                    clientes.Add(cliente);
                }

                var bulkConfig = new BulkConfig { 
                    PreserveInsertOrder = true, SetOutputIdentity = true, BatchSize = 4000 };
                context.BulkInsert(clientes);
                context.SaveChanges();
            }
        }
    }
}