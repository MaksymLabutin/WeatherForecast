using MediatR; 
using WeatherForecast.WebApi.Features.Weather.Dtos;

namespace WeatherForecast.WebApi.Features.Weather.Queries
{
    public class GetForecastGeneralInfoQuery : IRequest<WeatherForecastGeneralDto>
    {
        public GetForecastGeneralInfoQuery(string baseUrl)
        {
            BaseUrl = baseUrl;
        }

        public string BaseUrl { get; set; }
    }
}
