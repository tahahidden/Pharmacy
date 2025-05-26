using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pharmacy.DataAccess.Data;

namespace Pharmacy.DataAccess.DTOs
{
    public class MedicinesInventory
    {
        public long MedicineId { get; set; }

        public long? Count { get; set; }

        public DateTime ExpirationDate { get; set; }
    }
}
