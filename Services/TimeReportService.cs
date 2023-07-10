using Entities;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Services
{
    public class TimeReportService
    {
        private string myNotSoSecretToken = "3082b4e707da7cf9e8ebe69ab6e8c14c";
        private string _baseAddress = "https://arbetsprov.trinax.se/api/v1/";
        private HttpClient _httpClient;

        public TimeReportService()
        {
            _httpClient = CreateHttpClient();
        }

        public async Task<TimeReport> GetSingleTimeReport(int reportId)
        {
            try
            {
               
                HttpResponseMessage response = await _httpClient.GetAsync($"timereport/{reportId}");

                // Check if the request was successful (status code 200)
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as a string
                    string responseData = await response.Content.ReadAsStringAsync();

                    // Deserialize the JSON response into an instance of TimeReport
                    TimeReport timeReport = JsonConvert.DeserializeObject<TimeReport>(responseData);

                    // Return the deserialized time report
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
        private HttpClient CreateHttpClient()
        {
            // Create a HttpClient instance
            HttpClient httpClient = new HttpClient();

            // Set the base URL for all actions
            httpClient.BaseAddress = new Uri(_baseAddress);

            // Set the authorization token in the request header
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", myNotSoSecretToken);

            // Set the Accept header to indicate that you expect JSON response
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }
    }
}
