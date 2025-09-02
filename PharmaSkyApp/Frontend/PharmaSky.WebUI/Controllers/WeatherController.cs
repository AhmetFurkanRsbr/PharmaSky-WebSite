using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using PharmaSky.Domain.Entities;

namespace PharmaSky.WebUI.Controllers
{
    public class WeatherController : Controller
    {
        private readonly HttpClient _httpClient;

        public WeatherController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string city = "istanbul";
            string apiUrl = $"https://localhost:7173/api/Weathers/{city}";

            // Cookie’den token al
            var token = Request.Cookies["access_token"];

            if (string.IsNullOrEmpty(token))
            {
                var message = Uri.EscapeDataString("Oturum açmanız gerekiyor!");
                return Redirect($"https://localhost:7004/Login/Index?message={message}&messageType=warning");
            }


            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync(apiUrl);
            if (response.StatusCode == System.Net.HttpStatusCode.Forbidden ||
               response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                var message = Uri.EscapeDataString("Yetkiniz bu sayfaya erişmeye yeterli değil!");
                return Redirect($"https://localhost:7004/Login/Index?message={message}&messageType=warning");
            }

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Hava durumu bilgileri alınamadı.";
                return View("Error");
            }


            var bytes = await response.Content.ReadAsByteArrayAsync();
            var json = Encoding.UTF8.GetString(bytes);

            // JSON'u modele dönüştür
            var weatherData = JsonConvert.DeserializeObject<Weather>(json);
            var weatherList = new List<Weather> { weatherData };

            // ViewBag ile ekstra bilgiler taşı
            ViewBag.City = city;
            ViewBag.Token = token;
            ViewBag.LastUpdate = DateTime.Now.ToString("dd.MM.yyyy HH:mm");

            return View(weatherList);
        }
        [HttpGet]
        public async Task<IActionResult> Search(string city)
        {
            if (string.IsNullOrEmpty(city))
                return BadRequest("Şehir adı gerekli!");

            string apiUrl = $"https://localhost:7173/api/Weathers/{city}";

            var token = Request.Cookies["access_token"];
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { message = "Oturum açmanız gerekiyor!" });
            }

            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync(apiUrl);
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest(new { message = "Hava durumu bilgisi alınamadı!" });
            }

            var bytes = await response.Content.ReadAsByteArrayAsync();
            var json = Encoding.UTF8.GetString(bytes);

            var weatherData = JsonConvert.DeserializeObject<Weather>(json);

            return Json(weatherData); // JSON olarak geri döner
        }


    }
}
