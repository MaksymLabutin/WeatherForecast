using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Dynamic;
using WeatherForecast.WebApi.Features.Weather.Dtos;
using WeatherForecast.WebApi.Features.Weather.Services;
using Xunit;

namespace WeatherForecast.WebApi.Tests.Features.Weather.Services
{
    public class WeatherParserTest
    {

        [Fact]
        public void Should_parse_data_When_all_data_is_valid()
        {
            var parser = new WeatherParser();
            var testData = GetTestData();

            var result = parser.Parse(testData);

            var expectedResult = new List<WeatherDto>()
            {
                new       WeatherDto(){

                    Date = DateTime.Parse("3/16/2020 9:00:00 AM"),
                    Humidity = 48,
                    Temperature = 282.7,
                    WindSpeed = 1.2
                }      
            };

            expectedResult.Should().BeEquivalentTo(result); 
        }
        
        [Fact]
        public void Should_throw_exception_When_date_format_is_wrong()
        {
            var parser = new WeatherParser();

            var testData = GetWrongData(); 

            Action action = () => parser.Parse(testData);

            Assert.Throws<FormatException>(action); 
        }


        private dynamic GetTestData()
        {
            dynamic data = new ExpandoObject();
            dynamic element = new ExpandoObject();
            element.main = new ExpandoObject();
            element.main.humidity = new ExpandoObject();
            element.main.humidity.Value = 48;

            element.main.temp_max = new ExpandoObject();
            element.main.temp_max.Value = 283.42;
            element.main.temp_min = new ExpandoObject();
            element.main.temp_min.Value = 281.96;

            element.dt_txt = new ExpandoObject();
            element.dt_txt.Value = "2020-03-16 09:00:00";

            element.wind = new ExpandoObject();
            element.wind.speed = new ExpandoObject();
            element.wind.speed.Value = 1.19;

            data.list = new List<dynamic>
            {
               element
            };
            
            return data;
        }
        
        private dynamic GetWrongData()
        {
            dynamic data = new ExpandoObject();
            dynamic element = new ExpandoObject();
            element.main = new ExpandoObject();
            element.main.humidity = new ExpandoObject();
            element.main.humidity.Value = 48;

            element.main.temp_max = new ExpandoObject();
            element.main.temp_max.Value = 283.42;
            element.main.temp_min = new ExpandoObject();
            element.main.temp_min.Value = 281.96;

            element.dt_txt = new ExpandoObject();
            element.dt_txt.Value = "2020/03/16 09:00:00";

            element.wind = new ExpandoObject();
            element.wind.speed = new ExpandoObject();
            element.wind.speed.Value = 1.19;

            data.list = new List<dynamic>
            {
               element
            };


            return data;
        }
    }
}
