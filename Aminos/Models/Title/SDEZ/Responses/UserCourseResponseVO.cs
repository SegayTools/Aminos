using Aminos.Models.Title.SDEZ.Tables;

namespace Aminos.Models.Title.SDEZ.Responses
{
	public class UserCourseResponseVO
	{
		public ulong userId { get; set; }

		public long nextIndex { get; set; }

		public UserCourse[] userCourseList { get; set; }
	}
}
