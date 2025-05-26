using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pharmacy.DataAccess.Data;

namespace Pharmacy.DataAccess.DTOs
{
    public interface IMedicinesInventory
    {

    }

    public class MedicinesInventory : IMedicinesInventory
    {
        private readonly PharmacyContext _dbContext;
        public MedicinesInventory(PharmacyContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}