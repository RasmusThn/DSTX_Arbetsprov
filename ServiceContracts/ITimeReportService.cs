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
        Task<TimeReport> GetSingleTimeReport(int reportId);
        Task<int> PostTimeReport(TransferTimeReportDto timeReport);
        Task<List<TimeReport>> GetAllTimeReports();
        Task<List<TimeReport>> GetAllTimeReportsByIdAndDate(DateTime fromDate, DateTime toDate, int id);
        Task<List<Workplace>> GetAllWorkplacesAsync();
    }
}
