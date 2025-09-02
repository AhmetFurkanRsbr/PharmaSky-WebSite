using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaSky.Dto
{
    public class WeatherDto
    {
        public string Name { get; set; }          // şehir ismi
        public double Temp { get; set; }          // sıcaklık
        public int Humidity { get; set; }         // nem
        public string Description { get; set; }
    }
}
