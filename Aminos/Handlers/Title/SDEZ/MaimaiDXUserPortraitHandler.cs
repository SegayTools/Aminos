﻿using Aminos.Databases.Title.SDEZ;
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
		private bool enable;
		private int divMaxLength;

		public const string PicSaveFolderName = "MaimaiDXUserPortraits";

		private string picSavePath;

		public MaimaiDXUserPortraitHandler(ILogger<MaimaiDXUserPortraitHandler> logger, IFileProvider fileProvider, MaimaiDXDB maimaiDxDB)
		{
			this.logger = logger;
			this.maimaiDxDB = maimaiDxDB;

			enable = true;//todo 加个开关随时关闭启动
			divMaxLength = 10;

			picSavePath = Path.GetFullPath(fileProvider.GetFileInfo(PicSaveFolderName).PhysicalPath);
			Directory.CreateDirectory(picSavePath);
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
				if (divNumber == 0 && File.Exists(tmp_filename))
					File.Delete(tmp_filename);

				var imageData = Convert.FromBase64String(divData);
				using (var fs = File.Open(tmp_filename, FileMode.Append, FileAccess.Write))
					await fs.WriteAsync(imageData);

				logger.LogInformation($"received user {userId} photo data {divNumber + 1}/{divLength}");

				if (divNumber == (divLength - 1))
				{
					var filename = Path.Combine(picSavePath, $"{userId}-up.jpg");
					File.Move(tmp_filename, filename, true);

					userPhoto.divData = string.Empty;
					var userPortaitMetaJson = JsonSerializer.Serialize(userPhoto);
					var json_filename = Path.Combine(picSavePath, $"{userId}-up.json");

					using (var fs = File.Open(json_filename, FileMode.Create, FileAccess.Write))
						await fs.WriteAsync(Encoding.UTF8.GetBytes(userPortaitMetaJson));

					logger.LogInformation($"saved user {userId} photo data: {filename}");
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
			var templateJsonFilePath = Path.Combine(picSavePath, userDetail.Id + "-up.json");

			if (File.Exists(filePath))
			{
				if (File.Exists(templateJsonFilePath))
				{
					var jsonContent = await File.ReadAllTextAsync(templateJsonFilePath);

					using var bufferDisp = ArrayPool<byte>.Shared.RentWithDisposable(10240);
					var buffer = bufferDisp.Memory;

					using var fileStream = File.OpenRead(filePath);
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
