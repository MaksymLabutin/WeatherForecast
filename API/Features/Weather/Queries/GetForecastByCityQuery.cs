using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast.WebApi.Features.Weather.Dtos;

namespace WeatherForecast.WebApi.Features.Weather.Queries
{
    public class GetForecastByCityQuery : IRequest<List<WeatherForecastDto>>
    {
        public GetForecastByCityQuery(string cityName)
        {
            CityName = cityName;
        }

        public string CityName { get; set; }

    }
}
