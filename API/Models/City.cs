using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherForecast.WebApi.Models
{
    public class City
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string ZipCode { get; set; }
    }
}
