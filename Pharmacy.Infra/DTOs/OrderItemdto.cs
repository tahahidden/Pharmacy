using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Infra.DTOs
{
    public class OrderItemdto
    {
        public required string MedicineName { get; set; } 
        public required int ItemCount{ get; set; }
    }
}