using Aminos.Databases.Title.SDEZ;
using Aminos.Kernels.Injections.Attrbutes;
using Aminos.Models.Title.SDEZ.Requests;
using Aminos.Models.Title.SDEZ.Responses;
using Aminos.Models.Title.SDEZ.Tables;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Handlers.Title.SDEZ
{
    [RegisterInjectable(typeof(MaimaiDXUserChargeHandler))]
	public class MaimaiDXUserChargeHandler
	{
		private readonly MaimaiDXDB maimaiDxDB;

		public MaimaiDXUserChargeHandler(MaimaiDXDB maimaiDxDB)
		{
			this.maimaiDxDB = maimaiDxDB;
		}

		public async ValueTask<UserChargeResponseVO> GetUserCharge(UserChargeRequestVO request)
		{
			var userDetail = await maimaiDxDB.UserDetails.Include(x => x.UserCharges)
				.FirstOrDefaultAsync(x => x.Id == request.userId);

			var response = new UserChargeResponseVO();
			response.userChargeList = userDetail.UserCharges.ToArray();
			response.userId = request.userId;
			response.length = response.userChargeList.Length;

			return response;
		}

		public async ValueTask<UpsertResponseVO> UpsertUserChargelog(UserChargelogRequestVO request)
		{
			var userDetail = await maimaiDxDB.UserDetails.Include(x => x.UserCharges)
				.FirstOrDefaultAsync(x => x.Id == request.userId);

			var userCharge = request.userCharge;
			var state = EntityState.Added;
			if (userDetail.UserCharges.FirstOrDefault(x => x.chargeId == userCharge.chargeId) is UserCharge storedUserCharge)
			{
				userCharge.Id = storedUserCharge.Id;
				state = EntityState.Modified;
			}
			maimaiDxDB.Entry(userCharge).State = state;

			userDetail.UserChargelogs.Add(request.userChargelog);
			await maimaiDxDB.SaveChangesAsync();

			var response = new UpsertResponseVO();
			response.returnCode = 1;
			response.apiName = nameof(MaimaiDXUserChargeHandler);
			return response;
		}
	}
}
