using Aminos.Databases.Title.SDEZ;
using Aminos.Kernels.Injections.Attrbutes;
using Aminos.Models.Title.SDEZ.Requests;
using Aminos.Models.Title.SDEZ.Responses;

namespace Aminos.Handlers.Title.SDEZ
{
	[RegisterInjectable(typeof(MaimaiDXGameSettingHandler))]
	public class MaimaiDXGameSettingHandler
	{
		public Task<GameSettingResponseVO> GetGameSetting(GameSettingRequestVO request)
		{
			//todo 后面弄成个人自己自行设置

			var response = new GameSettingResponseVO();
			response.isAouAccession = true;
			response.gameSetting = new()
			{
				rebootEndTime = DateTime.MaxValue.ToString("yyyy-MM-dd HH:mm:ss.f"),
				rebootStartTime = DateTime.MaxValue.ToString("yyyy-MM-dd HH:mm:ss.f"),
				requestInterval = 10,
				isMaintenance = false,
				movieUploadLimit = 10000,
				movieStatus = 0,
				movieServerUri = string.Empty,
				deliverServerUri = string.Empty,
				usbDlServerUri = string.Empty,
				oldServerUri = string.Empty,
				rebootInterval = 0,
			};

			return Task.FromResult(response);
		}
	}
}
