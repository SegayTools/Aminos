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
		private readonly MaimaiDXDB maimaiDxDB;
		private readonly IFileProvider fileProvider;

		public const string PicSavePath = "MaimaiDXUserPhotos";

		public MaimaiDXUploadUserPhotoHandler(MaimaiDXDB maimaiDxDB, IFileProvider fileProvider)
		{
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
			var tempFilePath = Path.Combine(picSavePhysicalPath, "temp", $"{userId}-{trackNo}.tmp");

			using (var fs = File.Open(tempFilePath, FileMode.Append, FileAccess.ReadWrite))
				await fs.WriteAsync(imageData);

			if (divNumber == (divLength - 1))
			{
				var newFilePath = Path.Combine(picSavePhysicalPath, userId.ToString(), $"{DateTime.Now}.jpg");
				File.Move(tempFilePath, newFilePath, true);
			}

			var response = new UpsertResponseVO();
			response.apiName = nameof(MaimaiDXUploadUserPhotoHandler);
			response.returnCode = 1;
			return response;
		}
	}
}
