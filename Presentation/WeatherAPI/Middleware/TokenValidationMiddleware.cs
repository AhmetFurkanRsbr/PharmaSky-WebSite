//Şuan da kullanılmıyor
namespace WeatherAPI.Middleware
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly HttpClient _http;

        public TokenValidationMiddleware(RequestDelegate next, IHttpClientFactory httpFactory)
        {
            _next = next;
            _http = httpFactory.CreateClient();
        }

        public async Task InvokeAsync(HttpContext context, IConfiguration config)
        {
            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
            if (string.IsNullOrEmpty(authHeader))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Missing token");
                return;
            }

            var authApi = config["Services:Auth"]!;
            var req = new HttpRequestMessage(HttpMethod.Get, $"{authApi}/auth/validate");
            req.Headers.Add("Authorization", authHeader);

            var res = await _http.SendAsync(req);
            if (!res.IsSuccessStatusCode)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid token");
                return;
            }

            await _next(context);
        }
    }
}
