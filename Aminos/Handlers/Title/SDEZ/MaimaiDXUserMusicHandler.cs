using Aminos.Databases.Title.SDEZ;
using Aminos.Kernels.Injections.Attrbutes;
using Aminos.Models.Title.SDEZ.Requests;
using Aminos.Models.Title.SDEZ.Responses;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Handlers.Title.SDEZ
{
	[RegisterInjectable(typeof(MaimaiDXUserMusicHandler))]
	public class MaimaiDXUserMusicHandler
	{
		private readonly MaimaiDXDB maimaiDxDB;

		public MaimaiDXUserMusicHandler(MaimaiDXDB maimaiDxDB)
		{
			this.maimaiDxDB = maimaiDxDB;
		}

		public async ValueTask<UserMusicResponseVO> GetUserMusic(UserMusicRequestVO request)
		{
			var userDetail = await maimaiDxDB.UserDetails
				
				.FirstOrDefaultAsync(x => x.Id == request.userId);

			var musicDetails = userDetail.UserMusicDetails.Skip(request.nextIndex).Take(request.maxCount).ToArray();

			var response = new UserMusicResponseVO();
			response.userId = request.userId;
			response.userMusicList = musicDetails.Length > 0 ? new UserMusic[] { new() { userMusicDetailList = musicDetails } } : new UserMusic[0];
			response.nextIndex = request.nextIndex + musicDetails.Length;
			if (musicDetails.Length == 0)
				response.nextIndex = 0;
			return response;
		}
	}
}
