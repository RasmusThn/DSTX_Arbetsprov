using Entities;
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
    public class TimeReportService
    {
        private string myNotSoSecretToken = "3082b4e707da7cf9e8ebe69ab6e8c14c";
        private string _baseAddress = "https://arbetsprov.trinax.se/api/v1/";
        private HttpClient _httpClient;
        private readonly JsonSerializerOptions _options;

        public TimeReportService()
        {
            _httpClient = CreateHttpClient();
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<TimeReport> GetSingleTimeReport(int reportId)
        {
            try
            {
               
                HttpResponseMessage response = await _httpClient.GetAsync($"timereport/{reportId}");

                // Check if the request was successful (status code 200)
                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    TimeReport timeReport = JsonSerializer.Deserialize<TimeReport>(responseData,_options);

                    return timeReport;
                }
                else
                {
                    // Return null or throw an exception if the request was not successful
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occurred during the request
                throw new Exception("An error occurred during the API request.", ex);
            }
        }
        public async Task<string> CreateTimeReport(TimeReport timeReport)
        {
            try
            {
                var formattedTimeReport = new
                {
                    workplace_id = timeReport.WorkplaceId,
                    date = timeReport.Date.ToString("yyyy-MM-dd"),
                    hours = timeReport.Hours.ToString("0.00"),
                    info = timeReport.Info
                };

                var reportContent = JsonSerializer.Serialize(formattedTimeReport);
                var bodyContent = new StringContent(reportContent, Encoding.UTF8, "application/json");
               
                HttpResponseMessage response = await _httpClient.PostAsync("timereport", bodyContent);

                if (response.IsSuccessStatusCode) 
                {
                    return response.StatusCode.ToString();
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex) 
            {
                throw new Exception("An error occurred during the API request.", ex);
            }
        }
        private HttpClient CreateHttpClient()
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(_baseAddress);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", myNotSoSecretToken);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }
    }
}
