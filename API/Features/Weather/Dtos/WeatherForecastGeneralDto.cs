using System.Collections.Generic; 

namespace WeatherForecast.WebApi.Features.Weather.Dtos
{
    public class WeatherForecastGeneralDto
    {
        public string SelectedCity { get; set; }
        public List<LinkDto> Links{ get; set; }
        public List<CityDto> Cities { get; set; }
        public List<WeatherForecastDto> WeatherForecast { get; set; }
    }
}
