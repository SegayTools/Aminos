using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading.RateLimiting;
using Aminos.Authorization;
using Aminos.Core.Services.Injections;
using Aminos.Core.Utils.MethodExtensions;
using Aminos.Databases;
using Aminos.Databases.Title.SDEZ;
using Aminos.Services.AimeDB;
using Aminos.Utils;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace Aminos;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        using var p12FileStream =
            typeof(Program).Assembly.GetManifestResourceStream("Aminos.Resources.Billing.server.p12");
        var p12FileBytes = await p12FileStream.ToByteArrayAsync();

        builder.WebHost.ConfigureKestrel(option =>
        {
            option.ListenAnyIP(8443, config =>
            {
                var password = "aquaserver";

                var certificate = new X509Certificate2(p12FileBytes, password);
                config.UseHttps(certificate, conf => { conf.SslProtocols = SslProtocols.Tls11; });
                config.Protocols = HttpProtocols.Http1;
            });

            option.ListenAnyIP(443, config => { config.UseHttps(); });

            option.ListenAnyIP(80, config => { });
        });

        // Add services to the container.
        builder.Services.AddControllers();

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy(AuthRolePolicyString.OwnerRole,
                policy => policy.RequireRole(
                    AuthRolePolicyString.OwnerRole));
            options.AddPolicy(AuthRolePolicyString.AdminRole,
                policy => policy
                    .RequireRole(
                        AuthRolePolicyString.OwnerRole,
                        AuthRolePolicyString.AdminRole));
            options.AddPolicy(AuthRolePolicyString.UserRole,
                policy => policy
                    .RequireRole(
                        AuthRolePolicyString.UserRole,
                        AuthRolePolicyString.AdminRole,
                        AuthRolePolicyString.OwnerRole));
        });

        builder.Services.AddW3CLogging(o => { o.LoggingFields = W3CLoggingFields.All; });
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

        builder.Services.AddRateLimiter(o =>
        {
            o.AddFixedWindowLimiter("AntiBruteForce", o2 =>
            {
                o2.PermitLimit = 5;
                o2.Window = TimeSpan.FromSeconds(30);
                o2.QueueProcessingOrder = QueueProcessingOrder.NewestFirst;
                o2.QueueLimit = 3;
            });
        });

        builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options => { options.ClaimsIssuer = "AminosIssuer"; });

        builder.Services.AddDbContext<AminosDB>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
        builder.Services.AddDbContext<MaimaiDXDB>(options =>
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddLogging(options => { options.AddSeq("http://localhost:5341/", "wSkYZUo2uMdeChLfusYI"); });

        builder.Services.AddInjectsByAttributes(typeof(Program).Assembly);
        builder.Services.AddHostedService<AimeDBService>();
        builder.Services.AddRequestDecompression(option =>
        {
            var defaultDeflateProvider = option.DecompressionProviders["deflate"];
            option.DecompressionProviders["deflate"] = new ZlibOrGzipDecompressionProvider(defaultDeflateProvider);
        });

        var app = builder.Build();

        await CheckDBMigrations<AminosDB>(app.Services);
        await CheckDBMigrations<MaimaiDXDB>(app.Services);

        app.UseAuthentication();
        app.UseRateLimiter();
        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(@"F:\authAssets\"),
            RequestPath = "/authAssets",
            OnPrepareResponse = ctx =>
            {
                /*
                if (!ctx.Context.User.IsInRole(AuthRolePolicyString.UserRole))
                {
                    ctx.Context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    ctx.Context.Response.ContentLength = 0;
                    ctx.Context.Response.Body = Stream.Null;
                    ctx.Context.Response.Headers.Add("Cache-Control", "no-store");
                }
                */
            }
        });
        app.UseAuthorization();
        app.UseRequestDecompression();
        app.UseW3CLogging();
        app.UseHttpLogging();

        app.MapControllers();

        await app.RunAsync();
    }

    private static async ValueTask CheckDBMigrations<T>(IServiceProvider provider) where T : DbContext
    {
        var loggerFactory = provider.GetService<ILoggerFactory>();
        var logger = loggerFactory.CreateLogger(typeof(T));

        using var scope = provider.CreateScope();
        using var db = scope.ServiceProvider.GetRequiredService<T>();

        var applieds = (await db.Database.GetAppliedMigrationsAsync()).ToArray();
        var pendings = (await db.Database.GetPendingMigrationsAsync()).ToArray();

        logger.LogInformation("Applied migrations:");
        foreach (var item in applieds)
            logger.LogInformation($" * {item}");
        logger.LogInformation("Pending migrations:");
        foreach (var item in pendings)
            logger.LogInformation($" * {item}");

        await db.Database.MigrateAsync();
    }
}