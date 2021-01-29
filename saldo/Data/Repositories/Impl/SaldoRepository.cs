using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using ExInvest.Investimentos.Models;

namespace ExInvest.Investimentos.Repositories
{
    public class SaldoRepository : ISaldoRepository
    {
        private const string TableName = "Saldos";

        private readonly IAmazonDynamoDB _amazonDynamoDb;

        public SaldoRepository(IAmazonDynamoDB amazonDynamoDb) 
        {
            _amazonDynamoDb = amazonDynamoDb;
        }

        public async Task Create(Saldo saldo)
        {
            var request = new PutItemRequest
            {
                TableName = TableName,
                Item = new Dictionary<string, AttributeValue>
                {
                    { "Id", new AttributeValue { N = saldo.Id.ToString() }},
                    { "IdCliente", new AttributeValue { N = saldo.IdCliente.ToString() }},
                    { "IdFundo", new AttributeValue { N = saldo.IdFundo.ToString() }},
                    { "DataReferencia", new AttributeValue { S = saldo.DataReferencia.ToString() }},
                    { "ValorBruto", new AttributeValue { N = saldo.ValorBruto.ToString() }},
                    { "ImpostoDevido", new AttributeValue { N = saldo.ImpostoDevido.ToString() }},
                    { "ValorLiquido", new AttributeValue { N = saldo.ValorLiquido.ToString() }},
                }
            };

            await _amazonDynamoDb.PutItemAsync(request);
        }

        public async void CreateTable()
        {

            var request = new ListTablesRequest
            {
                Limit = 10
            };

            var response = await _amazonDynamoDb.ListTablesAsync(request);
            var results = response.TableNames;

            if (!results.Contains(TableName))
            {
                var createRequest = new CreateTableRequest
                {
                    TableName = TableName,
                    AttributeDefinitions = new List<AttributeDefinition>
                    {
                        new AttributeDefinition
                        {
                            AttributeName = "Id",
                            AttributeType = "N"
                        }
                    },
                    KeySchema = new List<KeySchemaElement>
                    {
                        new KeySchemaElement
                        {
                            AttributeName = "Id",
                            KeyType = "HASH"  // Partition Key
                        }
                    },
                    ProvisionedThroughput = new ProvisionedThroughput
                    {
                        ReadCapacityUnits = 2,
                        WriteCapacityUnits = 2
                    }
                };

                await _amazonDynamoDb.CreateTableAsync(createRequest);
            }
        }
    }
}