
<h1 align="center">🌤️ PharmaSky  </h1>

<p align="center">
  <img src="https://github.com/user-attachments/assets/3bae49d3-c5f9-404f-930e-ba309fb5af72" alt="PharmaSky Logo" width="180"/>
</p>

---
## 🌐 Overview (EN)

**PharmaSky** is an **pharmacy and weather information system** project developed using ASP.NET Core. The project provides access to functions such as **on-duty pharmacy information**, **weather data**, and **authentication**.
The UI component is a single MVC project. However, AuthAPI, PharmacyAPI, and WeatherAPI are separate API projects. 
I ensure communication between clients through tokens created using clientId, clientSecret, Username, and Password.
I used the “https://openweathermap.org/api” source to retrieve weather data. A microservice architecture was used.
The token contains role-based access permissions (Admin/Weather/On-CallPharmacy).

---
## 🇹🇷 Proje Özeti (TR)

**PharmaSky**, ASP.NET Core tabanlı geliştirilen bir **eczane ve hava durumu bilgi sistemi** projesidir. Proje kapsamında **Nöbetçi eczane bilgileri**, **hava durumu verileri** ve **yetkilendirme** gibi fonksiyonlar aracılığıyla erişilmektedir. 
UI kısmı tek bir MVC projesidir. Ancak AuthAPI, PharmacyAPI, WeatherAPI farklı API projeleridir. 
Client'ler arası iletişimi clientId, clientSecret, Username, Password ile oluşturulan token aracılığıyla sağlamaktayım.
Hava durumu verilerini çekmek için "https://openweathermap.org/api" kaynağını kullandım. Mikroservis mimarisi kullanılmıştır . 
Token içindeki rol bazlı erişim izni vardır(Admin/HavaDurumu/NobetciEczane).

---



## 🚀 Tech Stack / Teknoloji Yığını 
- **Design Pattern:** Microservice Pattern
- **Backend:** ASP.NET Core (C#)  
- **Frontend:** MVC / Razor Pages + Bootstrap  
- **Database / Veritabanı:** Entity Framework Core (SQL Server)  
- **Authentication / Kimlik Doğrulama:** JWT & Cookie Authentication (ClientID, ClientSecret, Username, Password)  
- **Other Tools / Diğer Araçlar:** Swagger, LINQ, Dependency Injection  

---

## ✨ Features / Özellikler  
- 🌍 **Weather Information / Hava Durumu Bilgisi** – City-based weather data / Şehir bazlı hava durumu verisi  
- 💊 **On-Duty Pharmacies / Nöbetçi Eczaneler** –  list of pharmacies / Nöbetçi eczane listesi  
- 🔐 **Authentication & Authorization / Kimlik Doğrulama ve Yetkilendirme** – JWT & Client-Id support / JWT ve Client-Id desteği  
- ⚡ **Error Management / Hata Yönetimi** – Centralized error handling with middleware / Middleware ile merkezi hata yönetimi  
- 📊 **API Documentation / API Dokümantasyonu** – Swagger integration / Swagger entegrasyonu  

---

## 🖼️ Uygulama Görselleri

<p align="center"> 
  <img src="https://github.com/user-attachments/assets/0ecaf003-142a-47c5-8265-e1637deb2878" width="400"/> 
  <img src="https://github.com/user-attachments/assets/873ee1d2-f689-4bae-ba55-dd1635d09fe6" width="400"/><br><br> 
  <img src="https://github.com/user-attachments/assets/a50c890c-f2e3-4d23-90ba-ff013ce663d0" width="400"/> 
  <img src="https://github.com/user-attachments/assets/69c7a426-2324-4724-97d5-da57f21c2698" width="400"/><br><br> 
  <img src="https://github.com/user-attachments/assets/18b0b233-d81a-44e2-b526-aa57e4247f11" width="400"/> 
  <img src="https://github.com/user-attachments/assets/93c57d7d-cad7-4afd-802d-61d87fad9774" width="400"/><br><br> 
  <img src="https://github.com/user-attachments/assets/db75fabe-3675-4d5e-9c59-78116049df34" width="400"/> 
  <img src="https://github.com/user-attachments/assets/c7f6cb39-227c-4c02-9f6b-d37b1fd0c086" width="400"/><br><br> 
  <img src="https://github.com/user-attachments/assets/48215619-ee63-4e28-93d5-0fbedb839e28" width="400"/> 

</p>

---
## 🛠️ Installation / Kurulum  
1. Clone the repository / Repoyu klonlayın:  
   ```bash
   git clone https://github.com/AhmetFurkanRsbr/PharmaSky.git
   cd PharmaSky
   ```
2. Configure **appsettings.json** (connection string, JWT secret, issuer, audience) / **appsettings.json** dosyasını yapılandırın (connection string, JWT secret, issuer, audience).  
3. Apply migrations / Migration işlemlerini uygulayın:  
   ```bash
   dotnet ef database update
   ```
4. Run the project with Api and Ui / Ui ve Api projelerini birlikte ve çalıştırın:  
   ```bash
   dotnet run PharmaSky.WebUI
   dotnet run PharmacyAPI
   dotnet run WeatherAPI
   dotnet run AuthAPI
   ```

---
