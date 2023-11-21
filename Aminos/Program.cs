using Aminos.Databases;
using Aminos.Databases.Title.SDEZ;
using Aminos.Handlers.AllNet;
using Aminos.Handlers.AllNet.Default;
using Aminos.Kernels.Injections;
using Aminos.Services.AimeDB;
using Aminos.Utils;
using Aminos.Utils.MethodExtensions;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.Net;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Aminos
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			using var p12FileStream = typeof(Program).Assembly.GetManifestResourceStream("Aminos.Resources.Billing.server.p12");
			var p12FileBytes = await p12FileStream.ToByteArrayAsync();

			builder.WebHost.ConfigureKestrel(option =>
			{
				option.ListenAnyIP(8443, config =>
				{
					var password = "aquaserver";

					// ���� .p12 ֤��
					var certificate = new X509Certificate2(p12FileBytes, password);
					config.UseHttps(certificate, conf =>
					{
						conf.SslProtocols = SslProtocols.Tls11;
					});
					config.Protocols = HttpProtocols.Http1;
				});

				option.ListenAnyIP(443, config =>
				{
					config.UseHttps();
				});

				option.ListenAnyIP(80, config =>
				{

				});
			});

			var fileProviderRootPath = Path.Combine(Path.GetTempPath(), "AnimosTemps");
			Directory.CreateDirectory(fileProviderRootPath);
			builder.Services.AddScoped<IFileProvider>((x) => new PhysicalFileProvider(fileProviderRootPath));

			// Add services to the container.
			builder.Services.AddControllers();
			builder.Services.AddSingleton<IAllNetHandler, DefaultAllNetHandler>();
			builder.Services.AddLogging(logging =>
			{
				logging.SetMinimumLevel(LogLevel.Trace);
			});
			builder.Services.AddW3CLogging(o =>
			{
				o.LoggingFields = W3CLoggingFields.All;
			});
			builder.Services.AddHttpLogging(o =>
			{
				o.LoggingFields =
					HttpLoggingFields.RequestMethod |
					HttpLoggingFields.RequestBody |
					HttpLoggingFields.RequestHeaders |
					HttpLoggingFields.RequestProtocol |
					HttpLoggingFields.RequestScheme |
					HttpLoggingFields.RequestPath |
					HttpLoggingFields.RequestQuery;

				o.LoggingFields = o.LoggingFields |
					HttpLoggingFields.ResponseHeaders |
					HttpLoggingFields.ResponseStatusCode |
					HttpLoggingFields.ResponseBody;
			});

			builder.Services.AddDbContext<AminosDB>(options =>
				options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")),
			ServiceLifetime.Scoped);
			builder.Services.AddDbContext<MaimaiDXDB>(options =>
				options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")),
			ServiceLifetime.Scoped);

			builder.Services.AddInjectsByAttributes(typeof(Program).Assembly);
			builder.Services.AddHostedService<AimeDBService>();
			var app = builder.Build();

			await CheckAndMigrateDatabase(app);

			//app.UseHttpsRedirection();
			app.UseAuthorization();
			app.UseW3CLogging();
			app.UseHttpLogging();
			app.MapControllers();

			await app.RunAsync();
		}

		private static Task CheckAndMigrateDatabase(WebApplication app)
		{
			return Task.CompletedTask;
		}
	}
}
