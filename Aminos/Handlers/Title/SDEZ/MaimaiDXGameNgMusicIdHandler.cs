using Aminos.Services.Injections.Attrbutes;
using Aminos.Models.Title.SDEZ.Requests;
using Aminos.Models.Title.SDEZ.Responses;

namespace Aminos.Handlers.Title.SDEZ
{
	[RegisterInjectable(typeof(MaimaiDXGameNgMusicIdHandler))]
	public class MaimaiDXGameNgMusicIdHandler
	{
		public ValueTask<GameNgMusicIdResponseVO> GetGameNgMusicId(GameNgMusicIdRequestVO request)
		{
			var response = new GameNgMusicIdResponseVO();
			response.musicIdList = new int[0];
			response.length = 0;

			return ValueTask.FromResult(response);
		}
	}
}
