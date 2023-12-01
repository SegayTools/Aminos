using Aminos.Databases;
using Aminos.Core.Models.General.Tables;
using Aminos.Core.Services.Injections.Attrbutes;
using Aminos.Utils;
using System.Collections.Concurrent;

namespace Aminos.Authorization.DefaultImpl
{
	[RegisterInjectable(typeof(IKeychipSafeHandleAuthorization))]
	public class DefaultKeychipSafeHandleAuthorization : IKeychipSafeHandleAuthorization
	{
		public static TimeSpan CacheExpiredTime { get; } = TimeSpan.FromMinutes(3);

		private record AuthorizationCached(DateTime ExpiredDate);

		private static ConcurrentDictionary<string, AuthorizationCached> cached = new();
		private static ConcurrentBag<string> bad = new();

		private readonly AminosDB aminosDB;
		private readonly ILogger<DefaultKeychipSafeHandleAuthorization> logger;

		public DefaultKeychipSafeHandleAuthorization(AminosDB aminosDB, ILogger<DefaultKeychipSafeHandleAuthorization> logger)
		{
			this.aminosDB = aminosDB;
			this.logger = logger;
		}

		public async ValueTask<bool> AuthorizeVerfiy(string safeHandle)
		{
			var nowTime = DateTime.Now;

			if (bad.Contains(safeHandle))
				return false;

			async ValueTask<bool> Authorize()
			{
				bool needUpdateCached;
				if (cached.TryGetValue(safeHandle, out var authorizationCached))
				{
					if (nowTime > authorizationCached.ExpiredDate)
						needUpdateCached = true;
					else
						return true;
				}
				else
					needUpdateCached = true;

				if (needUpdateCached)
				{
					if (!TryConvertSafeHandleToSerial(safeHandle, out var keychipStr))
						return false;

					var keychip = await aminosDB.Keychips.FindAsync(keychipStr);
					if (keychip is null)
						return false;

					nowTime = DateTime.Now;
					keychip.LastAccessDate = nowTime;
					await aminosDB.SaveChangesAsync();

					var authCache = new AuthorizationCached(nowTime + CacheExpiredTime);
					cached[safeHandle] = authCache;
				}

				return true;
			}

			var result = await Authorize();
			if (!result)
				bad.Add(safeHandle);
			return result;
		}

		public ValueTask ExpiredAll()
		{
			cached.Clear();
			bad.Clear();
			return ValueTask.CompletedTask;
		}

		private string ConvertSerialToSafeHandle(string keychip) => "sh_" + SimpleCryptography.Encrypt(keychip).Replace("=", "_");
		private bool TryConvertSafeHandleToSerial(string safeHandle, out string keychip)
		{
			keychip = string.Empty;
			if (safeHandle.StartsWith("sh_"))
				safeHandle = safeHandle.Substring(3);
			else
				return false;
			keychip = SimpleCryptography.Decrypt(safeHandle.Replace("_", "="));
			return keychip.All(char.IsLetterOrDigit);
		}

		public async ValueTask<string> GenerateSafeHandle(Keychip keychip)
		{
			var safeHandle = ConvertSerialToSafeHandle(keychip.Id);

			if (!cached.ContainsKey(safeHandle))
			{
				var authCache = new AuthorizationCached(DateTime.Now + CacheExpiredTime);
				cached[safeHandle] = authCache;

				aminosDB.Attach(keychip);
				keychip.LastAccessDate = DateTime.Now;
				await aminosDB.SaveChangesAsync();
			}

			return safeHandle;
		}
	}
}
