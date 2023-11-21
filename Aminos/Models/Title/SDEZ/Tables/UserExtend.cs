using Aminos.Models.Title.SDEZ.Enums;
using Aminos.Models.Title.SDEZ.Responses;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Aminos.Models.Title.SDEZ.Tables
{
    [Index(nameof(Id))]
    [Table("MaimaiDX_UserExtends")]
    public class UserExtend
    {
        [Key]
        public int Id { get; set; }

        public int selectMusicId;

        public int selectDifficultyId;

        public int categoryIndex;

        public int musicIndex;

        public int extraFlag;

        public int selectScoreType;

        public ulong extendContentBit;

        public bool isPhotoAgree;

        public bool isGotoCodeRead;

        public bool selectResultDetails;

        public int selectResultScoreViewType;

        public SortTabID sortCategorySetting;

        public SortMusicID sortMusicSetting;

        public PlaystatusTabID playStatusSetting;

        public int[] selectedCardList;

        public MapEncountNpc[] encountMapNpcList;
    }
}