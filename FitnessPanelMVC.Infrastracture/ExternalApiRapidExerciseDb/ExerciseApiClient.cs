using FitnessPanelMVC.Domain.Interfaces;
using FitnessPanelMVC.Domain.Model;
using FitnessPanelMVC.Infrastructure.ExternalApiRapidExerciseDb.Helpers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace FitnessPanelMVC.Infrastructure.ExternalApiRapidExerciseDb
{
    public class ExerciseApiClient : IExerciseApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        private readonly ExerciseUrlBuilder _urlBuilder;

        private readonly IConfiguration _configuration;

        public ExerciseApiClient(IHttpClientFactory httpClientFactory,
            ExerciseUrlBuilder urlBuilder,
            IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _urlBuilder = urlBuilder;
            _configuration = configuration;
        }

        public async Task<HttpResponseMessage> GetExerciseByBodyPartAsync(string bodyPart)
        {
            var apiKey = _configuration["ExerciseApi:X-RapidAPI-Key"];
            var apiHost = _configuration["ExerciseApi:X-RapidAPI-Host"];

            var httpClient = _httpClientFactory.CreateClient();

            var url = _urlBuilder.HttpUlrBuilder(bodyPart.ToLower());
            var builder = new UriBuilder(url);

            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", apiKey);
            httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Host", apiHost);

            var response = await httpClient.GetAsync(builder.Uri);
            return response;
        }
    }
}
