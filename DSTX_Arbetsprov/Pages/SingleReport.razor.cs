using Entities.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceContracts;

namespace PresentationBlazor.Pages
{
    public partial class SingleReport
    {
        [Inject]
        private ITimeReportService _timeReportService { get; set; }
        private int _reportID = 0;
        private TimeReport? _timeReport;
        private bool _error = false;
        private string _errorMessage = "";

        private async Task GetSingleReportAsync()
        {
            if (_reportID != 0)
            {
                try
                {
                    _timeReport = null;
                    _error = false;
                    _timeReport = await _timeReportService.GetSingleTimeReportAsync(_reportID);
                }
                catch (Exception ex)
                {
                    _error = true;
                    _timeReport = null;
                    _errorMessage = "An error occurred while retrieving time report: " + ex.Message;
                }
            }
            else
            {
                _timeReport = null;
            }

        }
    }
}
