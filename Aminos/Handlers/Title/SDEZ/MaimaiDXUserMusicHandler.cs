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
				.Include(x => x.UserMusicDetails)
				.FirstOrDefaultAsync(x => x.Id == request.userId);

			var musicDetails = userDetail.UserMusicDetails.Skip(request.nextIndex).Take(request.maxCount).ToArray();

			var response = new UserMusicResponseVO();
			response.userId = request.userId;
			response.userMusicList = new UserMusic[] { new() { userMusicDetailList = musicDetails } };
			response.nextIndex = request.nextIndex + musicDetails.Length;

			return response;
		}
	}
}
