using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PharmaSky.Domain.Entities
{
    public class OnCallPharmacy
    {
        public int PharmacyId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int CountyId { get; set; } // İlçeId
        [JsonIgnore]   // Burası önemli
        public virtual County County { get; set; } //İlçe
        public string Distance { get; set; } // Mesafe
    }
}
