using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;

namespace AuditTrailWebApi
{
    public class Program
    {
		public static async Task Main(string[] args)
		{
			ConfigureLog();

			try
			{
				var host = CreateHostBuilder(args).Build();

				Log.Information("Starting up");

				using (var scope = host.Services.CreateScope())
				{
					var services = scope.ServiceProvider;

					
				}

				await host.RunAsync();
                Log.Information("Application Started Successfully");

			}
			catch (Exception ex)
			{
				Log.Fatal(ex, "Application start-up failed");
			}
			finally
			{
				Log.Information("Closing up");
				Log.CloseAndFlush();
            }
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.UseSerilog()
				.ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });

		private static void ConfigureLog()
		{
			var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
			var configuration = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
								.AddJsonFile(
									$"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
									optional: true)
								.Build();

			var loggerConfiguration = new LoggerConfiguration()
									   .Enrich.FromLogContext()
									   .Enrich.WithExceptionDetails()
									   .Enrich.WithProperty("Company", "Leatherback")
									   .Enrich.WithProperty("Application", "AuditTrailApi")
									   .Enrich.WithProperty("Environment", environment)
									   .WriteTo.Console(outputTemplate: "[{Timestamp:yyy-MM-dd HH:mm:ss.fff zzz} {Level}] {Message} ({SourceContext:l}){NewLine}{Exception}");

			loggerConfiguration = loggerConfiguration.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
			{
				AutoRegisterTemplate = true,
				IndexFormat = $"leatherback-audittrail-api-{DateTime.Now.ToUniversalTime():yyyy-MM}",
				CustomFormatter = new ExceptionAsObjectJsonFormatter(renderMessage: true),
				MinimumLogEventLevel = LogEventLevel.Information,
				AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv7
			});

			Log.Logger = loggerConfiguration.CreateLogger();
		}
	}
}
