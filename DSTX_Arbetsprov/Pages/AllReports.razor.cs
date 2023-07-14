using Entities.Models;
using Microsoft.AspNetCore.Components;
using ServiceContracts;

namespace PresentationBlazor.Pages
{
    public partial class AllReports
    {
        [Inject]
        private ITimeReportService _timeReportService { get; set; }
        private List<TimeReport> timeReportList;
        private List<Workplace> workplaces;
        private int selectedId = 0;
        private DateTime[] dates = new DateTime[2];
        private DateTime dateTimeNow = DateTime.Now;
        private bool _error = false;
        private string _errorMessage = "";

        private bool sortByDateAscending = true;
        private bool sortByTemperatureCAscending = true;
        private bool sortByTemperatureFAscending = true;
        private bool sortBySummaryAscending = true;


        protected override async Task OnInitializedAsync()
        {
            dates[0] = new DateTime(2017, 10, 20);
            dates[1] = dateTimeNow;
            try
            {
                _error = false;
                workplaces = await _timeReportService.GetAllWorkplacesAsync();
            }
            catch (Exception ex)
            {
                _error = true;
                _errorMessage = "An error occurred while retrieving time reports: " + ex.Message + "Try refresh the page.";
            }
        }
        private async Task SetSelectedId(ChangeEventArgs e)
        {
            selectedId = Convert.ToInt32(e.Value);

        }
        private async Task GetAllTimeReportsById()
        {
            try
            {
                _error = false;
                timeReportList = await _timeReportService.GetAllTimeReportsByWorkplaceIdAndDateAsync(dates[0], dates[1], selectedId);
            }
            catch (Exception ex)
            {
                _error = true;
                _errorMessage = "An error occurred while retrieving time reports: " + ex.Message;
            }
        }

        #region Sorting
        private void SortByDate()
        {
            if (sortByDateAscending)
            {
                timeReportList = timeReportList.OrderBy(f => f.Date).ToList();
            }
            else
            {
                timeReportList = timeReportList.OrderByDescending(f => f.Date).ToList();
            }
            sortByDateAscending = !sortByDateAscending;
        }

        private void SortByHour()
        {
            if (sortByTemperatureCAscending)
            {
                timeReportList = timeReportList.OrderBy(f => f.Hours).ToList();
            }
            else
            {
                timeReportList = timeReportList.OrderByDescending(f => f.Hours).ToList();
            }
            sortByTemperatureCAscending = !sortByTemperatureCAscending;
        }

        private void SortByWorkplaceId()
        {
            if (sortByTemperatureFAscending)
            {
                timeReportList = timeReportList.OrderBy(f => f.WorkplaceId).ToList();
            }
            else
            {
                timeReportList = timeReportList.OrderByDescending(f => f.WorkplaceId).ToList();
            }
            sortByTemperatureFAscending = !sortByTemperatureFAscending;
        }

        private void SortById()
        {
            if (sortBySummaryAscending)
            {
                timeReportList = timeReportList.OrderBy(f => f.Id).ToList();
            }
            else
            {
                timeReportList = timeReportList.OrderByDescending(f => f.Id).ToList();
            }
            sortBySummaryAscending = !sortBySummaryAscending;
        }
        #endregion
    }
}
