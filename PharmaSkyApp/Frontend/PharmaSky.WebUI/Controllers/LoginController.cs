using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using PharmaSky.Dto;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace PharmaSky.WebUI.Controllers
{
    public class LoginController : Controller
    {

        private readonly IHttpClientFactory _http;
        private readonly IConfiguration _config;

        public LoginController(IHttpClientFactory http, IConfiguration config)
        {
            _http = http;
            _config = config;
        }

        [HttpPost("/login")]
        public async Task<IActionResult> Login(string username, string password)
        {
            var client = _http.CreateClient();
            var response = await client.PostAsJsonAsync(
                $"{_config["Services:Auth"]}/api/auth/token",
                new
                {
                    ClientId = _config["OAuth:ClientId"],
                    ClientSecret = _config["OAuth:ClientSecret"],
                    Username = username,
                    Password = password
                });


            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                ViewBag.Message = "Giriş başarısız!";
                ViewBag.MessageType = "error";
                return View("Index", new LoginDto { Username = username });
            }
            else if (response.StatusCode == HttpStatusCode.Forbidden)
            {
                ViewBag.Message = "Hesabınızın yetkisi yeterli değil!";
                ViewBag.MessageType = "warning";
                return View("Index", new LoginDto { Username = username });
            }
            else if (!response.IsSuccessStatusCode)
            {
                ViewBag.Message = "Bilinmeyen bir hata oluştu!";
                ViewBag.MessageType = "error";
                return View("Index", new LoginDto { Username = username });
            }

            var tokenResponse = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
            var token = tokenResponse["access_token"];

            //  Token'ı cookie'de sakla
            Response.Cookies.Append("access_token", token, new CookieOptions { HttpOnly = true });

            var role = tokenResponse.Values.ElementAt(1);
            Response.Cookies.Append("role", role);

            Response.Cookies.Append("user_name", username);

            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult Index(string? message, string? messageType)
        {

            if (!string.IsNullOrEmpty(message))
            {
                ViewBag.Message = message;
                ViewBag.MessageType = messageType ?? "info";
            }

            return View();
        }

        
        public async Task<IActionResult> Logout()
        {
            // Cookie'den token'ı sil
            Response.Cookies.Delete("access_token");

            // Kullanıcıyı çıkış yap
            Response.Cookies.Delete("role");
            Response.Cookies.Delete("user_name");

            return RedirectToAction("Index", "Login", new { message = "Başarıyla çıkış yapıldı.", messageType = "success" });
        }
    }
}
