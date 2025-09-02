// Partikül animasyonu ve diğer tüm sayfa işlevleri
$(document).ready(function () {
    // Particles.js'i başlat
    particlesJS('particles-js', {
        "particles": {
            "number": { "value": 80, "density": { "enable": true, "value_area": 800 } },
            "color": { "value": "#808080" },
            "shape": { "type": "circle" },
            "opacity": { "value": 0.5 },
            "size": { "value": 5, "random": true },
            "line_linked": { "enable": true, "distance": 150, "color": "#808080", "opacity": 0.4, "width": 1 },
            "move": { "enable": true, "speed": 6, "out_mode": "out" }
        },
        "interactivity": {
            "detect_on": "canvas",
            "events": { "onhover": { "enable": true, "mode": "repulse" }, "onclick": { "enable": true, "mode": "push" } },
            "modes": { "repulse": { "distance": 200 } }
        },
        "retina_detect": true
    });

    // 5 günlük tahmin için sabit veriler
    const forecastData = [
        { day: "Çarşamba", icon: "01d", max: 27, min: 19 },
        { day: "Perşembe", icon: "03d", max: 26, min: 18 },
        { day: "Cuma", icon: "09d", max: 22, min: 17 },
        { day: "Cumartesi", icon: "10d", max: 24, min: 18 },
        { day: "Pazar", icon: "02d", max: 25, min: 19 }
    ];

    // Sayfa yüklendiğinde 5 günlük sabit tahmin kartlarını oluştur
    const forecastContainer = document.getElementById('forecast-container');
    if (forecastContainer) {
        forecastContainer.innerHTML = ''; // Önceki içeriği temizle
        forecastData.forEach(dayInfo => {
            const forecastCard = document.createElement('div');
            forecastCard.classList.add('col-lg-2', 'col-md-4', 'col-sm-6', 'text-center', 'forecast-card');
            forecastCard.innerHTML = `
                <div class="card-body">
                    <h5>${dayInfo.day}</h5>
                    <img src="https://openweathermap.org/img/wn/${dayInfo.icon}.png" alt="Hava Durumu İkonu">
                    <p><span class="max-temp">${dayInfo.max}°</span> / <span class="min-temp">${dayInfo.min}°</span></p>
                </div>
            `;
            forecastContainer.appendChild(forecastCard);
        });
    }

    // Arama butonuna tıklama olayını dinle
    $("#searchButton").click(function () {
        var city = $("#cityInput").val();
        if (!city) {
            Swal.fire("Lütfen şehir adı girin!", "", "warning");
            return;
        }

        $.ajax({
            url: "/Weather/Search",
            type: "GET",
            data: { city: city },
            success: function (data) {
                // Anlık hava durumu ve 5 günlük tahmin kısımlarını görünür yap
                $("#weather-info").removeClass("d-none");
                $("#forecast-info").removeClass("d-none");
                $("#cityName").text(data.city);
                $("#temperature").text(data.temperature + " °C");
                $("#weatherDescription").text(data.weatherDescription);
                $("#feelsLike").text(data.weatherPerceived + " °C");
                $("#humidity").text(data.humidity + " %");
                $("#windSpeed").text(data.wind + " km/s");
            },
            error: function (xhr) {
                Swal.fire("Hata!", xhr.responseJSON?.message || "Hava durumu alınamadı.", "error");
            }
        });
    });
});









