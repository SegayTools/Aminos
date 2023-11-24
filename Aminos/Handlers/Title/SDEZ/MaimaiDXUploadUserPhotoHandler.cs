using Aminos.Databases.Title.SDEZ;
using Aminos.Services.Injections.Attrbutes;
using Aminos.Models.Title.SDEZ.Requests;
using Aminos.Models.Title.SDEZ.Responses;
using Aminos.Services.Files;
using Aminos.Services.Injections.Attrbutes;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace Aminos.Handlers.Title.SDEZ
{
    [RegisterInjectable(typeof(MaimaiDXUploadUserPhotoHandler))]
	public class MaimaiDXUploadUserPhotoHandler
	{
		private readonly ILogger<MaimaiDXUploadUserPhotoHandler> logger;
		private readonly MaimaiDXDB maimaiDxDB;
		private readonly IFileProvider fileProvider;

		public const string PicSavePath = "MaimaiDXUserPhotos";
		private string picSaveFolderPath;
		private string picTempSaveFolderPath;

		public MaimaiDXUploadUserPhotoHandler(ILogger<MaimaiDXUploadUserPhotoHandler> logger, MaimaiDXDB maimaiDxDB, IApplicationFilePath applicationPath)
		{
			this.logger = logger;
			this.maimaiDxDB = maimaiDxDB;
			picSaveFolderPath = Path.Combine(applicationPath.ApplicationDataFolderPath, PicSavePath);
			picTempSaveFolderPath = Path.Combine(picSaveFolderPath, "temp");
			Directory.CreateDirectory(picSaveFolderPath);
			Directory.CreateDirectory(picTempSaveFolderPath);
		}

		public async ValueTask<UpsertResponseVO> UploadUserPhoto(UserPhotoRequestVO request)
		{
			var userPhoto = request.userPhoto;

			var userId = userPhoto.userId;
			var trackNo = userPhoto.trackNo;
			var divNumber = userPhoto.divNumber;
			var divLength = userPhoto.divLength;
			var userDetail = await maimaiDxDB.UserDetails.FirstOrDefaultAsync(x => x.Id == userId);

			var divData = userPhoto.divData;

			var imageData = Convert.FromBase64String(divData);
			var tempFilePath = Path.Combine(picTempSaveFolderPath, $"{userId}-{trackNo}.tmp");

			if (divNumber == 0 && File.Exists(tempFilePath))
				File.Delete(tempFilePath);

			using (var fs = File.Open(tempFilePath, FileMode.Append, FileAccess.Write))
				await fs.WriteAsync(imageData);

			if (divNumber == (divLength - 1))
			{
				var userFolder = Path.Combine(picSaveFolderPath, userId.ToString());
				Directory.CreateDirectory(userFolder);
				var newFilePath = Path.Combine(userFolder, $"{DateTime.Now:yyyy-MM-dd HH-mm-ss} {trackNo}.jpg");
				File.Move(tempFilePath, newFilePath, true);

				logger.LogInformation($"save user {userId} photo to {newFilePath}");
			}

			var response = new UpsertResponseVO();
			response.apiName = nameof(MaimaiDXUploadUserPhotoHandler);
			response.returnCode = 1;
			return response;
		}
	}
}
