using Aminos.Core.Models.Title.SDEZ.Tables;

namespace Aminos.Core.Models.Title.SDEZ.Responses;

public class AllCollectionData
{
    public IconData[] IconDatas { get; set; }
    public FrameData[] FrameDatas { get; set; }
    public TitleData[] TitleDatas { get; set; }
    public PlateData[] PlateDatas { get; set; }
}