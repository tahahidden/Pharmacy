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
        Task<bool> AddMedicineInventory(DateTime expirationDate, long medicineId, int count);
        Task<List<Medicine>> GetWarningsMedicines(int warn);
        Task<List<Medicine>> GetLessMedicine();
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

        public async Task<bool> AddMedicineInventory(DateTime expirationDate, long medicineId, int count)
        {
            var medicineInventory = await _medicineInventoryService.GetByMedicineIdAndExpirationDate(medicineId, expirationDate);

            if (medicineInventory != null)
            {
                medicineInventory.Count += count;
                var isUpdateSuccess = await _medicineInventoryService.UpdateAsync(medicineInventory);
                if (isUpdateSuccess)
                    return true;
                else return false;
            }
            else
            {
                medicineInventory = new Medicinesinventory()
                {
                    Count = count,
                    ExpirationDate = expirationDate,
                    MedicineId = medicineId
                };

                medicineInventory = await _medicineInventoryService.InsertAsync(medicineInventory);
                if (medicineInventory != null)
                    return true;
                else return false;

            }

        }

        public async Task<List<Medicine>> GetWarningsMedicines(int warn)
        {
            var medicines = await _medicineService.GetAllAsync();
            var medicinesInventory = await _medicineInventoryService.GetAllAsync();

            var warnMedicines = new List<Medicine>();

            foreach (var medicine in medicines)
            {
                if (medicine.WarningLevel == null) continue;
                var inventory = medicinesInventory.Where(o => o.MedicineId == medicine.Id).Sum(o => o.Count);
                if (inventory <= (medicine.WarningLevel + warn) && inventory >= warn)
                    warnMedicines.Add(medicine);
            }
            return warnMedicines;
        }

        public async Task<List<Medicine>> GetLessMedicine()
        {
            var medicines = await _medicineService.GetAllAsync();
            var medicinesInventory = await _medicineInventoryService.GetAllAsync();

            var lessMedicines = new List<Medicine>();

              foreach (var medicine in medicines)
            {
                if (medicine.WarningLevel == null) continue;
                var inventory = medicinesInventory.Where(o => o.MedicineId == medicine.Id).Sum(o => o.Count);
                if (inventory <= medicine.WarningLevel)
                    lessMedicines.Add(medicine);
            }
            return lessMedicines;
        }
    }
}