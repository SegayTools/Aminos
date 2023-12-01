using Aminos.Databases.Title.SDEZ;
using Aminos.Core.Services.Injections.Attrbutes;
using Aminos.Core.Models.Title.SDEZ.Requests;
using Aminos.Core.Models.Title.SDEZ.Responses;
using Aminos.Core.Models.Title.SDEZ.Tables;

namespace Aminos.Handlers.Title.SDEZ
{
	[RegisterInjectable(typeof(MaimaiDXUserGhostHandler))]
	public class MaimaiDXUserGhostHandler
	{
		private readonly MaimaiDXDB maimaiDxDB;

		public MaimaiDXUserGhostHandler(MaimaiDXDB maimaiDxDB)
		{
			this.maimaiDxDB = maimaiDxDB;
		}

		public ValueTask<UserGhostResponseVO> GetUserGhost(UserGhostRequestVO request)
		{
			var response = new UserGhostResponseVO();
			response.userGhostList = new UserGhost[0];
			response.userId = request.userId;

			return ValueTask.FromResult(response);
		}
	}
}
