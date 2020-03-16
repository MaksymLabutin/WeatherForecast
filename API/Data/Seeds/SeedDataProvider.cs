using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeatherForecast.WebApi.Models;

namespace WeatherForecast.WebApi.Data.Seeds
{
    public static class SeedDataProvider
    {
        public static void SeedGeneralData(MigrationBuilder migrationBuilder)
        {
            List<City> Cities = new List<City>
            {
                new City{Name = "Leipzig", ZipCode = "04103"},
                new City{Name = "Berlin", ZipCode = "10115"},
                new City{Name = "Munich", ZipCode = "80331"},
                new City{Name = "Frankfurt", ZipCode = "60306"},
                new City{Name = "Hannover", ZipCode = "30159"},
                new City{Name = "Dresde", ZipCode = "01067"},
            };

            foreach(var city in Cities)
            {
                InsertCity(migrationBuilder, city);

            }

        }

        public static void InsertCity(MigrationBuilder migrationBuilder, City city)
        {
            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] {  "Name", "ZipCode" },
                values: new object[] { city.Name, city.ZipCode });
        }
    }
}
