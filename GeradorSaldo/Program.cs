
using Bogus;
using Confluent.Kafka;
using ExInvest.GeradorSaldo.Models;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeradorSaldo
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var logger = new LoggerConfiguration()
				.WriteTo.Console()
				.CreateLogger();
			logger.Information("Testando o envio de mensagens com Kafka");

			string bootstrapServers = "localhost:9092";
			string nomeTopic = "fila_saldos";

			var faker = new Faker<Saldo>("pt_BR")
				.CustomInstantiator(f => new Saldo())
				.RuleFor(s => s.Id, f => f.UniqueIndex)
				.RuleFor(c => c.IdCliente, f => f.Random.Int(1, 999999999))
				.RuleFor(c => c.ValorBruto, f => f.Random.Decimal(0.0m, 9999999999.99m));

			List<Saldo> saldos = faker.Generate(10000);

			await produzirMensagem(saldos, logger, bootstrapServers, nomeTopic);
		}
		private static async Task produzirMensagem(List<Saldo> saldos, Serilog.Core.Logger logger, string bootstrapServers, string nomeTopic)
		{
			try
			{
				var config = new ProducerConfig
				{
					BootstrapServers = bootstrapServers
				};

				using (var producer = new ProducerBuilder<Null, string>(config).Build())
				{
					foreach (var saldo in saldos)
					{
						var json = JsonConvert.SerializeObject(saldo, Formatting.None);
						var result = await producer.ProduceAsync(
							nomeTopic,
							new Message<Null, string>
							{ Value = json });

						logger.Information(
							$"Mensagem: {json} | " +
							$"Status: { result.Status.ToString()}");
					}
				}

				logger.Information("Concluído o envio de mensagens");
			}
			catch (Exception ex)
			{
				logger.Error($"Exceção: {ex.GetType().FullName} | " +
							 $"Mensagem: {ex.Message}");
			}
		}

	}
}
