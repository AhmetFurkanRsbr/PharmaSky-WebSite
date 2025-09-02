using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;


namespace PharmaSky.WebUI.Controllers
{    
    public class AuthController(HttpClient http, IConfiguration cfg) : Controller
    {
        public record LoginResponse(string AccessToken, int ExpiresIn);

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var res = await http.PostAsJsonAsync($"{cfg["Services:Auth"]}/auth/login", new
            {
                ClientId = cfg["Client:Id"],
                ClientSecret = cfg["Client:Secret"],
                Username = username,
                Password = password,

            });

            if (!res.IsSuccessStatusCode) return Unauthorized();

            var token = await res.Content.ReadFromJsonAsync<LoginResponse>();
            Response.Cookies.Append("access_token", token!.AccessToken, new CookieOptions { HttpOnly = true, Secure = true });

            return RedirectToAction("Index", "Home");
        }
    }

}
