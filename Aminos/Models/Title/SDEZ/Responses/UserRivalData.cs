using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Models.Title.SDEZ.Responses
{
	public class UserRivalData
	{
		public ulong rivalId;

		public string rivalName;
	}
}