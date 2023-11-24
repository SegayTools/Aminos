using Aminos.Databases;
using Aminos.Databases.Title.SDEZ;
using Aminos.Services.Injections.Attrbutes;
using Aminos.Models.General.Tables;
using Aminos.Models.Title.SDEZ.Requests;
using Aminos.Models.Title.SDEZ.Responses;
using Aminos.Models.Title.SDEZ.Tables;
using Aminos.Utils.MethodExtensions;
using Microsoft.EntityFrameworkCore;

namespace Aminos.Handlers.Title.SDEZ
{
	[RegisterInjectable(typeof(MaimaiDXUserAllHandler))]
	public class MaimaiDXUserAllHandler
	{
		private readonly ILogger<MaimaiDXUserAllHandler> logger;
		private readonly MaimaiDXDB maimaiDxDB;
		private readonly AminosDB aminosDB;

		public MaimaiDXUserAllHandler(ILogger<MaimaiDXUserAllHandler> logger, MaimaiDXDB maimaiDxDB, AminosDB aminosDB)
		{
			this.logger = logger;
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
					returnCode = 1,
					apiName = nameof(MaimaiDXUserAllHandler)
				};
			}

			userData.Id = request.userId;
			if ((await maimaiDxDB.UserDetails.FirstOrDefaultAsync(x => x.Id == request.userId)) is UserDetail storedUserDetail)
			{
				maimaiDxDB.CopyValuesWithoutKeys(storedUserDetail, userData);
				userData = storedUserDetail;
			}
			else
			{
				await maimaiDxDB.UserDetails.AddAsync(userData);
			}
			userData.isNetMember = 1;

			#endregion

			#region General Card

			var accessCode = userData.accessCode;
			if (!await aminosDB.Cards.AnyAsync(x => x.Luid == accessCode))
			{
				var rand = new Random();
				var extId = rand.Next(99999999).ToString();
				while (await aminosDB.Cards.AnyAsync(x => x.ExtId == extId))
					extId = rand.Next(99999999).ToString();

				var card = new Card()
				{
					Luid = accessCode,
					RegisterTime = DateTime.Now,
					AccessTime = DateTime.Now,
					ExtId = extId,
				};

				await aminosDB.Cards.AddAsync(card);
				await aminosDB.SaveChangesAsync();
			}

			#endregion

			#region UserExtend

			var userExtend = request.upsertUserAll.userExtend.FirstOrDefault();
			if (userExtend != null)
			{
				if (userData.UserExtend != null)
				{
					userExtend.Id = userData.UserExtend.Id;
					maimaiDxDB.CopyValuesWithoutKeys(userData.UserExtend, userExtend);
				}
				else
					userData.UserExtend = userExtend;
			}

			#endregion

			#region UserOption

			var userOption = request.upsertUserAll.userOption?.FirstOrDefault();
			if (userOption != null)
			{
				if (userData.UserOption != null)
				{
					userOption.Id = userData.UserOption.Id;
					maimaiDxDB.CopyValuesWithoutKeys(userData.UserOption, userOption);
				}
				else
					userData.UserOption = userOption;
			}

			#endregion

			ValueTask SimpleRepalceEntites<T>(IEnumerable<T> inList, ICollection<T> existList, Func<T, T, bool> childCmp) where T : class
			{
				if (inList != null)
				{
					foreach (var inEntity in inList)
					{
						if (existList.FirstOrDefault(x => childCmp(x, inEntity)) is T storedEntity)
						{
							maimaiDxDB.CopyValuesWithoutKeys(storedEntity, inEntity);
						}
						else
						{
							existList.Add(inEntity);
						}
					}
				}
				return ValueTask.CompletedTask;
			}

			#region UserCharacterList

			await SimpleRepalceEntites(
				request.upsertUserAll.userCharacterList,
				userData.UserCharacters,
				(a, b) => a.characterId == b.characterId);

			#endregion

			#region UserGhostList

			//todo UserGhostList

			#endregion

			#region UserMapList

			await SimpleRepalceEntites(
				request.upsertUserAll.userMapList,
				userData.UserMaps,
				(a, b) => a.mapId == b.mapId);

			#endregion

			#region UserLoginBonusList

			await SimpleRepalceEntites(
				request.upsertUserAll.userLoginBonusList,
				userData.UserLoginBonuses,
				(a, b) => a.bonusId == b.bonusId);

			#endregion

			#region UserRatingList

