using Aminos.Core.Models.General;
using Aminos.Core.Models.General.Tables;
using Aminos.Core.Models.Title.SDEZ.Tables;
using Aminos.Core.Services.Injections.Attrbutes;
using AminosUI.Services.Applications.Network;
using AminosUI.Utils.MethodExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AminosUI.Services.Applications.User.DefaultImpl
{
	[RegisterInjectable(typeof(IUserManager))]
	internal class DefaultUserManager : IUserManager
	{
		private readonly IApplicationHttpFactory applicationHttpFactory;
		private readonly MD5 md5;

		public UserAccount CurrentUser => throw new NotImplementedException();

		public DefaultUserManager(IApplicationHttpFactory applicationHttpFactory)
		{
			this.applicationHttpFactory = applicationHttpFactory;
			md5 = MD5.Create();
		}

		public async ValueTask<bool> Login(string username, string password)
		{
			var passwordHash = HashPassword(password);

			var resp = await applicationHttpFactory.Post<CommonApiResponse>("api/Account/Login", new
			{
				passwordHash,
				username
			});

			return resp.isSuccess;
		}

		private string HashPassword(string password)
		{
			return Convert.ToHexString(md5.ComputeHash(Encoding.UTF8.GetBytes(password + "2857")));
		}

		public async ValueTask<bool> Logout()
		{
			var resp = await applicationHttpFactory.Post<CommonApiResponse>("api/Account/Logout");

			return resp.isSuccess;
		}
	}
}
