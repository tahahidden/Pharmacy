using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.DataAccess.DTOs
{
    public class Medicindto
    {
        public string Name { get; set; }

        public DateTime ExpirationDate { get; set; }

        public int SupplyCount { get; set; }

        public long? Price { get; set; }
    }
}