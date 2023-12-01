namespace Aminos.Core.Models.General
{
	public record CommonApiResponse(bool isSuccess, string message = default);
	public record CommonApiResponse<T>(bool isSuccess, T obj, string message = default) :
		CommonApiResponse(isSuccess, message);
	public record CommonApiInternalExceptionResponse(Guid trackId) :
		CommonApiResponse(false, $"<内部错误:{trackId}>");
}