			var userRating = request.upsertUserAll.userRatingList?.FirstOrDefault();
			if (userRating != null)
			{
				if (userData.UserRating != null)
				{
					maimaiDxDB.CopyValuesWithoutKeys(userData.UserRating, userRating);

					void process(IEnumerable<UserRate> inRates, ICollection<UserRate> storedRates)
					{
						foreach (var inRate in inRates)
						{
							if (storedRates.FirstOrDefault(x => x.musicId == inRate.musicId && x.level == inRate.level) is
								UserRate storedRate)
							{
								maimaiDxDB.CopyValuesWithoutKeys(storedRate, inRate);
							}
							else
								storedRates.Add(inRate);
						}
					}

					process(userRating.ratingList, userData.UserRating.ratingList);
					process(userRating.newRatingList, userData.UserRating.newRatingList);
					process(userRating.nextNewRatingList, userData.UserRating.nextNewRatingList);
					process(userRating.nextRatingList, userData.UserRating.nextRatingList);

					maimaiDxDB.CopyValuesWithoutKeys(userData.UserRating.udemae, userRating.udemae);
				}
				else
					userData.UserRating = userRating;
			}

			#endregion

			#region UserItemList

			await SimpleRepalceEntites(
				request.upsertUserAll.userItemList,
				userData.UserItems,
				(a, b) => a.itemId == b.itemId && a.itemKind == b.itemKind);

			#endregion

			#region UserMusicDetailList

			await SimpleRepalceEntites(
				request.upsertUserAll.userMusicDetailList,
				userData.UserMusicDetails,
				(a, b) => a.musicId == b.musicId && a.level == b.level);

			#endregion

			#region UserCourseList

			await SimpleRepalceEntites(
				request.upsertUserAll.userCourseList,
				userData.UserCourses,
				(a, b) => a.courseId == b.courseId);

			#endregion

			#region UserFriendSeasonRankingList

			await SimpleRepalceEntites(
				request.upsertUserAll.userFriendSeasonRankingList,
				userData.UserFriendSeasonRankings,
				(a, b) => a.seasonId == b.seasonId);

			#endregion

			#region UserFavoriteList

			await SimpleRepalceEntites(
				request.upsertUserAll.userFavoriteList,
				userData.UserFavorites,
				(a, b) => a.Id == b.Id);

			#endregion

			#region UserActivityList

			var userActivity = request.upsertUserAll.userActivityList?.FirstOrDefault();
			if (userActivity != null)
			{
				if (userData.UserActivity != null)
				{
					maimaiDxDB.CopyValuesWithoutKeys(userData.UserActivity, userActivity);

					foreach (var inAct in userActivity.musicList.ToArray())
					{
						if (userData.UserActivity.musicList.FirstOrDefault(x => x.id == inAct.id && x.kind == inAct.kind) is UserAct storedAct)
						{
							maimaiDxDB.CopyValuesWithoutKeys(storedAct, inAct);
						}
						else
							userData.UserActivity.musicList.Add(inAct);
					}

					foreach (var inAct in userActivity.playList.ToArray())
					{
						if (userData.UserActivity.playList.FirstOrDefault(x => x.id == inAct.id && x.kind == inAct.kind) is UserAct storedAct)
						{
							maimaiDxDB.CopyValuesWithoutKeys(storedAct, inAct);
						}
						else
							userData.UserActivity.playList.Add(inAct);
					}
				}
				else
				{
					userData.UserActivity = userActivity;
				}
			}

			#endregion

			#region User2pPlaylog

			var user2pPlaylog = request.upsertUserAll.user2pPlaylog;
			if (user2pPlaylog != null)
			{
				if (user2pPlaylog.User2pPlaylogDetails?.Count > 0)
				{
					var userData1 = await maimaiDxDB.UserDetails.FirstOrDefaultAsync(x => x.Id == user2pPlaylog.userId1);
					var userData2 = await maimaiDxDB.UserDetails.FirstOrDefaultAsync(x => x.Id == user2pPlaylog.userId2);

					if (userData1.Id > userData2.Id)
						(userData1, userData2) = (userData2, userData1);

					foreach (var upsertDetail in user2pPlaylog.User2pPlaylogDetails)
					{
						upsertDetail.UserDetail1 = userData1;
						upsertDetail.UserDetail2 = userData2;

						if ((await maimaiDxDB.User2pPlaylogDetails.FirstOrDefaultAsync(x =>
							((x.UserDetail2 == upsertDetail.UserDetail2 && x.UserDetail1 == upsertDetail.UserDetail1)
							|| (x.UserDetail1 == upsertDetail.UserDetail2 && x.UserDetail2 == upsertDetail.UserDetail1))
							&& x.level == upsertDetail.level
							&& x.musicId == upsertDetail.musicId
						)) is User2pPlaylogDetail storedDetail)
						{
							maimaiDxDB.CopyValuesWithoutKeys(storedDetail, upsertDetail);
						}
						else
						{
							await maimaiDxDB.User2pPlaylogDetails.AddAsync(upsertDetail);
						}
					}
				}
			}

			#endregion

			#region UserGamePlaylogList

			await SimpleRepalceEntites(
				request.upsertUserAll.userGamePlaylogList,
				userData.UserGamePlaylogs,
				(a, b) => a.playlogId == b.playlogId);

			#endregion

			await maimaiDxDB.SaveChangesAsync();
			return new()
			{
				returnCode = 1,
				apiName = nameof(MaimaiDXUserAllHandler)
			};
		}
	}
}
