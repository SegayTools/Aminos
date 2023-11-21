using Aminos.Models.Title.SDEZ.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Models.Title.SDEZ.Tables
{
    [Index(nameof(Id))]
    [Table("MaimaiDX_UserOptions")]
    public class UserOption
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        public OptionKindID optionKind;

        public OptionNotespeedID noteSpeed;

        public OptionSlidespeedID slideSpeed;

        public OptionTouchspeedID touchSpeed;

        public OptionGametapID tapDesign;

        public OptionGameholdID holdDesign;

        public OptionGameslideID slideDesign;

        public OptionStartypeID starType;

        public OptionGameoutlineID outlineDesign;

        public OptionNotesizeID noteSize;

        public OptionSlidesizeID slideSize;

        public OptionTouchsizeID touchSize;

        public OptionStarrotateID starRotate;

        public OptionCenterdisplayID dispCenter;

        public OptionOutframedisplayID outFrameType;

        public OptionDispchainID dispChain;

        public OptionDisprateID dispRate;

        public OptionDispbarlineID dispBar;

        public OptionToucheffectID touchEffect;

        public OptionSubmonitorID submonitorAnimation;

        public OptionSubmonAchiveID submonitorAchive;

        public OptionAppealID submonitorAppeal;

        public OptionMatchingID matching;

        public OptionTrackskipID trackSkip;

        public OptionMoviebrightnessID brightness;

        public OptionMirrorID mirrorMode;

        public OptionDispjudgeID dispJudge;

        public OptionDispjudgeposID dispJudgePos;

        public OptionDispjudgetouchposID dispJudgeTouchPos;

        public OptionJudgetimingID adjustTiming;

        public OptionJudgetimingID judgeTiming;

        public OptionVolumeAnswerSoundID ansVolume;

        public OptionVolumeID tapHoldVolume;

        public OptionCriticalID criticalSe;

        public OptionTapSuccessSeID tapSe;

        public OptionBreakseID breakSe;

        public OptionVolumeID breakVolume;

        public OptionExseID exSe;

        public OptionVolumeID exVolume;

        public OptionSlideseID slideSe;

        public OptionVolumeID slideVolume;

        public OptionVolumeID breakSlideVolume;

        public OptionVolumeID touchVolume;

        public OptionVolumeID touchHoldVolume;

        public OptionVolumeID damageSeVolume;

        public OptionHeadphonevolumeID headPhoneVolume;

        public SortTabID sortTab;

        public SortMusicID sortMusic;
    }
}