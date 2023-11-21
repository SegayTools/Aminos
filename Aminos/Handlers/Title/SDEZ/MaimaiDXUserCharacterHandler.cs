using Aminos.Databases.Title.SDEZ;
using Aminos.Kernels.Injections.Attrbutes;
using Aminos.Models.Title.SDEZ.Requests;
using Aminos.Models.Title.SDEZ.Responses;
using Aminos.Models.Title.SDEZ.Tables;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Handlers.Title.SDEZ
{
    [RegisterInjectable(typeof(MaimaiDXUserCharacterHandler))]
	public class MaimaiDXUserCharacterHandler
	{
		private readonly MaimaiDXDB maimaiDxDB;

		public MaimaiDXUserCharacterHandler(MaimaiDXDB maimaiDxDB)
		{
			this.maimaiDxDB = maimaiDxDB;
		}

		public async ValueTask<UserCharacterResponseVO> GetUserCharacter(UserCharacterRequestVO request)
		{
			var userDetail = await maimaiDxDB.UserDetails.Include(x=>x.UserCharacters).FirstOrDefaultAsync(x => x.Id == request.userId);

			var response = new UserCharacterResponseVO();
			response.userId = request.userId;
			response.userCharacterList = userDetail?.UserCharacters.ToArray() ?? new UserCharacter[0];

			return response;
		}
	}
}
