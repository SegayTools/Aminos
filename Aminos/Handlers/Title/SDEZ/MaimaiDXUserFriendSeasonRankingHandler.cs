using Aminos.Databases.Title.SDEZ;
using Aminos.Core.Services.Injections.Attrbutes;
using Aminos.Core.Models.Title.SDEZ.Requests;
using Aminos.Core.Models.Title.SDEZ.Responses;
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
				
				.FirstOrDefaultAsync(x => x.Id == request.userId);

			var response = new UserFriendSeasonRankingResponseVO();
			response.userId = request.userId;
			response.userFriendSeasonRankingList = userDetail.UserFriendSeasonRankings
				.Skip((int)request.userId).Take(request.maxCount)
				.ToArray();
			response.nextIndex = request.nextIndex + response.userFriendSeasonRankingList.LongLength;
			if (response.userFriendSeasonRankingList.Length == 0)
				response.nextIndex = 0;

			return response;
		}
	}
}
