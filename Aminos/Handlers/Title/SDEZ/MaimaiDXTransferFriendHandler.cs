using Aminos.Core.Services.Injections.Attrbutes;
using Aminos.Core.Models.Title.SDEZ.Requests;
using Aminos.Core.Models.Title.SDEZ.Responses;

namespace Aminos.Handlers.Title.SDEZ
{
	[RegisterInjectable(typeof(MaimaiDXTransferFriendHandler))]
	public class MaimaiDXTransferFriendHandler
	{
		public Task<TransferFriendResponseVO> GetTransferFriend(TransferFriendRequestVO request)
		{
			//todo 有时间看看能不能实现吧

			var response = new TransferFriendResponseVO();
			response.userId = request.userId;
			response.transferFriendList = new TransferFriend[0];

			return Task.FromResult(response);
		}
	}
}
