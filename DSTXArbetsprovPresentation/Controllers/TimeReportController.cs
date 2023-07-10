using Microsoft.AspNetCore.Mvc;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/v1/")]
    [ApiController]
    public class TimeReportController : ControllerBase
    {
        private TimeReportService _timeReportService;
        
        public TimeReportController(TimeReportService service)
        {
            _timeReportService = service;            
        }

        [HttpGet]
        public async Task<IActionResult> GetSingleTimeReport()
        {
         
           var response =  await _timeReportService.GetSingleTimeReport();

            return Ok(response);
        }

    }
}
