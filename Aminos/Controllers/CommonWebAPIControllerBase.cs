using Aminos.Databases;
using Aminos.Core.Models.General.Tables;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Aminos.Controllers
{
    public class CommonWebAPIControllerBase : ControllerBase
    {
        private readonly AminosDB aminosDB;

        public CommonWebAPIControllerBase(AminosDB aminosDB)
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

        public async ValueTask<UserAccount> GetCurrentRequestUser()
        {
            var userIdStr = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserId").Value;
            if (string.IsNullOrWhiteSpace(userIdStr))
                return default;
            var userId = new Guid(userIdStr);
            return await aminosDB.UserAccounts.FindAsync(userId);
        }
    }
}
