using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pharmacy.Persistence.Repositories;
using PharmaSky.Dto;
using System.Net.Http;
using static System.Net.WebRequestMethods;

namespace PharmaSky.WebUI.Controllers
{
    public class PharmacyController : Controller
    {

        private readonly IHttpClientFactory _http;
        private readonly IConfiguration _config;
        private readonly IHttpContextAccessor _accessor;

        public PharmacyController(IHttpClientFactory http, IConfiguration config, IHttpContextAccessor accessor)
        {
            _http = http;
            _config = config;
            _accessor = accessor;
        }

      
        public async Task<IActionResult> Index()
        {
            var token = _accessor.HttpContext!.Request.Cookies["access_token"];

            var client = _http.CreateClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);


            if (string.IsNullOrEmpty(token))
            {
                var message = Uri.EscapeDataString("Oturum açmanız gerekiyor!");
                return Redirect($"https://localhost:7004/Login/Index?message={message}&messageType=warning");
            }

            // API'den gelen veriyi string olarak al
            var response = await client.GetAsync("https://localhost:7101/api/Pharmacies/GetAllPharmacies");

            if (response.StatusCode == System.Net.HttpStatusCode.Forbidden ||
                response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                var message = Uri.EscapeDataString("Yetkiniz bu sayfaya erişmeye yeterli değil!");
                return Redirect($"https://localhost:7004/Login/Index?message={message}&messageType=warning");
            }

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Eczane bilgileri alınamadı.";
                return View("Error");
            }



            var json = await response.Content.ReadAsStringAsync();
            var pharmacyList = JsonConvert.DeserializeObject<List<PharmacyDto>>(json);

            return View(pharmacyList);
        }
    }
}


