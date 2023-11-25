using Aminos.Authorization;
using Aminos.Databases;
using Aminos.Models.General.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text;

namespace Aminos.Controllers.Title
{
    public class VerifyKeychipAttribute : ActionFilterAttribute
    {
		private readonly AminosDB aminosDB;
		private readonly IKeychipSafeHandleAuthorization authorize;

		public VerifyKeychipAttribute(AminosDB aminosDB, IKeychipSafeHandleAuthorization authorize)
		{
			this.aminosDB = aminosDB;
			this.authorize = authorize;
		}

		public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var serviceProvider = context.HttpContext.RequestServices;

            bool result;
            if (!context.RouteData.Values.TryGetValue("safeHandle", out var strObj) || strObj is not string safeHandle || string.IsNullOrWhiteSpace(safeHandle))
                result = false;
            else
            {
                result = await authorize.AuthorizeVerfiy(safeHandle);
                if (!result)
                {
                    //get keychip and verify them again
                    //GetGameSettingApi#A63E01A1111
                    string userAgent = context.HttpContext.Request.Headers.UserAgent;

                    var split = userAgent.Split("#");
                    var keychipPart = split.ElementAtOrDefault(1);

                    if (!string.IsNullOrWhiteSpace(keychipPart))
                    {
                        if (await aminosDB.Keychips.FindAsync(keychipPart) is Keychip keychip && keychip.Enable)
                            result = true;
                    }
                }
            }

            if (result)
                await base.OnActionExecutionAsync(context, next);
            else
            {
                context.HttpContext.Response.StatusCode = 403;
                context.HttpContext.Response.ContentType = "text/plain";
                await context.HttpContext.Response.Body.WriteAsync(Encoding.UTF8.GetBytes("Invalid safeHandle, you have to poweron your machine."));
            }
        }
    }
}
