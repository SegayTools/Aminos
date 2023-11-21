using Microsoft.AspNetCore.Mvc.Filters;
using System.IO.Compression;

namespace Aminos.Controllers.Title
{
	public class TitleZlibCompressionAttribute : ActionFilterAttribute
	{
		public override Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
		{
			var stream = context.HttpContext.Response.Body;
			var zlibStream = new ZLibStream(stream, CompressionMode.Compress);
			context.HttpContext.Response.Body = zlibStream;
			context.HttpContext.Response.Headers.ContentEncoding = "deflat";

			return base.OnResultExecutionAsync(context, next);
		}
	}
}
