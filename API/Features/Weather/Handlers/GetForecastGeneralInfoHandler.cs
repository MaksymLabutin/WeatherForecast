using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WeatherForecast.WebApi.Data;
using WeatherForecast.WebApi.Features.Weather.Dtos;
using WeatherForecast.WebApi.Features.Weather.Interfaces;
using WeatherForecast.WebApi.Features.Weather.Queries;
using WeatherForecast.WebApi.Models;

namespace WeatherForecast.WebApi.Features.Weather.Handlers
{
    public class GetForecastGeneralInfoHandler : IRequestHandler<GetForecastGeneralInfoQuery, WeatherForecastGeneralDto>
    {

        private readonly WeatherForecastContext _context;

        private readonly IWeatherForecast _weatherForecast;
        private readonly IConfiguration _configuration;

        public GetForecastGeneralInfoHandler(WeatherForecastContext context, IWeatherForecast weatherForecast, IConfiguration configuration)
        {
            _context = context;
            _weatherForecast = weatherForecast;
            _configuration = configuration;
        }

        public async Task<WeatherForecastGeneralDto> Handle(GetForecastGeneralInfoQuery request, CancellationToken cancellationToken)
        {
            var rng = new Random();

            var cities = await _context.Cities.ToListAsync();

            var selectedCity = cities[rng.Next(0, cities.Count - 1)];

            var takeCount = int.Parse(_configuration.GetSection("TotalShow").Value);

            var weatherForecast = await LoadWeatherForecast(selectedCity, takeCount);

            if (weatherForecast.Count == 0 || weatherForecast.Count < takeCount)
            {
                await LoadFromApiAndPutToDb(weatherForecast, selectedCity);
                weatherForecast = await LoadWeatherForecast(selectedCity, takeCount);
            }

            var response = CreateResponse(cities, weatherForecast, request.BaseUrl, selectedCity.Name);

            return response;
        }
         
        public async Task<List<WeatherForecastData>> LoadWeatherForecast(City city, int takeCount)
        {
            return await _context.WeatherForecasts.Where(_ => _.Date.Date >= DateTime.Now.Date && _.City.Name == city.Name).Take(takeCount).ToListAsync();
        }

        private async Task LoadFromApiAndPutToDb(List<WeatherForecastData> weatherForecast, City city)
        {
            var data = await _weatherForecast.LoadByCityName(city.Name);

            var groupedData = data
                .GroupBy(_ => _.Date.Date)
                .ToDictionary(_ => _.Key, _ => _.ToList())
                .Select(_ => new WeatherForecastData
                {
                    Date = _.Key,
                    Temperature = _.Value.Average(_ => _.Temperature),
                    Humidity = _.Value.Average(_ => _.Humidity),
                    WindSpeed = _.Value.Average(_ => _.WindSpeed)
                });
              
            var maxDate = weatherForecast.Count == 0 ? DateTime.MinValue : weatherForecast.Max(_ => _.Date.Date);
            
            var newWeatherForecast = groupedData.Where(_ => _.Date.Date > maxDate).ToList();

            _context.WeatherForecasts.AddRange(newWeatherForecast.Select(_ => new WeatherForecastData
            {
                City = city,
                Date = _.Date,
                Humidity = _.Humidity,
                Temperature = _.Temperature,
                WindSpeed = _.WindSpeed
            }));

            _context.SaveChanges();
        }

        public WeatherForecastGeneralDto CreateResponse(List<City> cities, List<WeatherForecastData> weatherForecast, string baseUrl, string selectedCity)
        { 
            var response = new WeatherForecastGeneralDto
            {
                Links = new List<LinkDto> {
                    new LinkDto{
                        Rel = "getByCity",
                        Href = $"{baseUrl}/api/weather/forecast/"
                    }
                },
                Cities = cities.Select(_ => new CityDto
                {
                    Name = _.Name,
                    ZipCode = _.ZipCode
                }).ToList(),
                SelectedCity = selectedCity,
                WeatherForecast = weatherForecast.Select(_ => new WeatherForecastDto
                {
                    Date = _.Date,
                    Humidity = Math.Round(_.Humidity, 1),
                    Temperature = Math.Round(_.Temperature, 1),
                    WindSpeed = Math.Round(_.WindSpeed, 1)
                }).ToList()
            };

            return response;
        }
    }
}
