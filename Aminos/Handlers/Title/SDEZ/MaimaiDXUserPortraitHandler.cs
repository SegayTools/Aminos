using Aminos.Databases.Title.SDEZ;
using Aminos.Kernels.Injections.Attrbutes;
using Aminos.Models.Title.SDEZ;
using Aminos.Models.Title.SDEZ.Requests;
using Aminos.Models.Title.SDEZ.Responses;
using Aminos.Utils.MethodExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Buffers;
using System.Text;
using System.Text.Json;

namespace Aminos.Handlers.Title.SDEZ
{
	[RegisterInjectable(typeof(MaimaiDXUserPortraitHandler))]
	public class MaimaiDXUserPortraitHandler
	{
		private readonly ILogger<MaimaiDXUserPortraitHandler> logger;
		private readonly MaimaiDXDB maimaiDxDB;
		private readonly IFileProvider fileProvider;
		private bool enable;
		private int divMaxLength;

		public const string PicSaveFolderName = "MaimaiDXUserPortraits";

		private string picSavePath;

		public MaimaiDXUserPortraitHandler(ILogger<MaimaiDXUserPortraitHandler> logger, MaimaiDXDB maimaiDxDB, IFileProvider fileProvider)
		{
			this.logger = logger;
			this.maimaiDxDB = maimaiDxDB;
			this.fileProvider = fileProvider;

			enable = true;//todo 加个开关随时关闭启动
			divMaxLength = 10;

			picSavePath = Path.GetFullPath(fileProvider.GetFileInfo(PicSaveFolderName).PhysicalPath);
		}

		public async ValueTask<UpsertResponseVO> UploadUserPortrait(UploadUserPortraitRequestVO request)
		{
			var userPhoto = request.userPortrait;
			var userDetail = await maimaiDxDB.UserDetails.FirstOrDefaultAsync(x => x.Id == userPhoto.userId);
			var userId = userPhoto.userId;

			if (enable)
			{
				var divNumber = userPhoto.divNumber;
				var divLength = userPhoto.divLength;
				var divData = userPhoto.divData;

				if (divLength > divMaxLength)
				{
					logger.LogWarning($"stop user {userId} uploading photo data because divLength({divLength}) > divMaxLength({divMaxLength})");
					return new()
					{
						apiName = nameof(MaimaiDXUserPortraitHandler),
						returnCode = 0
					};
				}

				var tmp_filename = Path.Combine(picSavePath, $"{userId}-up.tmp");
				if (divNumber == 0)
					File.Delete(tmp_filename);

				var imageData = Convert.FromBase64String(divData);
				using (var fs = File.Open(tmp_filename, FileMode.Append, FileAccess.ReadWrite))
					await fs.WriteAsync(imageData);

				logger.LogInformation($"received user {userId} photo data {divNumber + 1}/{divLength}");

				if (divNumber == (divLength - 1))
				{
					var filename = Path.Combine(picSavePath, $"{userId}-up.jpg");
					File.Move(tmp_filename, filename, true);

					userPhoto.divData = string.Empty;
					var userPortaitMetaJson = JsonSerializer.Serialize(userPhoto);
					var json_filename = Path.Combine(picSavePath, $"{userId}-up.json");

					using (var fs = File.Open(tmp_filename, FileMode.Truncate, FileAccess.ReadWrite))
						await fs.WriteAsync(Encoding.UTF8.GetBytes(userPortaitMetaJson));

					logger.LogInformation($"saved user {userId} photo data");
				}
			}

			return new()
			{
				apiName = nameof(MaimaiDXUserPortraitHandler),
				returnCode = 1
			};
		}

		public async ValueTask<GetUserPortraitResponseVO> GetUserPortrait(GetUserPortraitRequestVO request)
		{
			var userDetail = await maimaiDxDB.UserDetails.FirstOrDefaultAsync(x => x.Id == request.userId);

			var response = new GetUserPortraitResponseVO();
			var list = new List<UserPortrait>();

			var filePath = Path.Combine(picSavePath, userDetail.Id + "-up.jpg");
			var fileInfo = fileProvider.GetFileInfo(filePath);

			if (fileInfo.Exists)
			{
				var templateJsonFilePath = Path.Combine(picSavePath, userDetail.Id + "-up.json");
				var templateJsonFileInfo = fileProvider.GetFileInfo(templateJsonFilePath);

				if (templateJsonFileInfo.Exists)
				{
					using var jsonStream = templateJsonFileInfo.CreateReadStream();
					var jsonContent = await new StreamReader(jsonStream).ReadToEndAsync();

					using var bufferDisp = ArrayPool<byte>.Shared.RentWithDisposable(10240);
					var buffer = bufferDisp.Memory;

					using var fileStream = fileInfo.CreateReadStream();
					while (true)
					{
						var read = await fileStream.ReadAsync(buffer);
						if (read == 0)
							break;

						var userPortrait = JsonSerializer.Deserialize<UserPortrait>(jsonContent);
						userPortrait.divData = Convert.ToBase64String(buffer.Span);
						userPortrait.divNumber = list.Count;

						list.Add(userPortrait);
					}

					foreach (var userPortrait in list)
					{
						userPortrait.divLength = list.Count;
					}
				}
				else
				{
					logger.LogWarning($".jpg is contained but .json is not. userId: {request.userId}");
				}
			}

			response.userPortraitList = list;
			response.length = list.Count;

			return response;
		}
	}
}
