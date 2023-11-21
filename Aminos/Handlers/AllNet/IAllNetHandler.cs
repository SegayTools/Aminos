using Aminos.Models.AllNet.Requests;
using Aminos.Models.AllNet.Responses;

namespace Aminos.Handlers.AllNet
{
	public interface IAllNetHandler
	{
		ValueTask<PowerOnResponseBase> HandlePowerOn(PowerOnRequest request, ConnectionInfo connectionInfo);
	}
}
