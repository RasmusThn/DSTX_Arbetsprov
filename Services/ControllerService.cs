using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using ServiceContracts;

namespace Services
{
    public class ControllerService : IControllerService
    {
        private readonly ITimeReportService _timeReportService;
        private readonly IDataAccessService _dataAccessService;

        public ControllerService(ITimeReportService timeReportService,IDataAccessService dataAccessService)
        {

            _timeReportService = timeReportService;
            _dataAccessService = dataAccessService;

        }

        public async Task<TransferTimeReportDto> CreateTimeReportAsync(IFormCollection form)
        {
            TransferTimeReportDto formattedTimeReport = new TransferTimeReportDto
            {
                WorkplaceId = Convert.ToInt32(form["workplaceId"]),
                Date = form["date"],
                Hours = form["hours"],
                Info = form["info"]
            };

            int id;

            id = await _timeReportService.PostTimeReportAsync(formattedTimeReport); 

            //id = 1337;  // UnComment this when testing and comment out line above to skip posting to API 

            formattedTimeReport.Id = id;
            
            //Saving file, filename and Id to Database
            var file = form.Files["image"];
            if (file != null && file.Length > 0)
            {
                _dataAccessService.SaveFileToDB(file,id);
            }
            return formattedTimeReport;
        }
    }
}
