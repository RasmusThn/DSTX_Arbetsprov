using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Entities
{
    public class TimeReport
    {
        public int Id { get; set; }

        [JsonPropertyName("workplace_id")]
        [RegularExpression(@"^\d+$", ErrorMessage = "The WorkplaceId field must be an integer.")]
        public int WorkplaceId { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("hours")]
        [Display(Name = "Hours")]
        [Range(-1, 24, ErrorMessage = "The Hours field must be in the range 0 - 24.")]
        public float Hours { get; set; }

        [JsonPropertyName("info")]
        [Display(Name = "Info")]
        public string? Info { get; set; }
    }
}
