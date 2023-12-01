using Aminos.Core.Models.General.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AminosUI.Services.Applications.User
{
	public interface IUserManager
	{
		UserAccount CurrentUser { get; }

		ValueTask<bool> Login(string username, string password);
		ValueTask<bool> Logout();
	}
}
