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
		[JsonIgnore]
		public ulong UserDetailId { get; set; }

		[Key]
		[JsonIgnore]
		public uint Id { get; set; }

		public OptionKindID optionKind { get; set; }

		public OptionNotespeedID noteSpeed { get; set; }

		public OptionSlidespeedID slideSpeed { get; set; }

		public OptionTouchspeedID touchSpeed { get; set; }

		public OptionGametapID tapDesign { get; set; }

		public OptionGameholdID holdDesign { get; set; }

		public OptionGameslideID slideDesign { get; set; }

		public OptionStartypeID starType { get; set; }

		public OptionGameoutlineID outlineDesign { get; set; }

		public OptionNotesizeID noteSize { get; set; }

		public OptionSlidesizeID slideSize { get; set; }

		public OptionTouchsizeID touchSize { get; set; }

		public OptionStarrotateID starRotate { get; set; }

		public OptionCenterdisplayID dispCenter { get; set; }

		public OptionOutframedisplayID outFrameType { get; set; }

		public OptionDispchainID dispChain { get; set; }

		public OptionDisprateID dispRate { get; set; }

		public OptionDispbarlineID dispBar { get; set; }

		public OptionToucheffectID touchEffect { get; set; }

		public OptionSubmonitorID submonitorAnimation { get; set; }

		public OptionSubmonAchiveID submonitorAchive { get; set; }

		public OptionAppealID submonitorAppeal { get; set; }

		public OptionMatchingID matching { get; set; }

		public OptionTrackskipID trackSkip { get; set; }

		public OptionMoviebrightnessID brightness { get; set; }

		public OptionMirrorID mirrorMode { get; set; }

		public OptionDispjudgeID dispJudge { get; set; }

		public OptionDispjudgeposID dispJudgePos { get; set; }

		public OptionDispjudgetouchposID dispJudgeTouchPos { get; set; }

		public OptionJudgetimingID adjustTiming { get; set; }

		public OptionJudgetimingID judgeTiming { get; set; }

		public OptionVolumeAnswerSoundID ansVolume { get; set; }

		public OptionVolumeID tapHoldVolume { get; set; }

		public OptionCriticalID criticalSe { get; set; }

		public OptionTapSuccessSeID tapSe { get; set; }

		public OptionBreakseID breakSe { get; set; }

		public OptionVolumeID breakVolume { get; set; }

		public OptionExseID exSe { get; set; }

		public OptionVolumeID exVolume { get; set; }

		public OptionSlideseID slideSe { get; set; }

		public OptionVolumeID slideVolume { get; set; }

		public OptionVolumeID breakSlideVolume { get; set; }

		public OptionVolumeID touchVolume { get; set; }

		public OptionVolumeID touchHoldVolume { get; set; }

		public OptionVolumeID damageSeVolume { get; set; }

		public OptionHeadphonevolumeID headPhoneVolume { get; set; }

		public SortTabID sortTab { get; set; }

		public SortMusicID sortMusic { get; set; }
	}
}