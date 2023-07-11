using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class TransferTimeReportDto
    {
        public int Id { get; set; }

        [JsonPropertyName("workplace_id")]
        public int WorkplaceId { get; set; }

        [JsonPropertyName("date")]
        public string Date { get; set; }

        [JsonPropertyName("hours")]
        public string Hours { get; set; } = "0";

        [JsonPropertyName("info")]
        public string? Info { get; set; }
    }
}
