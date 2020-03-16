using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherForecast.WebApi.Features.Weather.Dtos;

namespace WeatherForecast.WebApi.Features.Weather.Interfaces
{
    public interface IWeatherForecast
    {
        public Task<List<WeatherDto>> LoadByCityName(string cityName);
    }
}
