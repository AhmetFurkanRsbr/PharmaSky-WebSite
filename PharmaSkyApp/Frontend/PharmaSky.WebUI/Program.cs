using Microsoft.EntityFrameworkCore;
using Pharmacy.Persistence.Context;

// Program.cs
var builder = WebApplication.CreateBuilder(args);

// DbContext servisini ekle (varsa)
builder.Services.AddDbContext<PharmaSkyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// IHttpClientFactory servisini ekle
builder.Services.AddHttpClient();

// IHttpContextAccessor servisini ekle (Cookies için gerekli)
builder.Services.AddHttpContextAccessor();

// Kontrolcü ve View'larý ekle
builder.Services.AddControllersWithViews();

var app = builder.Build();

// --- HTTP Ýstek Ýþlem Hattý (Middleware) ---

// Geliþtirme ortamýnda hata sayfasýný kullan
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Routing'i etkinleþtir
app.UseRouting();

// Kimlik doðrulama ve yetkilendirme middleware'lerini ekle

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();