using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmaSky.Dto
{
    public class PharmacyDto
    {
        public int PharmacyId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Distance { get; set; }
        public int CountyId { get; set; }
        public string CountyName { get; set; }
    }
}
