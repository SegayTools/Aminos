namespace Aminos.Utils.MethodExtensions
{
	public static class LoggerMethodExtensions
	{
		public static Guid LogErrorAndGetTrackId<T>(this ILogger<T> logger, Exception exception, string message)
		{
			var trackId = new Guid();
			logger.LogError(exception, $"{message}, trackId:{trackId}");
			return trackId;
		}
	}
}
