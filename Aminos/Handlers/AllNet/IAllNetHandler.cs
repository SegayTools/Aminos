using Aminos.Models.AllNet.Responses;
using Aminos.Models.AllNet.Requests;
using System.Net;

namespace Aminos.Handlers.AllNet
{
	public interface IAllNetHandler
	{
		ValueTask<PowerOnResponseBase> HandlePowerOn(PowerOnRequest request, ConnectionInfo connectionInfo);
	}
}
