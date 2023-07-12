using Entities;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace PresentationBlazor.Controllers
{
    [ApiController]
    [Route("api/")]
    public class TimeReportController : ControllerBase
    {
        private readonly IControllerService _controllerService;

        public TimeReportController(IControllerService controllerService)
        {
            _controllerService = controllerService;
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateTimeReport()
        {
            try
            {
                var form = await Request.ReadFormAsync();

                var result = await _controllerService.CreateTimeReport(form);

                if (result != null)
                {
                    return Ok(result);
                }
                else
                {
                    return BadRequest(new { success = false });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, new { error = "An unexpected error occurred." });
            }
        }
    }
}
