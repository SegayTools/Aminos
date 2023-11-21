using Aminos.Databases.Title.SDEZ;
using Aminos.Kernels.Injections.Attrbutes;
using Aminos.Models.Title.SDEZ.Requests;
using Aminos.Models.Title.SDEZ.Responses;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Handlers.Title.SDEZ
{
	[RegisterInjectable(typeof(MaimaiDXUserCourseHandler))]
	public class MaimaiDXUserCourseHandler
	{
		private readonly MaimaiDXDB maimaiDxDB;

		public MaimaiDXUserCourseHandler(MaimaiDXDB maimaiDxDB)
		{
			this.maimaiDxDB = maimaiDxDB;
		}

		public async Task<UserCourseResponseVO> GetUserCourse(UserCourseRequestVO request)
		{
			var userDetail = await maimaiDxDB.UserDetails
				.Include(x => x.UserCourses)
				.FirstOrDefaultAsync(x => x.Id == request.userId);

			var response = new UserCourseResponseVO();
			response.userCourseList = userDetail.UserCourses.Skip((int)request.nextIndex).ToArray();
			response.nextIndex = request.nextIndex + response.userCourseList.LongLength;
			response.userId = request.userId;

			return response;
		}
	}
}
