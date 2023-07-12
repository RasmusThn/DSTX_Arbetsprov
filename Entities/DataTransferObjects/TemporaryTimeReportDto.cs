using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
    public class TemporaryTimeReportDto
    {
        public string Id { get; set; }
        public int WorkplaceId { get; set; }
        public string Date { get; set; }
        public string Hours { get; set; }
        public string Info { get; set; }
    }
}
