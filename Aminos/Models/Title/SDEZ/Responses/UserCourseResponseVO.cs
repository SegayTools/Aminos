using Aminos.Models.Title.SDEZ.Tables;

namespace Aminos.Models.Title.SDEZ.Responses
{
    public class UserCourseResponseVO
	{
		public ulong userId;

		public long nextIndex;

		public UserCourse[] userCourseList;
	}
}
