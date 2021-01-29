using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using ExInvest.Investimentos.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using ExInvest.Investimentos.Models;
using System.Text;
using Newtonsoft.Json;
using Serilog;

namespace ExInvest.Investimentos.Handlers
{
    public class SaldoHandler : IHostedService
    {
         private readonly Serilog.ILogger _logger;
         private readonly ISaldoRepository _repository;
        public SaldoHandler(ILogger<SaldoHandler> logger, ISaldoRepository repository)
        {
            _logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
            _repository = repository;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.Information("Criando o database");
            _repository.CreateTable();
            _logger.Information("Testando o recebimento de mensagens com Kafka");

            var conf = new ConsumerConfig
            {
                GroupId = "test-consumer-group",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using (var c = new ConsumerBuilder<Ignore, string>(conf).Build())
            {
                c.Subscribe("fila_saldos");
                var cts = new CancellationTokenSource();

                try
                {
                    while (true)
                    {
                        var cr = c.Consume(cts.Token).Message.Value;
                        var saldo = JsonConvert.DeserializeObject<Saldo>(cr);
                        _logger.Information($"Mensagem Recebida ${cr}");
                        await _repository.Create(saldo);
                    }
                }
                catch (OperationCanceledException)
                {
                    c.Close();
                }
            }


        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}