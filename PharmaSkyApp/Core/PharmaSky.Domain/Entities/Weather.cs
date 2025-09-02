using PharmaSky.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaSky.Domain.Entities
{
    public class Weather
    {
        public int WeatherId { get; set; }
        public string City { get; set; }
        public int Temperature { get; set; } // Derece
        public string WeatherDescription { get; set; } // Hava durumu tanımlaması
        public string WeatherPerceived { get; set; } // Hissedilen
        public int Humidity { get; set; } // Nem
        public string Wind { get; set; } // Rüzgar
        public DateTime Date { get; set; }
        public EDays Day { get; set; }
    }
}
