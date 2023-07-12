using Entities;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace PresentationBlazor.Controllers
{
    [ApiController]
    [Route("api/")]
    public class TimeReportController : ControllerBase
    {
        private readonly ITimeReportService _timeReportService;

        public TimeReportController(ITimeReportService timeReportService)
        {
            _timeReportService = timeReportService;
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateTimeReport()
        {
            try
            {


                var form = await Request.ReadFormAsync();

                // Extract the values from the form
                var workplaceId = Convert.ToInt32(form["workplaceId"]);
                var date = DateTime.Parse(form["date"]);
                var hours = Convert.ToInt32(form["hours"]);
                var info = form["info"].ToString();

                // Process the file upload
                var file = form.Files["image"];
                if (file != null && file.Length > 0)
                {
                    // You can save the file to disk or process it in any other way
                    // For demonstration purposes, let's just retrieve the file name
                    var fileName = file.FileName;

                    // Implement your logic to create a time report using the extracted values
                    // and the uploaded file information

                    return Ok(new { success = true, image = true });
                }
                else
                {
                    return Ok(new { success = true, image = false });

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(new { success = false });
            }
        }
    }
}
