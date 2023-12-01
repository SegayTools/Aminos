using Aminos.Core.Models.General.Tables;
using Microsoft.AspNetCore.Identity;

namespace Aminos.Authorization
{
	public interface IKeychipSafeHandleAuthorization
	{
		/// <summary>
		/// 通过keychip生成safeHandle
		/// </summary>
		/// <param name="keychip"></param>
		/// <returns></returns>
		public ValueTask<string> GenerateSafeHandle(Keychip keychip);

		/// <summary>
		/// 检查此safeHandle是否有对应的权限
		/// </summary>
		/// <param name="safeHandle"></param>
		/// <returns></returns>
		public ValueTask<bool> AuthorizeVerfiy(string safeHandle);

		/// <summary>
		/// 清空所有有效时间，迫使所有safeHandle和Keychip在验证权限时，
		/// 都要做一次有效性检查
		/// </summary>
		/// <returns></returns>
		public ValueTask ExpiredAll();
	}
}
