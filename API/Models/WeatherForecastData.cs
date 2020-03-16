using System; 

namespace WeatherForecast.WebApi.Models
{
    public class WeatherForecastData
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public double Temperature { get; set; }

        public double Humidity { get; set; }

        public double WindSpeed { get; set; }
         
        public City City { get; set; }  
    }
}
