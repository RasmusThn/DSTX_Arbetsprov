using Entities.DataTransferObjects;
using Entities.Models;
using ServiceContracts;
using Services.Exceptions;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services
{
    public class TimeReportService : ITimeReportService
    {
        private readonly string _myNotSoSecretToken;
        private readonly string _baseAddress;
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;

        public TimeReportService(string myNotSoSecretToken, string baseAdress)
        {
            _baseAddress = baseAdress;
            _myNotSoSecretToken = myNotSoSecretToken;
            _httpClient = CreateHttpClient();
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<TimeReport> GetSingleTimeReportAsync(int reportId)
        {

            HttpResponseMessage response = await _httpClient.GetAsync($"timereport/{reportId}");
            await HandleResponseAsync(response);

            string responseData = await response.Content.ReadAsStringAsync();
            TransferTimeReportDto? transferTimeReport = JsonSerializer.Deserialize<TransferTimeReportDto>(responseData, _options);
            if (transferTimeReport == null)
            {
                return null;
            }
            else
            {
                TimeReport timeReport = MapTransferToTimeReport(transferTimeReport);
                return timeReport;
            }

        }
        public async Task<int> PostTimeReportAsync(TransferTimeReportDto timeReport)
        {
            var reportContent = JsonSerializer.Serialize(timeReport);
            var bodyContent = new StringContent(reportContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("timereport", bodyContent).ConfigureAwait(false);
            await HandleResponseAsync(response);

            string responseData = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            TemporaryTimeReportDto temporaryTimeReport = JsonSerializer.Deserialize<TemporaryTimeReportDto>(responseData, _options);

            int id = Convert.ToInt32(temporaryTimeReport.Id);

            return id;
        }
        public async Task<List<Workplace>> GetAllWorkplacesAsync()
        {
            HttpResponseMessage response = await _httpClient.GetAsync("workplace").ConfigureAwait(false);
            await HandleResponseAsync(response);

            string responseData = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            List<Workplace> workplaces = JsonSerializer.Deserialize<List<Workplace>>(responseData, _options);

            return workplaces;
        }
        public async Task<List<TimeReport>> GetAllTimeReportsByWorkplaceIdAndDateAsync(DateTime fromDate, DateTime toDate, int id)
        {
            string from_date = fromDate.ToString("yyyy-MM-dd");
            string to_date = toDate.ToString("yyyy-MM-dd");
            string url = "";

            //if "0", client will get all workplaces
            if (id == 0)
            {
                url = $"timereport?from_date={from_date}&to_date={to_date}";
            }
            else
            {
                url = $"timereport?from_date={from_date}&to_date={to_date}&workplace={id}";
            }

            HttpResponseMessage response = await _httpClient.GetAsync(url).ConfigureAwait(false);
            await HandleResponseAsync(response);

            string responseData = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            List<TransferTimeReportDto> transferTimeReports = JsonSerializer.Deserialize<List<TransferTimeReportDto>>(responseData, _options);

            // Map the TransferTimeReportDto objects to TimeReport objects
            List<TimeReport> timeReports = transferTimeReports?.Select(MapTransferToTimeReport).ToList();

            return timeReports;
        }

        private async Task<bool> HandleResponseAsync(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new NotFoundException("The requested resource was not found.");
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedException("Unauthorized access. Please authenticate.");
            }
            else if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                throw new ForbiddenException("Access to the resource is forbidden.");
            }
            else
            {
                throw new ApiException("An error occurred during the API request.");
            }
        }
        private TimeReport MapTransferToTimeReport(TransferTimeReportDto transferTimeReport)
        {
            TimeReport timeReport = new TimeReport
            {
                Id = transferTimeReport.Id,
                WorkplaceId = transferTimeReport.WorkplaceId,
                Date = DateTime.Parse(transferTimeReport.Date),
                Hours = float.Parse(transferTimeReport.Hours),
                Info = transferTimeReport.Info
            };

            return timeReport;
        }
        private HttpClient CreateHttpClient()
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(_baseAddress);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _myNotSoSecretToken);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }

    }
}
