using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class TimeReport
    {
        public int Id { get; set; }
        public int WorkplaceId { get; set; }
        public DateTime Date { get; set; }
        public float Hours { get; set; }
        public string? Info { get; set; }
    }
}
