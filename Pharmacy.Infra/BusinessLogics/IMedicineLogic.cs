using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pharmacy.DataAccess.Data;
using Pharmacy.DataAccess.Services;

namespace Pharmacy.Infra.BusinessLogics
{
    public interface IMedicineLogic
    {

    }

    public class MedicineLogic : IMedicineLogic
    {
        private readonly IMedicineService _medicineService;
        private readonly IMedicineInventoryService _medicineInventoryService;
        public MedicineLogic(IMedicineService medicineService, IMedicineInventoryService medicineInventoryService)
        {
            _medicineService = medicineService;
            _medicineInventoryService = medicineInventoryService;
        }

        public async Task GetWarningsMedicines(int near)
        {
            var medicinesInventory = await _medicineInventoryService.GetAllAsync();

            
        }
    }
}