using Entities.DataTransferObjects;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    public interface ITimeReportService
    {
        Task<TimeReport> GetSingleTimeReportAsync(int reportId);
        Task<int> PostTimeReportAsync(TransferTimeReportDto timeReport);
        Task<List<TimeReport>> GetAllTimeReportsAsync();
        Task<List<TimeReport>> GetAllTimeReportsByIdAndDateAsync(DateTime fromDate, DateTime toDate, int id);
        Task<List<Workplace>> GetAllWorkplacesAsync();
    }
}
