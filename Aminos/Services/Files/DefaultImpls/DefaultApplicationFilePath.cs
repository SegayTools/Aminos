using Aminos.Core.Services.Injections.Attrbutes;
using Aminos.Services.Files;

namespace Aminos.Services.Files.DefaultImpls
{
    [RegisterInjectable(typeof(IApplicationFilePath), serviceLifetime: ServiceLifetime.Singleton)]
    public class DefaultApplicationFilePath : IApplicationFilePath
    {
        public string TempFolderPath { get; set; }

        public string ApplicationDataFolderPath { get; set; }

        public DefaultApplicationFilePath(IConfiguration configuration, ILogger<DefaultApplicationFilePath> logger)
        {
            try
            {
                TempFolderPath = Path.GetTempPath();
                ApplicationDataFolderPath = Path.GetFullPath(configuration.GetSection("ApplicationPersistence")["DataFolderPath"]);

                Directory.CreateDirectory(ApplicationDataFolderPath);

                logger.LogInformation($"Application temp data/files will be saved to: {TempFolderPath}");
                logger.LogInformation($"Application persistence data/files will be saved to: {ApplicationDataFolderPath}");
            }
            catch (Exception e)
            {
                logger.LogError(e, "Init DefaultApplicationFilePath() failed, please check appsetting.json.");
                Environment.Exit(1);
            }
        }
    }
}
