using System;

namespace WeatherForecast.WebApi.Features.Weather.Dtos
{
    public class WeatherForecastDto
    {
        public DateTime Date { get; set; }
        public double Temperature { get; set; }
        public double Humidity { get; set; }
        public double WindSpeed { get; set; } 
    }
}
