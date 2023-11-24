using Aminos.Utils.Json;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Aminos.Models.Title.SDEZ.Tables
{
    [Table("Client_Bookkeepings")]
    [Index(nameof(Id))]
    public class ClientBookkeeping
    {
        [Key]
        [JsonPropertyName("clientId")]
        public string Id { get; set; }

        public int placeId { get; set; }

        [JsonConverter(typeof(TitleString2DateTimeConverter))]
        public DateTime updateDate { get; set; }

        public int creditSetting0 { get; set; }

        public int creditSetting1 { get; set; }

        public int creditSetting2 { get; set; }

        public int credits1P { get; set; }

        public int credits2P { get; set; }

        public int creditsFreedom { get; set; }

        public int creditsTicket { get; set; }

        public int creditCoin { get; set; }

        public int creditService { get; set; }

        public int creditEmoney { get; set; }

        public int timeTotal { get; set; }

        public int timeTotalPlay { get; set; }

        public int timeLongest1P { get; set; }

        public int timeShortest1P { get; set; }

        public int timeLongest2P { get; set; }

        public int timeShortest2P { get; set; }

        public int timeLongestFreedom { get; set; }

        public int timeShortestFreedom { get; set; }

        public int newFreeUserNum { get; set; }

        public int tutorialPlay { get; set; }

        public int play1PNum { get; set; }

        public int play2PNum { get; set; }

        public int playFreedomNum { get; set; }

        public int aimeLoginNum { get; set; }

        public int guestLoginNum { get; set; }

        public int regionId { get; set; }

        public int playCount { get; set; }

        public int coinToCredit { get; set; }
    }
}