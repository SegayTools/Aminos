using Aminos.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Aminos.Controllers.Title
{
	public class TitleControllerBase : ControllerBase
	{
		public static JsonSerializerOptions JsonSerializeOption { get; } = new JsonSerializerOptions()
		{
			WriteIndented = false,
			IncludeFields = true,
			IgnoreReadOnlyProperties = true,
		};

		public IActionResult Json<T>(T obj) => new JsonResult(obj, JsonSerializeOption);
	}
}
