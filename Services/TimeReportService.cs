﻿using Entities.DataTransferObjects;
using Entities.Models;
using ServiceContracts;
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
        public async Task<int> PostTimeReport(TransferTimeReportDto timeReport)
        {

            var reportContent = JsonSerializer.Serialize(timeReport);
            var bodyContent = new StringContent(reportContent, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("timereport", bodyContent);

            if (response.IsSuccessStatusCode)
            {

                string responseData = await response.Content.ReadAsStringAsync();
                TemporaryTimeReportDto temporaryTimeReport = JsonSerializer.Deserialize<TemporaryTimeReportDto>(responseData, _options);

                int id = Convert.ToInt32(temporaryTimeReport.Id);

                return id;
            }
            else
            {
                throw new Exception("An error occurred during the API request.");
            }

        }
        public async Task<List<TimeReport>> GetAllTimeReports()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"timereport");

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    List<TransferTimeReportDto> transferTimeReports = JsonSerializer.Deserialize<List<TransferTimeReportDto>>(responseData, _options);

                    if (transferTimeReports != null)
                    {
                        // Map the TransferTimeReportDto objects to TimeReport objects
                        List<TimeReport> timeReports = transferTimeReports.Select(MapTransferToTimeReport).ToList();

                        return timeReports;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occurred during the request
                throw new Exception("An error occurred during the API request.", ex);
            }
        }
        public async Task<List<Workplace>> GetAllWorkplacesAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"workplace");

                // Check if the request was successful (status code 200)
                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    List<Workplace>? workplace = JsonSerializer.Deserialize<List<Workplace>>(responseData, _options);
                    if (workplace == null)
                    {
                        return null;
                    }
                    else
                    {
                        return workplace;
                    }
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
        public async Task<List<TimeReport>> GetAllTimeReportsByIdAndDate(DateTime fromDate, DateTime toDate, int id)
        {
            try
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

                HttpResponseMessage response = await _httpClient.GetAsync(url);

                // Check if the request was successful (status code 200)
                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    List<TransferTimeReportDto> transferTimeReports = JsonSerializer.Deserialize<List<TransferTimeReportDto>>(responseData, _options);

                    if (transferTimeReports != null)
                    {
                        // Map the TransferTimeReportDto objects to TimeReport objects
                        List<TimeReport> timeReports = transferTimeReports.Select(MapTransferToTimeReport).ToList();

                        return timeReports;
                    }
                    else
                    {
                        return null;
                    }
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
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", myNotSoSecretToken);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }

    }
}
