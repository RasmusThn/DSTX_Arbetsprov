using Entities;
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
        Task<string> CreateTimeReport(TimeReport timeReport);
        Task<List<TimeReport>> GetAllTimeReports();
        Task<List<Workplace>> GetAllWorkplacesAsync();
    }
}
