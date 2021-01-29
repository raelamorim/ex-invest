using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Bogus;
using Bogus.Extensions.Brazil;
using CsvHelper;
using GeradorMassa.Models;
using Newtonsoft.Json;

namespace GeradorMassa
{
	/* 
        compilar conforme abaixo:
        dotnet build .\GeradorMassa.csproj /p:StartupObject=GeradorMassa.GeradorCliente
    */
	public class GeradorCliente
	{
		public static async Task Main(string[] args)
		{
			var clientes = new List<Cliente>();


			Parallel.For(0, 5, new ParallelOptions { MaxDegreeOfParallelism = 10 }, x =>
		   {
			   var faker = new Faker<Cliente>("pt_BR")
				   .CustomInstantiator(f => new Cliente())
				   .RuleFor(c => c.Nome, f => f.Name.FullName())
				   .RuleFor(c => c.CPF, f => f.Person.Cpf())
				   .RuleFor(c => c.DataNascimento, f => f.Person.DateOfBirth.ToShortDateString())
				   .RuleFor(c => c.Email, f => f.Person.Email)
				   ;
			   var cliente = faker.Generate();

			   AddConta(cliente);
			   AddTelefone(cliente);
			   AddEndereco(cliente);

			   clientes.Add(cliente);
		   });
			var json = JsonConvert.SerializeObject(clientes, Newtonsoft.Json.Formatting.None);
			await File.WriteAllTextAsync("..\\data\\ClientesJson.json", json);
		}

		private static void AddConta(Cliente cliente)
		{
			var contas = new List<Conta>();
			Random rnd = new Random();
			for (int i = 0; i <= rnd.Next(1, 4); i++)
			{
				contas.Add(new Faker<Conta>("pt_BR")
					.CustomInstantiator(f => new Conta())
					.RuleFor(c => c.Agencia, f => f.Random.Int(1, 9999))
					.RuleFor(c => c.NumeroConta, f => f.Random.Int(1, 99999))
					.RuleFor(c => c.DAC, f => f.Random.Int(1, 9))
				);
			}
			cliente.Contas = contas;
		}

		private static void AddEndereco(Cliente cliente)
		{
			var enderecos = new List<Endereco>();
			Random rnd = new Random();
			for (int i = 0; i <= rnd.Next(1, 3); i++)
			{
				enderecos.Add(new Faker<Endereco>("pt_BR")
					.CustomInstantiator(f => new Endereco())
					   .RuleFor(c => c.EnderecoCompleto, f => f.Person.Address.Street)
					   .RuleFor(c => c.Complemento, f => f.Person.Address.Suite)
					   .RuleFor(c => c.Cidade, f => f.Person.Address.City)
					   .RuleFor(c => c.Estado, f => f.Person.Address.State)
					   .RuleFor(c => c.CEP, f => f.Person.Address.ZipCode)
				);
			}
			cliente.Enderecos = enderecos;
		}

		private static void AddTelefone(Cliente cliente)
		{
			var telefones = new List<Telefone>();
			Random rnd = new Random();
			for (int i = 0; i <= rnd.Next(0, 5); i++)
			{
				telefones.Add(new Faker<Telefone>("pt_BR")
					.CustomInstantiator(f => new Telefone())
					.RuleFor(t => t.DDD, f => f.Random.Int(10, 60))
					.RuleFor(t => t.Numero, f => f.Random.Int(911111111, 999999999))
				);
			}
			cliente.Telefones = telefones;
		}
	}
}
