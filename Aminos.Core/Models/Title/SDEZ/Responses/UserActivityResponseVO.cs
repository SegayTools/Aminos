using Aminos.Core.Models.Title.SDEZ.Tables;

namespace Aminos.Core.Models.Title.SDEZ.Responses
{
	public class UserActivityResponseVO
	{
		public class UserActivityWrap
		{
			public UserActivityWrap(IEnumerable<UserActivity> activities)
			{
				playList = activities.Where(x => x.kind == 1).ToList();
				musicList = activities.Where(x => x.kind == 2).ToList();
			}
			
			public List<UserActivity> playList{ get; set; }
			public List<UserActivity> musicList{ get; set; }
		}
		
		//public UserActivity userActivity { get; set; }
		public UserActivityWrap userActivity { get; set; }
	}
}
