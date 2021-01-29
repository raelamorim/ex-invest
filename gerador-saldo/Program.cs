using Bogus;
using ExInvest.GeradorSaldo.Models;
using GeradorMassa.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace gerador_saldo
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			List<Cliente> clientes = CarregarClientes();
			var saldos = GeraMassa(clientes);
			var json = JsonConvert.SerializeObject(saldos, Newtonsoft.Json.Formatting.None);
			await File.WriteAllTextAsync("..\\data\\SaldosJson.json", json);
		}

		private static List<Saldo> GeraMassa(List<Cliente> clientes)
		{
			var saldos = new List<Saldo>();

			Parallel.For(0, 100, new ParallelOptions { MaxDegreeOfParallelism = 10 }, x =>
			{
				var faker = new Faker<Saldo>("pt_BR")
					.CustomInstantiator(f => new Saldo())
					.RuleFor(s => s.CPF, f => clientes[f.Random.Int(0,4)].CPF)
					.RuleFor(s => s.IdFundo, f => f.Random.Int(40000, 60000))
					.RuleFor(s => s.ValorBruto, f => Math.Round(f.Random.Double(0.0, 999999.99), 2))
					;

				var saldo = faker.Generate();
				saldos.Add(saldo);
			});

			return saldos;
		}

		private static List<Cliente> CarregarClientes()
		{
			List<Cliente> items;
			using (StreamReader r = new StreamReader("..\\data\\ClientesJson.json"))
			{
				string clientesJson = r.ReadToEnd();
				items = JsonConvert.DeserializeObject<List<Cliente>>(clientesJson);
			}

			return items;
		}
	}
}
