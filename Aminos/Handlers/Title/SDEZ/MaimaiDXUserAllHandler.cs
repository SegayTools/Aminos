using Aminos.Databases;
using Aminos.Databases.Title.SDEZ;
using Aminos.Kernels.Injections.Attrbutes;
using Aminos.Models.Title.SDEZ.Requests;
using Aminos.Models.Title.SDEZ.Responses;
using Aminos.Models.Title.SDEZ.Tables;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Handlers.Title.SDEZ
{
    [RegisterInjectable(typeof(MaimaiDXUserAllHandler))]
	public class MaimaiDXUserAllHandler
	{
		private readonly MaimaiDXDB maimaiDxDB;
		private readonly AminosDB aminosDB;

		public MaimaiDXUserAllHandler(MaimaiDXDB maimaiDxDB, AminosDB aminosDB)
		{
			this.maimaiDxDB = maimaiDxDB;
			this.aminosDB = aminosDB;
		}

		public async ValueTask<UpsertResponseVO> UpsertUserAll(UserAllRequestVO request)
		{
			if ((request.userId & 281474976710657UL) == 281474976710657UL)
			{
				//it's guest
				return new()
				{
					returnCode = 1,
					apiName = nameof(MaimaiDXUserAllHandler)
				};
			}

			#region UserDetail

			var userData = request.upsertUserAll.userData?.ElementAtOrDefault(0);

			if (userData == null)
			{
				return new()
				{
					returnCode = 0,
					apiName = nameof(MaimaiDXUserAllHandler)
				};
			}

			using (var transaction = await maimaiDxDB.Database.BeginTransactionAsync())
			{
				var isNew = !await maimaiDxDB.UserDetails.ContainsAsync(userData);
				if (isNew)
					await maimaiDxDB.UserDetails.AddAsync(userData);
				else
				{
					maimaiDxDB.Update(userData);
				}

				userData.isNetMember = 1;
				await maimaiDxDB.SaveChangesAsync();
				await transaction.CommitAsync();
			}

			#endregion

			foreach (var collection in maimaiDxDB.Entry(userData).Collections)
				await collection.LoadAsync();

			#region UserExtend

			var userExtend = request.upsertUserAll.userExtend.FirstOrDefault();
			if (userExtend != null)
				userData.UserExtend = userExtend;

			#endregion

			#region UserOption

			var userOption = request.upsertUserAll.userOption?.FirstOrDefault();
			if (userOption != null)
				userData.UserOption = userOption;

			#endregion

			void SimpleRepalceEntites<T>(IEnumerable<T> inList, ICollection<T> existList, Func<T, int> specIdGetter, Action<T, T> idCopyFunc) where T : class
			{
				if (inList != null)
				{
					foreach (var inEntity in inList)
					{
						if (existList.FirstOrDefault(x => specIdGetter(x) == specIdGetter(inEntity)) is T storedEntity)
						{
							idCopyFunc(inEntity, storedEntity);
							maimaiDxDB.Attach(inEntity).State = EntityState.Modified;
						}
						else
						{
							existList.Add(inEntity);
						}
					}
				}
			}

			#region UserCharacterList

			SimpleRepalceEntites(
				request.upsertUserAll.userCharacterList,
				userData.UserCharacters,
				a => a.characterId,
				(a, b) => a.Id = b.Id);

			#endregion

			#region UserGhostList

			//todo UserGhostList

			#endregion

			#region UserMapList

			SimpleRepalceEntites(
				request.upsertUserAll.userMapList,
				userData.UserMaps,
				a => a.mapId,
				(a, b) => a.Id = b.Id);

			#endregion

			#region UserLoginBonusList

			SimpleRepalceEntites(
				request.upsertUserAll.userLoginBonusList,
				userData.UserLoginBonuses,
				a => a.bonusId,
				(a, b) => a.Id = b.Id);

			#endregion

			#region UserRatingList

			var userRating = request.upsertUserAll.userRatingList?.FirstOrDefault();
			if (userRating != null)
				userData.UserRating = userRating;

			#endregion

			#region UserItemList

			SimpleRepalceEntites(
				request.upsertUserAll.userItemList,
				userData.UserItems,
				a => a.itemId,
				(a, b) => a.Id = b.Id);

			#endregion

			#region UserMusicDetailList

			SimpleRepalceEntites(
				request.upsertUserAll.userMusicDetailList,
				userData.UserMusicDetails,
				a => a.musicId,
				(a, b) => a.Id = b.Id);

			#endregion

			#region UserCourseList

			SimpleRepalceEntites(
				request.upsertUserAll.userCourseList,
				userData.UserCourses,
				a => a.courseId,
				(a, b) => a.Id = b.Id);

			#endregion

			#region UserFriendSeasonRankingList

			SimpleRepalceEntites(
				request.upsertUserAll.userFriendSeasonRankingList,
				userData.UserFriendSeasonRankings,
				a => a.seasonId,
				(a, b) => a.Id = b.Id);

			#endregion

			#region UserFavoriteList

			SimpleRepalceEntites(
				request.upsertUserAll.userFavoriteList,
				userData.UserFavorites,
				a => a.Id,
				(a, b) => a.Id = b.Id);

			#endregion

			#region UserActivityList

			var userActivity = request.upsertUserAll.userActivityList?.FirstOrDefault();
			if (userOption != null)
				userData.UserActivity = userActivity;

			#endregion

			#region User2pPlaylog

			var user2pPlaylog = request.upsertUserAll.user2pPlaylog;
			if (user2pPlaylog != null)
			{
				var userData1 = await maimaiDxDB.UserDetails.FirstOrDefaultAsync(x => x.Id == user2pPlaylog.userId1);
				var userData2 = await maimaiDxDB.UserDetails.FirstOrDefaultAsync(x => x.Id == user2pPlaylog.userId2);

				if (userData1.Id > userData2.Id)
					(userData1, userData2) = (userData2, userData1);

				if (user2pPlaylog.User2pPlaylogDetails != null)
				{
					foreach (var upsertDetail in user2pPlaylog.User2pPlaylogDetails)
					{
						var state = EntityState.Added;
						upsertDetail.UserDetail1 = userData1;
						upsertDetail.UserDetail2 = userData2;

						if ((await maimaiDxDB.User2pPlaylogDetails.FirstOrDefaultAsync(x =>
							((x.UserDetail2 == upsertDetail.UserDetail2 && x.UserDetail1 == upsertDetail.UserDetail1)
							|| (x.UserDetail1 == upsertDetail.UserDetail2 && x.UserDetail2 == upsertDetail.UserDetail1))
							&& x.level == upsertDetail.level
							&& x.musicId == upsertDetail.musicId
						)) is User2pPlaylogDetail storedDetail)
						{
							state = EntityState.Modified;
							upsertDetail.Id = storedDetail.Id;
						}
						maimaiDxDB.Entry(upsertDetail).State = state;
					}
				}
			}

			#endregion

			#region UserGamePlaylogList

			SimpleRepalceEntites(
				request.upsertUserAll.userGamePlaylogList,
				userData.UserGamePlaylogs,
				a => a.Id,
				(a, b) => a.Id = b.Id);

			#endregion

			return new()
			{
				returnCode = 1,
				apiName = nameof(MaimaiDXUserAllHandler)
			};
		}
	}
}
