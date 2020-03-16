using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherForecast.WebApi.Features.Weather.Dtos;
using WeatherForecast.WebApi.Features.Weather.Interfaces;

namespace WeatherForecast.WebApi.Features.Weather.Services
{
    public class WeatherForecastClient : IWeatherForecast
    {
        private readonly HttpClient _client; 
        private readonly IConfiguration _configuration;
        private readonly IWeatherForecastResult _weatherForecastResult;

        public WeatherForecastClient(IConfiguration configuration, IWeatherForecastResult weatherForecastResults)
        {
            _client = new HttpClient();
            _configuration = configuration;
            _weatherForecastResult = weatherForecastResults;
        }

        public async Task<List<WeatherDto>> LoadByCityName(string cityName)
        { 
            var data = await LoadData(cityName); 
            var list = _weatherForecastResult.Parse(data); 
            return list;
        }

        private async Task<dynamic> LoadData(string cityName)
        {
            var weatherForecastSection = _configuration.GetSection("WeatherForecast");
            var url = $"{weatherForecastSection.GetSection("WeatherForecastApiUrl").Value}?q={cityName}&appid={weatherForecastSection.GetSection("AuthKey").Value}";

            HttpResponseMessage response = await _client.GetAsync(url);

            if (!response.IsSuccessStatusCode) throw new HttpRequestException(response.Content.ToString());

            dynamic str = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());

            return str;
        }

    }


}
