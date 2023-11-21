using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aminos.Migrations.MaimaiDXDBMigrations
{
	/// <inheritdoc />
	public partial class V2_Add_SDEZ_Tables : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "MaimaiDX_GameCharges",
				columns: table => new
				{
					Id = table.Column<ulong>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					orderId = table.Column<int>(type: "INTEGER", nullable: false),
					chargeId = table.Column<int>(type: "INTEGER", nullable: false),
					price = table.Column<int>(type: "INTEGER", nullable: false),
					startDate = table.Column<string>(type: "TEXT", nullable: true),
					endDate = table.Column<string>(type: "TEXT", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_MaimaiDX_GameCharges", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "MaimaiDX_GameEvents",
				columns: table => new
				{
					Id = table.Column<ulong>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					type = table.Column<int>(type: "INTEGER", nullable: false),
					startDate = table.Column<string>(type: "TEXT", nullable: true),
					endDate = table.Column<string>(type: "TEXT", nullable: true),
					enable = table.Column<bool>(type: "INTEGER", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_MaimaiDX_GameEvents", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "MaimaiDX_GameRankings",
				columns: table => new
				{
					id = table.Column<long>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					point = table.Column<long>(type: "INTEGER", nullable: false),
					userName = table.Column<string>(type: "TEXT", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_MaimaiDX_GameRankings", x => x.id);
				});

			migrationBuilder.CreateTable(
				name: "MaimaiDX_UserActivities",
				columns: table => new
				{
					Id = table.Column<ulong>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_MaimaiDX_UserActivities", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "MaimaiDX_UserExtends",
				columns: table => new
				{
					Id = table.Column<ulong>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					selectMusicId = table.Column<int>(type: "INTEGER", nullable: false),
					selectDifficultyId = table.Column<int>(type: "INTEGER", nullable: false),
					categoryIndex = table.Column<int>(type: "INTEGER", nullable: false),
					musicIndex = table.Column<int>(type: "INTEGER", nullable: false),
					extraFlag = table.Column<int>(type: "INTEGER", nullable: false),
					selectScoreType = table.Column<int>(type: "INTEGER", nullable: false),
					extendContentBit = table.Column<ulong>(type: "INTEGER", nullable: false),
					isPhotoAgree = table.Column<bool>(type: "INTEGER", nullable: false),
					isGotoCodeRead = table.Column<bool>(type: "INTEGER", nullable: false),
					selectResultDetails = table.Column<bool>(type: "INTEGER", nullable: false),
					selectResultScoreViewType = table.Column<int>(type: "INTEGER", nullable: false),
					sortCategorySetting = table.Column<int>(type: "INTEGER", nullable: false),
					sortMusicSetting = table.Column<int>(type: "INTEGER", nullable: false),
					playStatusSetting = table.Column<int>(type: "INTEGER", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_MaimaiDX_UserExtends", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "MaimaiDX_UserOptions",
				columns: table => new
				{
					Id = table.Column<uint>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					optionKind = table.Column<int>(type: "INTEGER", nullable: false),
					noteSpeed = table.Column<int>(type: "INTEGER", nullable: false),
					slideSpeed = table.Column<int>(type: "INTEGER", nullable: false),
					touchSpeed = table.Column<int>(type: "INTEGER", nullable: false),
					tapDesign = table.Column<int>(type: "INTEGER", nullable: false),
					holdDesign = table.Column<int>(type: "INTEGER", nullable: false),
					slideDesign = table.Column<int>(type: "INTEGER", nullable: false),
					starType = table.Column<int>(type: "INTEGER", nullable: false),
					outlineDesign = table.Column<int>(type: "INTEGER", nullable: false),
					noteSize = table.Column<int>(type: "INTEGER", nullable: false),
					slideSize = table.Column<int>(type: "INTEGER", nullable: false),
					touchSize = table.Column<int>(type: "INTEGER", nullable: false),
					starRotate = table.Column<int>(type: "INTEGER", nullable: false),
					dispCenter = table.Column<int>(type: "INTEGER", nullable: false),
					outFrameType = table.Column<int>(type: "INTEGER", nullable: false),
					dispChain = table.Column<int>(type: "INTEGER", nullable: false),
					dispRate = table.Column<int>(type: "INTEGER", nullable: false),
					dispBar = table.Column<int>(type: "INTEGER", nullable: false),
					touchEffect = table.Column<int>(type: "INTEGER", nullable: false),
					submonitorAnimation = table.Column<int>(type: "INTEGER", nullable: false),
					submonitorAchive = table.Column<int>(type: "INTEGER", nullable: false),
					submonitorAppeal = table.Column<int>(type: "INTEGER", nullable: false),
					matching = table.Column<int>(type: "INTEGER", nullable: false),
					trackSkip = table.Column<int>(type: "INTEGER", nullable: false),
					brightness = table.Column<int>(type: "INTEGER", nullable: false),
					mirrorMode = table.Column<int>(type: "INTEGER", nullable: false),
					dispJudge = table.Column<int>(type: "INTEGER", nullable: false),
					dispJudgePos = table.Column<int>(type: "INTEGER", nullable: false),
					dispJudgeTouchPos = table.Column<int>(type: "INTEGER", nullable: false),
					adjustTiming = table.Column<int>(type: "INTEGER", nullable: false),
					judgeTiming = table.Column<int>(type: "INTEGER", nullable: false),
					ansVolume = table.Column<int>(type: "INTEGER", nullable: false),
					tapHoldVolume = table.Column<int>(type: "INTEGER", nullable: false),
					criticalSe = table.Column<int>(type: "INTEGER", nullable: false),
					tapSe = table.Column<int>(type: "INTEGER", nullable: false),
					breakSe = table.Column<int>(type: "INTEGER", nullable: false),
					breakVolume = table.Column<int>(type: "INTEGER", nullable: false),
					exSe = table.Column<int>(type: "INTEGER", nullable: false),
					exVolume = table.Column<int>(type: "INTEGER", nullable: false),
					slideSe = table.Column<int>(type: "INTEGER", nullable: false),
					slideVolume = table.Column<int>(type: "INTEGER", nullable: false),
					breakSlideVolume = table.Column<int>(type: "INTEGER", nullable: false),
					touchVolume = table.Column<int>(type: "INTEGER", nullable: false),
					touchHoldVolume = table.Column<int>(type: "INTEGER", nullable: false),
					damageSeVolume = table.Column<int>(type: "INTEGER", nullable: false),
					headPhoneVolume = table.Column<int>(type: "INTEGER", nullable: false),
					sortTab = table.Column<int>(type: "INTEGER", nullable: false),
					sortMusic = table.Column<int>(type: "INTEGER", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_MaimaiDX_UserOptions", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "MaimaiDX_UserUdemaes",
				columns: table => new
				{
					Id = table.Column<ulong>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					rate = table.Column<int>(type: "INTEGER", nullable: false),
					maxRate = table.Column<int>(type: "INTEGER", nullable: false),
					classValue = table.Column<int>(type: "INTEGER", nullable: false),
					maxClassValue = table.Column<int>(type: "INTEGER", nullable: false),
					totalWinNum = table.Column<uint>(type: "INTEGER", nullable: false),
					totalLoseNum = table.Column<uint>(type: "INTEGER", nullable: false),
					maxWinNum = table.Column<uint>(type: "INTEGER", nullable: false),
					maxLoseNum = table.Column<uint>(type: "INTEGER", nullable: false),
					winNum = table.Column<uint>(type: "INTEGER", nullable: false),
					loseNum = table.Column<uint>(type: "INTEGER", nullable: false),
					npcTotalWinNum = table.Column<uint>(type: "INTEGER", nullable: false),
					npcTotalLoseNum = table.Column<uint>(type: "INTEGER", nullable: false),
					npcMaxWinNum = table.Column<uint>(type: "INTEGER", nullable: false),
					npcMaxLoseNum = table.Column<uint>(type: "INTEGER", nullable: false),
					npcWinNum = table.Column<uint>(type: "INTEGER", nullable: false),
					npcLoseNum = table.Column<uint>(type: "INTEGER", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_MaimaiDX_UserUdemaes", x => x.Id);
				});

			migrationBuilder.CreateTable(
				name: "MaimaiDX_UserActs",
				columns: table => new
				{
					Id = table.Column<ulong>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					kind = table.Column<int>(type: "INTEGER", nullable: false),
					sortNumber = table.Column<long>(type: "INTEGER", nullable: false),
					param1 = table.Column<int>(type: "INTEGER", nullable: false),
					param2 = table.Column<int>(type: "INTEGER", nullable: false),
					param3 = table.Column<int>(type: "INTEGER", nullable: false),
					param4 = table.Column<int>(type: "INTEGER", nullable: false),
					userId = table.Column<int>(type: "INTEGER", nullable: false),
					UserActivityId = table.Column<ulong>(type: "INTEGER", nullable: true),
					UserActivityId1 = table.Column<ulong>(type: "INTEGER", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_MaimaiDX_UserActs", x => x.Id);
					table.ForeignKey(
						name: "FK_MaimaiDX_UserActs_MaimaiDX_UserActivities_UserActivityId",
						column: x => x.UserActivityId,
						principalTable: "MaimaiDX_UserActivities",
						principalColumn: "Id");
					table.ForeignKey(
						name: "FK_MaimaiDX_UserActs_MaimaiDX_UserActivities_UserActivityId1",
						column: x => x.UserActivityId1,
						principalTable: "MaimaiDX_UserActivities",
						principalColumn: "Id");
				});

			migrationBuilder.CreateTable(
				name: "MaimaiDX_UserRatings",
				columns: table => new
				{
					Id = table.Column<ulong>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					rating = table.Column<int>(type: "INTEGER", nullable: false),
					udemaeId = table.Column<ulong>(type: "INTEGER", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_MaimaiDX_UserRatings", x => x.Id);
					table.ForeignKey(
						name: "FK_MaimaiDX_UserRatings_MaimaiDX_UserUdemaes_udemaeId",
						column: x => x.udemaeId,
						principalTable: "MaimaiDX_UserUdemaes",
						principalColumn: "Id");
				});

			migrationBuilder.CreateTable(
				name: "MaimaiDX_UserDetails",
				columns: table => new
				{
					Id = table.Column<ulong>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					UserOptionId = table.Column<uint>(type: "INTEGER", nullable: true),
					UserRatingId = table.Column<ulong>(type: "INTEGER", nullable: true),
					UserExtendId = table.Column<ulong>(type: "INTEGER", nullable: true),
					UserActivityId = table.Column<ulong>(type: "INTEGER", nullable: true),
					accessCode = table.Column<string>(type: "TEXT", nullable: true),
					userName = table.Column<string>(type: "TEXT", nullable: true),
					isNetMember = table.Column<int>(type: "INTEGER", nullable: false),
					iconId = table.Column<int>(type: "INTEGER", nullable: false),
					plateId = table.Column<int>(type: "INTEGER", nullable: false),
					titleId = table.Column<int>(type: "INTEGER", nullable: false),
					partnerId = table.Column<int>(type: "INTEGER", nullable: false),
					frameId = table.Column<int>(type: "INTEGER", nullable: false),
					selectMapId = table.Column<int>(type: "INTEGER", nullable: false),
					totalAwake = table.Column<int>(type: "INTEGER", nullable: false),
					gradeRating = table.Column<int>(type: "INTEGER", nullable: false),
					musicRating = table.Column<int>(type: "INTEGER", nullable: false),
					playerRating = table.Column<int>(type: "INTEGER", nullable: false),
					highestRating = table.Column<int>(type: "INTEGER", nullable: false),
					gradeRank = table.Column<int>(type: "INTEGER", nullable: false),
					classRank = table.Column<int>(type: "INTEGER", nullable: false),
					courseRank = table.Column<int>(type: "INTEGER", nullable: false),
					contentBit = table.Column<ulong>(type: "INTEGER", nullable: false),
					playCount = table.Column<int>(type: "INTEGER", nullable: false),
					currentPlayCount = table.Column<int>(type: "INTEGER", nullable: false),
					renameCredit = table.Column<int>(type: "INTEGER", nullable: false),
					mapStock = table.Column<int>(type: "INTEGER", nullable: false),
					eventWatchedDate = table.Column<string>(type: "TEXT", nullable: true),
					lastGameId = table.Column<string>(type: "TEXT", nullable: true),
					lastRomVersion = table.Column<string>(type: "TEXT", nullable: true),
					lastDataVersion = table.Column<string>(type: "TEXT", nullable: true),
					lastLoginDate = table.Column<string>(type: "TEXT", nullable: true),
					lastPlayDate = table.Column<string>(type: "TEXT", nullable: true),
					lastPlayCredit = table.Column<int>(type: "INTEGER", nullable: false),
					lastPlayMode = table.Column<int>(type: "INTEGER", nullable: false),
					lastPlaceId = table.Column<int>(type: "INTEGER", nullable: false),
					lastPlaceName = table.Column<string>(type: "TEXT", nullable: true),
					lastAllNetId = table.Column<int>(type: "INTEGER", nullable: false),
					lastRegionId = table.Column<int>(type: "INTEGER", nullable: false),
					lastRegionName = table.Column<string>(type: "TEXT", nullable: true),
					lastClientId = table.Column<string>(type: "TEXT", nullable: true),
					lastCountryCode = table.Column<string>(type: "TEXT", nullable: true),
					lastSelectEMoney = table.Column<int>(type: "INTEGER", nullable: false),
					lastSelectTicket = table.Column<int>(type: "INTEGER", nullable: false),
					lastSelectCourse = table.Column<int>(type: "INTEGER", nullable: false),
					lastCountCourse = table.Column<int>(type: "INTEGER", nullable: false),
					firstGameId = table.Column<string>(type: "TEXT", nullable: true),
					firstRomVersion = table.Column<string>(type: "TEXT", nullable: true),
					firstDataVersion = table.Column<string>(type: "TEXT", nullable: true),
					firstPlayDate = table.Column<string>(type: "TEXT", nullable: true),
					compatibleCmVersion = table.Column<string>(type: "TEXT", nullable: true),
					dailyBonusDate = table.Column<string>(type: "TEXT", nullable: true),
					dailyCourseBonusDate = table.Column<string>(type: "TEXT", nullable: true),
					lastPairLoginDate = table.Column<string>(type: "TEXT", nullable: true),
					lastTrialPlayDate = table.Column<string>(type: "TEXT", nullable: true),
					playVsCount = table.Column<int>(type: "INTEGER", nullable: false),
					playSyncCount = table.Column<int>(type: "INTEGER", nullable: false),
					winCount = table.Column<int>(type: "INTEGER", nullable: false),
					helpCount = table.Column<int>(type: "INTEGER", nullable: false),
					comboCount = table.Column<int>(type: "INTEGER", nullable: false),
					totalDeluxscore = table.Column<long>(type: "INTEGER", nullable: false),
					totalBasicDeluxscore = table.Column<long>(type: "INTEGER", nullable: false),
					totalAdvancedDeluxscore = table.Column<long>(type: "INTEGER", nullable: false),
					totalExpertDeluxscore = table.Column<long>(type: "INTEGER", nullable: false),
					totalMasterDeluxscore = table.Column<long>(type: "INTEGER", nullable: false),
					totalReMasterDeluxscore = table.Column<long>(type: "INTEGER", nullable: false),
					totalSync = table.Column<int>(type: "INTEGER", nullable: false),
					totalBasicSync = table.Column<int>(type: "INTEGER", nullable: false),
					totalAdvancedSync = table.Column<int>(type: "INTEGER", nullable: false),
					totalExpertSync = table.Column<int>(type: "INTEGER", nullable: false),
					totalMasterSync = table.Column<int>(type: "INTEGER", nullable: false),
					totalReMasterSync = table.Column<int>(type: "INTEGER", nullable: false),
					totalAchievement = table.Column<long>(type: "INTEGER", nullable: false),
					totalBasicAchievement = table.Column<long>(type: "INTEGER", nullable: false),
					totalAdvancedAchievement = table.Column<long>(type: "INTEGER", nullable: false),
					totalExpertAchievement = table.Column<long>(type: "INTEGER", nullable: false),
					totalMasterAchievement = table.Column<long>(type: "INTEGER", nullable: false),
					totalReMasterAchievement = table.Column<long>(type: "INTEGER", nullable: false),
					playerOldRating = table.Column<long>(type: "INTEGER", nullable: false),
					playerNewRating = table.Column<long>(type: "INTEGER", nullable: false),
					banState = table.Column<int>(type: "INTEGER", nullable: false),
					dateTime = table.Column<long>(type: "INTEGER", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_MaimaiDX_UserDetails", x => x.Id);
					table.ForeignKey(
						name: "FK_MaimaiDX_UserDetails_MaimaiDX_UserActivities_UserActivityId",
						column: x => x.UserActivityId,
						principalTable: "MaimaiDX_UserActivities",
						principalColumn: "Id");
					table.ForeignKey(
						name: "FK_MaimaiDX_UserDetails_MaimaiDX_UserExtends_UserExtendId",
						column: x => x.UserExtendId,
						principalTable: "MaimaiDX_UserExtends",
						principalColumn: "Id");
					table.ForeignKey(
						name: "FK_MaimaiDX_UserDetails_MaimaiDX_UserOptions_UserOptionId",
						column: x => x.UserOptionId,
						principalTable: "MaimaiDX_UserOptions",
						principalColumn: "Id");
					table.ForeignKey(
						name: "FK_MaimaiDX_UserDetails_MaimaiDX_UserRatings_UserRatingId",
						column: x => x.UserRatingId,
						principalTable: "MaimaiDX_UserRatings",
						principalColumn: "Id");
				});

			migrationBuilder.CreateTable(
				name: "MaimaiDX_UserRates",
				columns: table => new
				{
					Id = table.Column<ulong>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					musicId = table.Column<int>(type: "INTEGER", nullable: false),
					level = table.Column<int>(type: "INTEGER", nullable: false),
					romVersion = table.Column<uint>(type: "INTEGER", nullable: false),
					achievement = table.Column<uint>(type: "INTEGER", nullable: false),
					UserRatingId = table.Column<ulong>(type: "INTEGER", nullable: true),
					UserRatingId1 = table.Column<ulong>(type: "INTEGER", nullable: true),
					UserRatingId2 = table.Column<ulong>(type: "INTEGER", nullable: true),
					UserRatingId3 = table.Column<ulong>(type: "INTEGER", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_MaimaiDX_UserRates", x => x.Id);
					table.ForeignKey(
						name: "FK_MaimaiDX_UserRates_MaimaiDX_UserRatings_UserRatingId",
						column: x => x.UserRatingId,
						principalTable: "MaimaiDX_UserRatings",
						principalColumn: "Id");
					table.ForeignKey(
						name: "FK_MaimaiDX_UserRates_MaimaiDX_UserRatings_UserRatingId1",
						column: x => x.UserRatingId1,
						principalTable: "MaimaiDX_UserRatings",
						principalColumn: "Id");
					table.ForeignKey(
						name: "FK_MaimaiDX_UserRates_MaimaiDX_UserRatings_UserRatingId2",
						column: x => x.UserRatingId2,
						principalTable: "MaimaiDX_UserRatings",
						principalColumn: "Id");
					table.ForeignKey(
						name: "FK_MaimaiDX_UserRates_MaimaiDX_UserRatings_UserRatingId3",
						column: x => x.UserRatingId3,
						principalTable: "MaimaiDX_UserRatings",
						principalColumn: "Id");
				});

			migrationBuilder.CreateTable(
				name: "MaimaiDX_User2pPlaylogDetails",
				columns: table => new
				{
					Id = table.Column<int>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					UserDetail1Id = table.Column<ulong>(type: "INTEGER", nullable: true),
					UserDetail2Id = table.Column<ulong>(type: "INTEGER", nullable: true),
					musicId = table.Column<int>(type: "INTEGER", nullable: false),
					level = table.Column<int>(type: "INTEGER", nullable: false),
					achievement = table.Column<int>(type: "INTEGER", nullable: false),
					deluxscore = table.Column<int>(type: "INTEGER", nullable: false),
					userPlayDate = table.Column<string>(type: "TEXT", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_MaimaiDX_User2pPlaylogDetails", x => x.Id);
					table.ForeignKey(
						name: "FK_MaimaiDX_User2pPlaylogDetails_MaimaiDX_UserDetails_UserDetail1Id",
						column: x => x.UserDetail1Id,
						principalTable: "MaimaiDX_UserDetails",
						principalColumn: "Id");
					table.ForeignKey(
						name: "FK_MaimaiDX_User2pPlaylogDetails_MaimaiDX_UserDetails_UserDetail2Id",
						column: x => x.UserDetail2Id,
						principalTable: "MaimaiDX_UserDetails",
						principalColumn: "Id");
				});

			migrationBuilder.CreateTable(
				name: "MaimaiDX_UserCards",
				columns: table => new
				{
					Id = table.Column<ulong>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					cardId = table.Column<int>(type: "INTEGER", nullable: false),
					cardTypeId = table.Column<int>(type: "INTEGER", nullable: false),
					charaId = table.Column<int>(type: "INTEGER", nullable: false),
					mapId = table.Column<int>(type: "INTEGER", nullable: false),
					startDate = table.Column<string>(type: "TEXT", nullable: true),
					endDate = table.Column<string>(type: "TEXT", nullable: true),
					UserDetailId = table.Column<ulong>(type: "INTEGER", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_MaimaiDX_UserCards", x => x.Id);
					table.ForeignKey(
						name: "FK_MaimaiDX_UserCards_MaimaiDX_UserDetails_UserDetailId",
						column: x => x.UserDetailId,
						principalTable: "MaimaiDX_UserDetails",
						principalColumn: "Id");
				});

			migrationBuilder.CreateTable(
				name: "MaimaiDX_UserCharacters",
				columns: table => new
				{
					Id = table.Column<ulong>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					characterId = table.Column<int>(type: "INTEGER", nullable: false),
					level = table.Column<uint>(type: "INTEGER", nullable: false),
					awakening = table.Column<uint>(type: "INTEGER", nullable: false),
					useCount = table.Column<uint>(type: "INTEGER", nullable: false),
					UserDetailId = table.Column<ulong>(type: "INTEGER", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_MaimaiDX_UserCharacters", x => x.Id);
					table.ForeignKey(
						name: "FK_MaimaiDX_UserCharacters_MaimaiDX_UserDetails_UserDetailId",
						column: x => x.UserDetailId,
						principalTable: "MaimaiDX_UserDetails",
						principalColumn: "Id");
				});

			migrationBuilder.CreateTable(
				name: "MaimaiDX_UserChargelogs",
				columns: table => new
				{
					Id = table.Column<ulong>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					chargeId = table.Column<int>(type: "INTEGER", nullable: false),
					price = table.Column<int>(type: "INTEGER", nullable: false),
					purchaseDate = table.Column<string>(type: "TEXT", nullable: true),
					playCount = table.Column<int>(type: "INTEGER", nullable: false),
					playerRating = table.Column<int>(type: "INTEGER", nullable: false),
					placeId = table.Column<int>(type: "INTEGER", nullable: false),
					regionId = table.Column<int>(type: "INTEGER", nullable: false),
					clientId = table.Column<string>(type: "TEXT", nullable: true),
					UserDetailId = table.Column<ulong>(type: "INTEGER", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_MaimaiDX_UserChargelogs", x => x.Id);
					table.ForeignKey(
						name: "FK_MaimaiDX_UserChargelogs_MaimaiDX_UserDetails_UserDetailId",
						column: x => x.UserDetailId,
						principalTable: "MaimaiDX_UserDetails",
						principalColumn: "Id");
				});

			migrationBuilder.CreateTable(
				name: "MaimaiDX_UserCharges",
				columns: table => new
				{
					Id = table.Column<ulong>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					chargeId = table.Column<int>(type: "INTEGER", nullable: false),
					stock = table.Column<int>(type: "INTEGER", nullable: false),
					purchaseDate = table.Column<string>(type: "TEXT", nullable: true),
					validDate = table.Column<string>(type: "TEXT", nullable: true),
					UserDetailId = table.Column<ulong>(type: "INTEGER", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_MaimaiDX_UserCharges", x => x.Id);
					table.ForeignKey(
						name: "FK_MaimaiDX_UserCharges_MaimaiDX_UserDetails_UserDetailId",
						column: x => x.UserDetailId,
						principalTable: "MaimaiDX_UserDetails",
						principalColumn: "Id");
				});

			migrationBuilder.CreateTable(
				name: "MaimaiDX_UserCourses",
				columns: table => new
				{
					Id = table.Column<ulong>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					courseId = table.Column<int>(type: "INTEGER", nullable: false),
					isLastClear = table.Column<bool>(type: "INTEGER", nullable: false),
					totalRestlife = table.Column<uint>(type: "INTEGER", nullable: false),
					totalAchievement = table.Column<uint>(type: "INTEGER", nullable: false),
					totalDeluxscore = table.Column<uint>(type: "INTEGER", nullable: false),
					playCount = table.Column<uint>(type: "INTEGER", nullable: false),
					clearDate = table.Column<string>(type: "TEXT", nullable: true),
					lastPlayDate = table.Column<string>(type: "TEXT", nullable: true),
					bestAchievement = table.Column<uint>(type: "INTEGER", nullable: false),
					bestAchievementDate = table.Column<string>(type: "TEXT", nullable: true),
					bestDeluxscore = table.Column<uint>(type: "INTEGER", nullable: false),
					bestDeluxscoreDate = table.Column<string>(type: "TEXT", nullable: true),
					UserDetailId = table.Column<ulong>(type: "INTEGER", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_MaimaiDX_UserCourses", x => x.Id);
					table.ForeignKey(
						name: "FK_MaimaiDX_UserCourses_MaimaiDX_UserDetails_UserDetailId",
						column: x => x.UserDetailId,
						principalTable: "MaimaiDX_UserDetails",
						principalColumn: "Id");
				});

			migrationBuilder.CreateTable(
				name: "MaimaiDX_UserFavoriteItems",
				columns: table => new
				{
					UserFavoriteItemId = table.Column<int>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					kind = table.Column<int>(type: "INTEGER", nullable: false),
					id = table.Column<ulong>(type: "INTEGER", nullable: false),
					UserDetailId = table.Column<ulong>(type: "INTEGER", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_MaimaiDX_UserFavoriteItems", x => x.UserFavoriteItemId);
					table.ForeignKey(
						name: "FK_MaimaiDX_UserFavoriteItems_MaimaiDX_UserDetails_UserDetailId",
						column: x => x.UserDetailId,
						principalTable: "MaimaiDX_UserDetails",
						principalColumn: "Id");
				});

			migrationBuilder.CreateTable(
				name: "MaimaiDX_UserFavorites",
				columns: table => new
				{
					Id = table.Column<ulong>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					userId = table.Column<ulong>(type: "INTEGER", nullable: false),
					itemKind = table.Column<int>(type: "INTEGER", nullable: false),
					itemId = table.Column<string>(type: "TEXT", nullable: true),
					UserDetailId = table.Column<ulong>(type: "INTEGER", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_MaimaiDX_UserFavorites", x => x.Id);
					table.ForeignKey(
						name: "FK_MaimaiDX_UserFavorites_MaimaiDX_UserDetails_UserDetailId",
						column: x => x.UserDetailId,
						principalTable: "MaimaiDX_UserDetails",
						principalColumn: "Id");
				});

			migrationBuilder.CreateTable(
				name: "MaimaiDX_UserFriendSeasonRankings",
				columns: table => new
				{
					Id = table.Column<ulong>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					UserDetailId = table.Column<ulong>(type: "INTEGER", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_MaimaiDX_UserFriendSeasonRankings", x => x.Id);
					table.ForeignKey(
						name: "FK_MaimaiDX_UserFriendSeasonRankings_MaimaiDX_UserDetails_UserDetailId",
						column: x => x.UserDetailId,
						principalTable: "MaimaiDX_UserDetails",
						principalColumn: "Id");
				});

			migrationBuilder.CreateTable(
				name: "MaimaiDX_UserGamePlaylogs",
				columns: table => new
				{
					Id = table.Column<ulong>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					version = table.Column<string>(type: "TEXT", nullable: true),
					playDate = table.Column<string>(type: "TEXT", nullable: true),
					playMode = table.Column<int>(type: "INTEGER", nullable: false),
					useTicketId = table.Column<int>(type: "INTEGER", nullable: false),
					playCredit = table.Column<int>(type: "INTEGER", nullable: false),
					playTrack = table.Column<int>(type: "INTEGER", nullable: false),
					clientId = table.Column<string>(type: "TEXT", nullable: true),
					isPlayTutorial = table.Column<bool>(type: "INTEGER", nullable: false),
					isEventMode = table.Column<bool>(type: "INTEGER", nullable: false),
					isNewFree = table.Column<bool>(type: "INTEGER", nullable: false),
					playCount = table.Column<int>(type: "INTEGER", nullable: false),
					playSpecial = table.Column<int>(type: "INTEGER", nullable: false),
					playOtherUserId = table.Column<ulong>(type: "INTEGER", nullable: false),
					UserDetailId = table.Column<ulong>(type: "INTEGER", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_MaimaiDX_UserGamePlaylogs", x => x.Id);
					table.ForeignKey(
						name: "FK_MaimaiDX_UserGamePlaylogs_MaimaiDX_UserDetails_UserDetailId",
						column: x => x.UserDetailId,
						principalTable: "MaimaiDX_UserDetails",
						principalColumn: "Id");
				});

			migrationBuilder.CreateTable(
				name: "MaimaiDX_UserItems",
				columns: table => new
				{
					Id = table.Column<ulong>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					itemKind = table.Column<int>(type: "INTEGER", nullable: false),
					itemId = table.Column<int>(type: "INTEGER", nullable: false),
					stock = table.Column<int>(type: "INTEGER", nullable: false),
					isValid = table.Column<bool>(type: "INTEGER", nullable: false),
					UserDetailId = table.Column<ulong>(type: "INTEGER", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_MaimaiDX_UserItems", x => x.Id);
					table.ForeignKey(
						name: "FK_MaimaiDX_UserItems_MaimaiDX_UserDetails_UserDetailId",
						column: x => x.UserDetailId,
						principalTable: "MaimaiDX_UserDetails",
						principalColumn: "Id");
				});

			migrationBuilder.CreateTable(
				name: "MaimaiDX_UserLoginBonuses",
				columns: table => new
				{
					Id = table.Column<ulong>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					bonusId = table.Column<int>(type: "INTEGER", nullable: false),
					point = table.Column<uint>(type: "INTEGER", nullable: false),
					isCurrent = table.Column<bool>(type: "INTEGER", nullable: false),
					isComplete = table.Column<bool>(type: "INTEGER", nullable: false),
					UserDetailId = table.Column<ulong>(type: "INTEGER", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_MaimaiDX_UserLoginBonuses", x => x.Id);
					table.ForeignKey(
						name: "FK_MaimaiDX_UserLoginBonuses_MaimaiDX_UserDetails_UserDetailId",
						column: x => x.UserDetailId,
						principalTable: "MaimaiDX_UserDetails",
						principalColumn: "Id");
				});

			migrationBuilder.CreateTable(
				name: "MaimaiDX_UserMaps",
				columns: table => new
				{
					Id = table.Column<ulong>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					mapId = table.Column<int>(type: "INTEGER", nullable: false),
					distance = table.Column<uint>(type: "INTEGER", nullable: false),
					isLock = table.Column<bool>(type: "INTEGER", nullable: false),
					isClear = table.Column<bool>(type: "INTEGER", nullable: false),
					isComplete = table.Column<bool>(type: "INTEGER", nullable: false),
					UserDetailId = table.Column<ulong>(type: "INTEGER", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_MaimaiDX_UserMaps", x => x.Id);
					table.ForeignKey(
						name: "FK_MaimaiDX_UserMaps_MaimaiDX_UserDetails_UserDetailId",
						column: x => x.UserDetailId,
						principalTable: "MaimaiDX_UserDetails",
						principalColumn: "Id");
				});

			migrationBuilder.CreateTable(
				name: "MaimaiDX_UserMusicDetails",
				columns: table => new
				{
					Id = table.Column<ulong>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					musicId = table.Column<int>(type: "INTEGER", nullable: false),
					level = table.Column<int>(type: "INTEGER", nullable: false),
					playCount = table.Column<uint>(type: "INTEGER", nullable: false),
					achievement = table.Column<uint>(type: "INTEGER", nullable: false),
					comboStatus = table.Column<int>(type: "INTEGER", nullable: false),
					syncStatus = table.Column<int>(type: "INTEGER", nullable: false),
					deluxscoreMax = table.Column<uint>(type: "INTEGER", nullable: false),
					scoreRank = table.Column<int>(type: "INTEGER", nullable: false),
					extNum1 = table.Column<uint>(type: "INTEGER", nullable: false),
					UserDetailId = table.Column<ulong>(type: "INTEGER", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_MaimaiDX_UserMusicDetails", x => x.Id);
					table.ForeignKey(
						name: "FK_MaimaiDX_UserMusicDetails_MaimaiDX_UserDetails_UserDetailId",
						column: x => x.UserDetailId,
						principalTable: "MaimaiDX_UserDetails",
						principalColumn: "Id");
				});

			migrationBuilder.CreateTable(
				name: "MaimaiDX_UserPlaylogs",
				columns: table => new
				{
					Id = table.Column<ulong>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					userId = table.Column<ulong>(type: "INTEGER", nullable: false),
					orderId = table.Column<int>(type: "INTEGER", nullable: false),
					playlogId = table.Column<ulong>(type: "INTEGER", nullable: false),
					version = table.Column<int>(type: "INTEGER", nullable: false),
					placeId = table.Column<int>(type: "INTEGER", nullable: false),
					placeName = table.Column<string>(type: "TEXT", nullable: true),
					loginDate = table.Column<long>(type: "INTEGER", nullable: false),
					playDate = table.Column<string>(type: "TEXT", nullable: true),
					userPlayDate = table.Column<string>(type: "TEXT", nullable: true),
					type = table.Column<int>(type: "INTEGER", nullable: false),
					musicId = table.Column<int>(type: "INTEGER", nullable: false),
					level = table.Column<int>(type: "INTEGER", nullable: false),
					trackNo = table.Column<int>(type: "INTEGER", nullable: false),
					vsMode = table.Column<int>(type: "INTEGER", nullable: false),
					vsUserName = table.Column<string>(type: "TEXT", nullable: true),
					vsStatus = table.Column<int>(type: "INTEGER", nullable: false),
					vsUserRating = table.Column<int>(type: "INTEGER", nullable: false),
					vsUserAchievement = table.Column<int>(type: "INTEGER", nullable: false),
					vsUserGradeRank = table.Column<int>(type: "INTEGER", nullable: false),
					vsRank = table.Column<int>(type: "INTEGER", nullable: false),
					playerNum = table.Column<int>(type: "INTEGER", nullable: false),
					playedUserId1 = table.Column<ulong>(type: "INTEGER", nullable: false),
					playedUserName1 = table.Column<string>(type: "TEXT", nullable: true),
					playedMusicLevel1 = table.Column<int>(type: "INTEGER", nullable: false),
					playedUserId2 = table.Column<ulong>(type: "INTEGER", nullable: false),
					playedUserName2 = table.Column<string>(type: "TEXT", nullable: true),
					playedMusicLevel2 = table.Column<int>(type: "INTEGER", nullable: false),
					playedUserId3 = table.Column<ulong>(type: "INTEGER", nullable: false),
					playedUserName3 = table.Column<string>(type: "TEXT", nullable: true),
					playedMusicLevel3 = table.Column<int>(type: "INTEGER", nullable: false),
					characterId1 = table.Column<int>(type: "INTEGER", nullable: false),
					characterLevel1 = table.Column<int>(type: "INTEGER", nullable: false),
					characterAwakening1 = table.Column<int>(type: "INTEGER", nullable: false),
					characterId2 = table.Column<int>(type: "INTEGER", nullable: false),
					characterLevel2 = table.Column<int>(type: "INTEGER", nullable: false),
					characterAwakening2 = table.Column<int>(type: "INTEGER", nullable: false),
					characterId3 = table.Column<int>(type: "INTEGER", nullable: false),
					characterLevel3 = table.Column<int>(type: "INTEGER", nullable: false),
					characterAwakening3 = table.Column<int>(type: "INTEGER", nullable: false),
					characterId4 = table.Column<int>(type: "INTEGER", nullable: false),
					characterLevel4 = table.Column<int>(type: "INTEGER", nullable: false),
					characterAwakening4 = table.Column<int>(type: "INTEGER", nullable: false),
					characterId5 = table.Column<int>(type: "INTEGER", nullable: false),
					characterLevel5 = table.Column<int>(type: "INTEGER", nullable: false),
					characterAwakening5 = table.Column<int>(type: "INTEGER", nullable: false),
					achievement = table.Column<int>(type: "INTEGER", nullable: false),
					deluxscore = table.Column<int>(type: "INTEGER", nullable: false),
					scoreRank = table.Column<int>(type: "INTEGER", nullable: false),
					maxCombo = table.Column<int>(type: "INTEGER", nullable: false),
					totalCombo = table.Column<int>(type: "INTEGER", nullable: false),
					maxSync = table.Column<int>(type: "INTEGER", nullable: false),
					totalSync = table.Column<int>(type: "INTEGER", nullable: false),
					tapCriticalPerfect = table.Column<int>(type: "INTEGER", nullable: false),
					tapPerfect = table.Column<int>(type: "INTEGER", nullable: false),
					tapGreat = table.Column<int>(type: "INTEGER", nullable: false),
					tapGood = table.Column<int>(type: "INTEGER", nullable: false),
					tapMiss = table.Column<int>(type: "INTEGER", nullable: false),
					holdCriticalPerfect = table.Column<int>(type: "INTEGER", nullable: false),
					holdPerfect = table.Column<int>(type: "INTEGER", nullable: false),
					holdGreat = table.Column<int>(type: "INTEGER", nullable: false),
					holdGood = table.Column<int>(type: "INTEGER", nullable: false),
					holdMiss = table.Column<int>(type: "INTEGER", nullable: false),
					slideCriticalPerfect = table.Column<int>(type: "INTEGER", nullable: false),
					slidePerfect = table.Column<int>(type: "INTEGER", nullable: false),
					slideGreat = table.Column<int>(type: "INTEGER", nullable: false),
					slideGood = table.Column<int>(type: "INTEGER", nullable: false),
					slideMiss = table.Column<int>(type: "INTEGER", nullable: false),
					touchCriticalPerfect = table.Column<int>(type: "INTEGER", nullable: false),
					touchPerfect = table.Column<int>(type: "INTEGER", nullable: false),
					touchGreat = table.Column<int>(type: "INTEGER", nullable: false),
					touchGood = table.Column<int>(type: "INTEGER", nullable: false),
					touchMiss = table.Column<int>(type: "INTEGER", nullable: false),
					breakCriticalPerfect = table.Column<int>(type: "INTEGER", nullable: false),
					breakPerfect = table.Column<int>(type: "INTEGER", nullable: false),
					breakGreat = table.Column<int>(type: "INTEGER", nullable: false),
					breakGood = table.Column<int>(type: "INTEGER", nullable: false),
					breakMiss = table.Column<int>(type: "INTEGER", nullable: false),
					isTap = table.Column<bool>(type: "INTEGER", nullable: false),
					isHold = table.Column<bool>(type: "INTEGER", nullable: false),
					isSlide = table.Column<bool>(type: "INTEGER", nullable: false),
					isTouch = table.Column<bool>(type: "INTEGER", nullable: false),
					isBreak = table.Column<bool>(type: "INTEGER", nullable: false),
					isCriticalDisp = table.Column<bool>(type: "INTEGER", nullable: false),
					isFastLateDisp = table.Column<bool>(type: "INTEGER", nullable: false),
					fastCount = table.Column<int>(type: "INTEGER", nullable: false),
					lateCount = table.Column<int>(type: "INTEGER", nullable: false),
					isAchieveNewRecord = table.Column<bool>(type: "INTEGER", nullable: false),
					isDeluxscoreNewRecord = table.Column<bool>(type: "INTEGER", nullable: false),
					comboStatus = table.Column<int>(type: "INTEGER", nullable: false),
					syncStatus = table.Column<int>(type: "INTEGER", nullable: false),
					isClear = table.Column<bool>(type: "INTEGER", nullable: false),
					beforeRating = table.Column<int>(type: "INTEGER", nullable: false),
					afterRating = table.Column<int>(type: "INTEGER", nullable: false),
					beforeGrade = table.Column<int>(type: "INTEGER", nullable: false),
					afterGrade = table.Column<int>(type: "INTEGER", nullable: false),
					afterGradeRank = table.Column<int>(type: "INTEGER", nullable: false),
					beforeDeluxRating = table.Column<int>(type: "INTEGER", nullable: false),
					afterDeluxRating = table.Column<int>(type: "INTEGER", nullable: false),
					isPlayTutorial = table.Column<bool>(type: "INTEGER", nullable: false),
					isEventMode = table.Column<bool>(type: "INTEGER", nullable: false),
					isFreedomMode = table.Column<bool>(type: "INTEGER", nullable: false),
					playMode = table.Column<int>(type: "INTEGER", nullable: false),
					isNewFree = table.Column<bool>(type: "INTEGER", nullable: false),
					trialPlayAchievement = table.Column<int>(type: "INTEGER", nullable: false),
					extNum1 = table.Column<int>(type: "INTEGER", nullable: false),
					extNum2 = table.Column<int>(type: "INTEGER", nullable: false),
					extNum4 = table.Column<int>(type: "INTEGER", nullable: false),
					extBool1 = table.Column<bool>(type: "INTEGER", nullable: false),
					UserDetailId = table.Column<ulong>(type: "INTEGER", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_MaimaiDX_UserPlaylogs", x => x.Id);
					table.ForeignKey(
						name: "FK_MaimaiDX_UserPlaylogs_MaimaiDX_UserDetails_UserDetailId",
						column: x => x.UserDetailId,
						principalTable: "MaimaiDX_UserDetails",
						principalColumn: "Id");
				});

			migrationBuilder.CreateTable(
				name: "MaimaiDX_UserRegions",
				columns: table => new
				{
					Id = table.Column<ulong>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					regionId = table.Column<int>(type: "INTEGER", nullable: false),
					playCount = table.Column<int>(type: "INTEGER", nullable: false),
					created = table.Column<string>(type: "TEXT", nullable: true),
					UserDetailId = table.Column<ulong>(type: "INTEGER", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_MaimaiDX_UserRegions", x => x.Id);
					table.ForeignKey(
						name: "FK_MaimaiDX_UserRegions_MaimaiDX_UserDetails_UserDetailId",
						column: x => x.UserDetailId,
						principalTable: "MaimaiDX_UserDetails",
						principalColumn: "Id");
				});

			migrationBuilder.CreateTable(
				name: "MaimaiDX_UserScoreRankings",
				columns: table => new
				{
					Id = table.Column<ulong>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					tournamentId = table.Column<int>(type: "INTEGER", nullable: false),
					totalScore = table.Column<long>(type: "INTEGER", nullable: false),
					ranking = table.Column<int>(type: "INTEGER", nullable: false),
					rankingDate = table.Column<string>(type: "TEXT", nullable: true),
					UserDetailId = table.Column<ulong>(type: "INTEGER", nullable: true)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_MaimaiDX_UserScoreRankings", x => x.Id);
					table.ForeignKey(
						name: "FK_MaimaiDX_UserScoreRankings_MaimaiDX_UserDetails_UserDetailId",
						column: x => x.UserDetailId,
						principalTable: "MaimaiDX_UserDetails",
						principalColumn: "Id");
				});

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_GameCharges_Id",
				table: "MaimaiDX_GameCharges",
				column: "Id");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_GameEvents_Id",
				table: "MaimaiDX_GameEvents",
				column: "Id");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_GameRankings_id",
				table: "MaimaiDX_GameRankings",
				column: "id");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_User2pPlaylogDetails_Id",
				table: "MaimaiDX_User2pPlaylogDetails",
				column: "Id");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_User2pPlaylogDetails_UserDetail1Id",
				table: "MaimaiDX_User2pPlaylogDetails",
				column: "UserDetail1Id");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_User2pPlaylogDetails_UserDetail2Id",
				table: "MaimaiDX_User2pPlaylogDetails",
				column: "UserDetail2Id");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserActivities_Id",
				table: "MaimaiDX_UserActivities",
				column: "Id");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserActs_Id",
				table: "MaimaiDX_UserActs",
				column: "Id");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserActs_UserActivityId",
				table: "MaimaiDX_UserActs",
				column: "UserActivityId");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserActs_UserActivityId1",
				table: "MaimaiDX_UserActs",
				column: "UserActivityId1");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserCards_Id",
				table: "MaimaiDX_UserCards",
				column: "Id");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserCards_UserDetailId",
				table: "MaimaiDX_UserCards",
				column: "UserDetailId");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserCharacters_Id",
				table: "MaimaiDX_UserCharacters",
				column: "Id");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserCharacters_UserDetailId",
				table: "MaimaiDX_UserCharacters",
				column: "UserDetailId");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserChargelogs_Id",
				table: "MaimaiDX_UserChargelogs",
				column: "Id");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserChargelogs_UserDetailId",
				table: "MaimaiDX_UserChargelogs",
				column: "UserDetailId");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserCharges_Id",
				table: "MaimaiDX_UserCharges",
				column: "Id");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserCharges_UserDetailId",
				table: "MaimaiDX_UserCharges",
				column: "UserDetailId");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserCourses_Id",
				table: "MaimaiDX_UserCourses",
				column: "Id");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserCourses_UserDetailId",
				table: "MaimaiDX_UserCourses",
				column: "UserDetailId");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserDetails_Id",
				table: "MaimaiDX_UserDetails",
				column: "Id");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserDetails_UserActivityId",
				table: "MaimaiDX_UserDetails",
				column: "UserActivityId");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserDetails_UserExtendId",
				table: "MaimaiDX_UserDetails",
				column: "UserExtendId");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserDetails_UserOptionId",
				table: "MaimaiDX_UserDetails",
				column: "UserOptionId");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserDetails_UserRatingId",
				table: "MaimaiDX_UserDetails",
				column: "UserRatingId");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserExtends_Id",
				table: "MaimaiDX_UserExtends",
				column: "Id");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserFavoriteItems_UserDetailId",
				table: "MaimaiDX_UserFavoriteItems",
				column: "UserDetailId");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserFavoriteItems_UserFavoriteItemId",
				table: "MaimaiDX_UserFavoriteItems",
				column: "UserFavoriteItemId");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserFavorites_Id",
				table: "MaimaiDX_UserFavorites",
				column: "Id");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserFavorites_UserDetailId",
				table: "MaimaiDX_UserFavorites",
				column: "UserDetailId");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserFriendSeasonRankings_Id",
				table: "MaimaiDX_UserFriendSeasonRankings",
				column: "Id");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserFriendSeasonRankings_UserDetailId",
				table: "MaimaiDX_UserFriendSeasonRankings",
				column: "UserDetailId");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserGamePlaylogs_Id",
				table: "MaimaiDX_UserGamePlaylogs",
				column: "Id");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserGamePlaylogs_UserDetailId",
				table: "MaimaiDX_UserGamePlaylogs",
				column: "UserDetailId");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserItems_Id",
				table: "MaimaiDX_UserItems",
				column: "Id");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserItems_UserDetailId",
				table: "MaimaiDX_UserItems",
				column: "UserDetailId");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserLoginBonuses_Id",
				table: "MaimaiDX_UserLoginBonuses",
				column: "Id");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserLoginBonuses_UserDetailId",
				table: "MaimaiDX_UserLoginBonuses",
				column: "UserDetailId");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserMaps_Id",
				table: "MaimaiDX_UserMaps",
				column: "Id");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserMaps_UserDetailId",
				table: "MaimaiDX_UserMaps",
				column: "UserDetailId");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserMusicDetails_Id",
				table: "MaimaiDX_UserMusicDetails",
				column: "Id");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserMusicDetails_UserDetailId",
				table: "MaimaiDX_UserMusicDetails",
				column: "UserDetailId");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserOptions_Id",
				table: "MaimaiDX_UserOptions",
				column: "Id");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserPlaylogs_Id",
				table: "MaimaiDX_UserPlaylogs",
				column: "Id");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserPlaylogs_UserDetailId",
				table: "MaimaiDX_UserPlaylogs",
				column: "UserDetailId");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserRates_Id",
				table: "MaimaiDX_UserRates",
				column: "Id");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserRates_UserRatingId",
				table: "MaimaiDX_UserRates",
				column: "UserRatingId");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserRates_UserRatingId1",
				table: "MaimaiDX_UserRates",
				column: "UserRatingId1");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserRates_UserRatingId2",
				table: "MaimaiDX_UserRates",
				column: "UserRatingId2");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserRates_UserRatingId3",
				table: "MaimaiDX_UserRates",
				column: "UserRatingId3");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserRatings_Id",
				table: "MaimaiDX_UserRatings",
				column: "Id");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserRatings_udemaeId",
				table: "MaimaiDX_UserRatings",
				column: "udemaeId");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserRegions_Id",
				table: "MaimaiDX_UserRegions",
				column: "Id");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserRegions_UserDetailId",
				table: "MaimaiDX_UserRegions",
				column: "UserDetailId");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserScoreRankings_Id",
				table: "MaimaiDX_UserScoreRankings",
				column: "Id");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserScoreRankings_UserDetailId",
				table: "MaimaiDX_UserScoreRankings",
				column: "UserDetailId");

			migrationBuilder.CreateIndex(
				name: "IX_MaimaiDX_UserUdemaes_Id",
				table: "MaimaiDX_UserUdemaes",
				column: "Id");
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "MaimaiDX_GameCharges");

			migrationBuilder.DropTable(
				name: "MaimaiDX_GameEvents");

			migrationBuilder.DropTable(
				name: "MaimaiDX_GameRankings");

			migrationBuilder.DropTable(
				name: "MaimaiDX_User2pPlaylogDetails");

			migrationBuilder.DropTable(
				name: "MaimaiDX_UserActs");

			migrationBuilder.DropTable(
				name: "MaimaiDX_UserCards");

			migrationBuilder.DropTable(
				name: "MaimaiDX_UserCharacters");

			migrationBuilder.DropTable(
				name: "MaimaiDX_UserChargelogs");

			migrationBuilder.DropTable(
				name: "MaimaiDX_UserCharges");

			migrationBuilder.DropTable(
				name: "MaimaiDX_UserCourses");

			migrationBuilder.DropTable(
				name: "MaimaiDX_UserFavoriteItems");

			migrationBuilder.DropTable(
				name: "MaimaiDX_UserFavorites");

			migrationBuilder.DropTable(
				name: "MaimaiDX_UserFriendSeasonRankings");

			migrationBuilder.DropTable(
				name: "MaimaiDX_UserGamePlaylogs");

			migrationBuilder.DropTable(
				name: "MaimaiDX_UserItems");

			migrationBuilder.DropTable(
				name: "MaimaiDX_UserLoginBonuses");

			migrationBuilder.DropTable(
				name: "MaimaiDX_UserMaps");

			migrationBuilder.DropTable(
				name: "MaimaiDX_UserMusicDetails");

			migrationBuilder.DropTable(
				name: "MaimaiDX_UserPlaylogs");

			migrationBuilder.DropTable(
				name: "MaimaiDX_UserRates");

			migrationBuilder.DropTable(
				name: "MaimaiDX_UserRegions");

			migrationBuilder.DropTable(
				name: "MaimaiDX_UserScoreRankings");

			migrationBuilder.DropTable(
				name: "MaimaiDX_UserDetails");

			migrationBuilder.DropTable(
				name: "MaimaiDX_UserActivities");

			migrationBuilder.DropTable(
				name: "MaimaiDX_UserExtends");

			migrationBuilder.DropTable(
				name: "MaimaiDX_UserOptions");

			migrationBuilder.DropTable(
				name: "MaimaiDX_UserRatings");

			migrationBuilder.DropTable(
				name: "MaimaiDX_UserUdemaes");
		}
	}
}
