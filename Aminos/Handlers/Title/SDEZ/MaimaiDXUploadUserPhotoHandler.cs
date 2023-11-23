using Aminos.Databases.Title.SDEZ;
using Aminos.Kernels.Injections.Attrbutes;
using Aminos.Models.Title.SDEZ.Requests;
using Aminos.Models.Title.SDEZ.Responses;
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

		public MaimaiDXUploadUserPhotoHandler(ILogger<MaimaiDXUploadUserPhotoHandler> logger, MaimaiDXDB maimaiDxDB, IFileProvider fileProvider)
		{
			this.logger = logger;
			this.maimaiDxDB = maimaiDxDB;
			this.fileProvider = fileProvider;
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
			var picSavePhysicalPath = Path.GetFullPath(fileProvider.GetFileInfo(PicSavePath).PhysicalPath);
			var tempFolder = Path.Combine(picSavePhysicalPath, "temp");
			Directory.CreateDirectory(tempFolder);
			var tempFilePath = Path.Combine(tempFolder, $"{userId}-{trackNo}.tmp");

			if (divNumber == 0 && File.Exists(tempFilePath))
				File.Delete(tempFilePath);

			using (var fs = File.Open(tempFilePath, FileMode.Append, FileAccess.Write))
				await fs.WriteAsync(imageData);

			if (divNumber == (divLength - 1))
			{
				var userFolder = Path.Combine(picSavePhysicalPath, userId.ToString());
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
