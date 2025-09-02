using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Persistence.Repositories;
using PharmaSky.Domain.Entities;
using PharmaSky.Domain.Enums;

namespace WeatherAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "HavaDurumu,Admin")]
    public class WeathersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly WeatherRepository _weatherRepository;


        public WeathersController(IConfiguration configuration, WeatherRepository weatherRepository)
        {
            _configuration = configuration;
            _weatherRepository = weatherRepository;
        }

        [HttpGet("{city}")]
        public async Task<IActionResult> GetWeather(string city)
        {
            var result = await _weatherRepository.GetWeatherByCityAsync(city, _configuration["WeatherApi:ApiKey"]);
            // DTO’yu Entity’ye dönüştür
            var entity = new Weather
            {

                City = result.City,
                Temperature = result.Temperature,//(int)Math.Round(result.Temperature),
                WeatherDescription = result.WeatherDescription,
                WeatherPerceived = Math.Round(double.Parse(result.WeatherPerceived), 0).ToString(),
                Humidity = result.Humidity,
                //Wind = result.Wind?.ToString(),
                Wind = ((int)(double.Parse(result.Wind) * 3.6)).ToString(),
                Date = DateTime.Now,
                Day = (EDays)DateTime.Now.DayOfWeek
            };

            // DB’ye kaydet
            await _weatherRepository.SaveWeatherAsync(entity);

           
            return Ok(entity);
        }

    }
}
