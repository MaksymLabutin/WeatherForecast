
using System.Collections.Generic;
using WeatherForecast.WebApi.Features.Weather.Dtos;

namespace WeatherForecast.WebApi.Features.Weather.Interfaces
{
    public interface IWeatherForecastResult
    {
        public List<WeatherDto> Parse(dynamic data);
    }
}
