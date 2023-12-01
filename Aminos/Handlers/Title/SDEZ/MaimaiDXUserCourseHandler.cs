using Aminos.Databases.Title.SDEZ;
using Aminos.Core.Services.Injections.Attrbutes;
using Aminos.Core.Models.Title.SDEZ.Requests;
using Aminos.Core.Models.Title.SDEZ.Responses;
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
				
				.FirstOrDefaultAsync(x => x.Id == request.userId);

			var response = new UserCourseResponseVO();
			response.userCourseList = userDetail.UserCourses.Skip((int)request.nextIndex).ToArray();
			response.nextIndex = request.nextIndex + response.userCourseList.LongLength;
			if (response.userCourseList.Length == 0)
				response.nextIndex = 0;
			response.userId = request.userId;

			return response;
		}
	}
}
