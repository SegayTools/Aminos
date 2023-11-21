using Aminos.Databases.Title.SDEZ;
using Aminos.Kernels.Injections.Attrbutes;
using Aminos.Models.Title.SDEZ.Requests;
using Aminos.Models.Title.SDEZ.Responses;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Handlers.Title.SDEZ
{
	[RegisterInjectable(typeof(MaimaiDXUserFriendSeasonRankingHandler))]
	public class MaimaiDXUserFriendSeasonRankingHandler
	{
		private readonly MaimaiDXDB maimaiDxDB;

		public MaimaiDXUserFriendSeasonRankingHandler(MaimaiDXDB maimaiDxDB)
		{
			this.maimaiDxDB = maimaiDxDB;
		}

		public async ValueTask<UserFriendSeasonRankingResponseVO> GetUserFriendSeasonRanking(UserFriendSeasonRankingRequestVO request)
		{
			var userDetail = await maimaiDxDB.UserDetails
				.Include(x => x.UserFriendSeasonRankings)
				.FirstOrDefaultAsync(x => x.Id == request.userId);

			var response = new UserFriendSeasonRankingResponseVO();
			response.userId = request.userId;
			response.userFriendSeasonRankingList = userDetail.UserFriendSeasonRankings
				.Skip((int)request.userId).Take(request.maxCount)
				.ToArray();
			response.nextIndex = request.nextIndex + response.userFriendSeasonRankingList.LongLength;

			return response;
		}
	}
}
