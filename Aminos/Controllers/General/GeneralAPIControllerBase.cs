using Aminos.Databases;
using Aminos.Models.General.Tables;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Aminos.Controllers.General
{
	public class GeneralAPIControllerBase : ControllerBase
	{
		private readonly AminosDB aminosDB;

		public GeneralAPIControllerBase(AminosDB aminosDB)
		{
			this.aminosDB = aminosDB;
		}

		public static JsonSerializerOptions JsonSerializeOption { get; } = new JsonSerializerOptions()
		{
			WriteIndented = false,
			IncludeFields = true,
			IgnoreReadOnlyProperties = true,
		};

		public IActionResult Json<T>(T obj)
		{
			return new JsonResult(obj, JsonSerializeOption);
		}

		public async ValueTask<UserAccount> GetUser()
		{
			var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId");
			if (userId is null)
				return default;

			return await aminosDB.UserAccounts.FindAsync(userId);
        }
	}
}
