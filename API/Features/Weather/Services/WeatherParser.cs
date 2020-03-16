using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast.WebApi.Features.Weather.Dtos;
using WeatherForecast.WebApi.Features.Weather.Interfaces;

namespace WeatherForecast.WebApi.Features.Weather.Services
{
    public class WeatherParser : IWeatherForecastResult
    {
        public List<WeatherDto> Parse(dynamic data)
        { 
            var list = new List<WeatherDto>();

            foreach (var el in data.list)
            {
                var humidity = (int)el.main.humidity.Value;
                double temperature = (el.main.temp_max.Value + el.main.temp_min.Value) / 2;
                double windSpped = el.wind.speed.Value;
                DateTime date = DateTime.ParseExact(el.dt_txt.Value, "yyyy-MM-dd HH:mm:ss", CultureInfo.CurrentCulture);

                list.Add(new WeatherDto
                {
                    Date = date,
                    Humidity = humidity,
                    Temperature = Math.Round(temperature, 1),
                    WindSpeed = Math.Round(windSpped, 1)
                });
            }

            return list;
        }
    }
}
