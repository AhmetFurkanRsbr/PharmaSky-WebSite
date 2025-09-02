using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaSky.Domain.Entities
{
    public class County
    {
        public int CountyId { get; set; }
        public string CountyName { get; set; } // İlçe Adı
        //public ICollection<OnCallPharmacy> OnCallPharmacies { get; set; }

        // Navigation property
        public virtual ICollection<OnCallPharmacy> OnCallPharmacies { get; set; } = new List<OnCallPharmacy>();
    }
}
