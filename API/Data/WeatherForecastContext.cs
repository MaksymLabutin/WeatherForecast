using Microsoft.EntityFrameworkCore; 
using WeatherForecast.WebApi.Models;

namespace WeatherForecast.WebApi.Data
{
    public class WeatherForecastContext : DbContext
    {
        public WeatherForecastContext(DbContextOptions<WeatherForecastContext> options) : base(options) { }
         
        public DbSet<City> Cities { get; set; }
        public DbSet<WeatherForecastData> WeatherForecasts { get; set; }

    }
}
