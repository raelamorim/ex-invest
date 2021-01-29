using Amazon;
using Amazon.DynamoDBv2;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using ExInvest.Investimentos.Handlers;
using ExInvest.Investimentos.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Collections.Generic;

namespace ExInvest.Investimentos
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureDevelopmentServices(IServiceCollection services)
		{
			var logger = new LoggerConfiguration()
				.WriteTo.Console()
				.CreateLogger();
			logger.Information("Passei aqui");

			var dynamoDbConfig = Configuration.GetSection("DynamoDb");
			var awsConfig = Configuration.GetSection("AWS");
			var runLocalDynamoDb = dynamoDbConfig.GetValue<bool>("LocalMode");
			var awsCredentials = new BasicAWSCredentials(awsConfig.GetValue<string>("ClientId"), awsConfig.GetValue<string>("ClientSecret"));
			var region = RegionEndpoint.GetBySystemName(awsConfig.GetValue<string>("Region"));

			if (runLocalDynamoDb)
			{
				services.AddSingleton<IAmazonDynamoDB>(sp =>
				{
					var clientConfig = new AmazonDynamoDBConfig
					{
						ServiceURL = dynamoDbConfig.GetValue<string>("LocalServiceUrl"),
						RegionEndpoint = region
					};
					return new AmazonDynamoDBClient(awsCredentials, clientConfig);
				});
				logger.Information("Registrei esse");
			}
			else
			{
				var awsOptions = new AWSOptions();
				awsOptions.Credentials = awsCredentials;
				awsOptions.Region = region;
				services.AddAWSService<IAmazonDynamoDB>();
			}

			addServices(services);
		}

		private void addServices(IServiceCollection services)
		{
			services.AddControllers().AddNewtonsoftJson(opt =>
			{
				opt.SerializerSettings.ReferenceLoopHandling =
					Newtonsoft.Json.ReferenceLoopHandling.Ignore;
			});
			services.AddScoped<ISaldoRepository, SaldoRepository>();
			services.AddHostedService<SaldoHandler>();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			//	if (env.IsDevelopment())
			//	{
			//		app.UseDeveloperExceptionPage();
			//	}

			//	app.UseHttpsRedirection();
			//	app.UseRouting();
		}
	}
}
