using Aminos.Databases.Title.SDEZ;
using Aminos.Kernels.Injections.Attrbutes;
using Aminos.Models.Title.SDEZ.Requests;
using Aminos.Models.Title.SDEZ.Responses;
using Aminos.Models.Title.SDEZ.Tables;

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

		public async ValueTask<UserGhostResponseVO> GetUserGhost(UserGhostRequestVO request)
		{
			var response = new UserGhostResponseVO();
			response.userGhostList = new UserGhost[0];
			response.userId = request.userId;

			return response;
		}
	}
}
