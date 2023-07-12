using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ControllerService : IControllerService
    {
        private readonly ITimeReportService _timeReportService;


        public ControllerService(ITimeReportService timeReportService)
        {

            _timeReportService = timeReportService;

        }

        public async Task<TransferTimeReportDto> CreateTimeReport(IFormCollection form)
        {

            TransferTimeReportDto formattedTimeReport = new TransferTimeReportDto
            {
                WorkplaceId = Convert.ToInt32(form["workplaceId"]),
                Date = form["date"],
                Hours = form["hours"],
                Info = form["info"]
            };

            int id;
            id = await _timeReportService.PostTimeReport(formattedTimeReport);
           
            formattedTimeReport.Id = id;

            //Saving file and Id to Database
            var file = form.Files["image"];
            if (file != null && file.Length > 0)
            {
                // You can save the file to disk or process it in any other way
                // For demonstration purposes, let's just retrieve the file name
                var fileName = file.FileName;
                // Implement your logic to create a time report using the extracted values
                // and the uploaded file information

            }
            return formattedTimeReport;
        }
    }
}
