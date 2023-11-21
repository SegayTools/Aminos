using Aminos.Databases.Title.SDEZ;
using Aminos.Kernels.Injections.Attrbutes;
using Aminos.Models.Title.SDEZ.Requests;
using Aminos.Models.Title.SDEZ.Responses;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Handlers.Title.SDEZ
{
	[RegisterInjectable(typeof(MaimaiDXUploadUserPlaylogHandler))]
	public class MaimaiDXUploadUserPlaylogHandler
	{
		private readonly MaimaiDXDB maimaiDxDB;

		public MaimaiDXUploadUserPlaylogHandler(MaimaiDXDB maimaiDxDB)
		{
			this.maimaiDxDB = maimaiDxDB;
		}

		public async ValueTask<UpsertResponseVO> UploadUserPlaylog(UserPlaylogRequestVO request)
		{
			var userDetail = await maimaiDxDB.UserDetails
				.Include(x => x.UserPlaylogs)
				.FirstOrDefaultAsync(x => x.Id == request.userId);

			using var transaction = await maimaiDxDB.Database.BeginTransactionAsync();
			{
				userDetail.UserPlaylogs.Add(request.userPlaylog);
				await maimaiDxDB.SaveChangesAsync();
			}
			await transaction.CommitAsync();

			var response = new UpsertResponseVO();
			response.returnCode = 1;
			response.apiName = nameof(MaimaiDXUploadUserPlaylogHandler);

			return response;
		}
	}
}
