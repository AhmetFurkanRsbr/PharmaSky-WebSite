using Newtonsoft.Json.Linq;
using Pharmacy.Persistence.Context;
using PharmaSky.Domain.Entities;
using PharmaSky.Domain.Enums;


namespace Pharmacy.Persistence.Repositories
{
    public class WeatherRepository
    {
        private readonly PharmaSkyDbContext _weatherDbContext;

        public WeatherRepository(PharmaSkyDbContext weatherDbContext)
        {
            _weatherDbContext = weatherDbContext;
        }

        public async Task<Weather> GetWeatherByCityAsync(string city, string apiKey)
        {
            // Burada API çağrısı yapılacak
            using var httpClient = new HttpClient();
            var postValue = "https://api.openweathermap.org/data/2.5/weather?q=" + city + "&appid=" + apiKey + "&units=metric&lang=tr";
            var response = await httpClient.GetStringAsync(postValue);


            var obj = JObject.Parse(response);

            var entity = new Weather
            {
                City = obj["name"]?.ToString(),
                Temperature = (int)Math.Round((double)obj["main"]["temp"]),
                WeatherDescription = obj["weather"][0]["description"]?.ToString(),
                WeatherPerceived = obj["main"]["feels_like"]?.ToString(),
                Humidity = (int)obj["main"]["humidity"],
                Wind = obj["wind"]["speed"]?.ToString(),
                Date = DateTime.Now,
                Day = (EDays)DateTime.Now.DayOfWeek // DayOfWeek -> enum mapping
            };

            return entity;

        }


        public async Task SaveWeatherAsync(Weather entity)
        {
            _weatherDbContext.Add(entity);
            await _weatherDbContext.SaveChangesAsync();
        }
    }
}
