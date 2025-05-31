using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Infra.DTOs
{
    public class Medicindto
    {
        public required string Name { get; set; }
        public long? Price { get; set; }
        public int? WarningLevel { get; set; }
    }
}