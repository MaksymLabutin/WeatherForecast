using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherForecast.WebApi.Features.Weather.Interfaces;
using WeatherForecast.WebApi.Features.Weather.Queries;
using WeatherForecast.WebApi.Models;

namespace WeatherForecast.WebApi.Features.Weather
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly ILogger<WeatherController> _logger;
        private readonly IWeatherForecast _weatherForecast;
        private readonly IMediator _mediator;

        public WeatherController(ILogger<WeatherController> logger, IWeatherForecast weatherForecast, IMediator mediator)
        {
            _logger = logger;
            _weatherForecast = weatherForecast;
            _mediator = mediator;
        }


        [HttpGet("~/api/weather/forecast")]
        public async Task<IActionResult> Get()
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}{Request.PathBase}";

            var response = await _mediator.Send(new GetForecastGeneralInfoQuery(baseUrl)).ConfigureAwait(false);

            return Ok(response);
        }


        [HttpGet("~/api/weather/forecast/{cityName}")]
        public async Task<IActionResult> ForecastByCity(string cityName)
        { 
            var response = await _mediator.Send(new GetForecastByCityQuery(cityName)).ConfigureAwait(false);
            return Ok(response); 

        }
    }


}
