using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Pharmacy.DataAccess.Data;
using Pharmacy.DataAccess.Enums;
using Pharmacy.DataAccess.Exceptions;
using Pharmacy.DataAccess.Services;
using Pharmacy.Infra.DTOs;
using Pharmacy.Infra.Exceptions;

namespace Pharmacy.Infra.BusinessLogics
{
    public interface IMedicineLogic
    {
        Task<bool> AddMedicineInventory(DateTime expirationDate, long medicineId, int count);
        Task<List<Medicine>> GetWarningsMedicines(int warn);
        Task<List<Medicine>> GetLessMedicine();
        Task<Medicine> AddMedicine(Medicindto medicinedto);
        Task<List<Medicine>> GetAllMedicines();
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

        public async Task<Medicine> AddMedicine(Medicindto medicinedto)
        {
            try
            {
                var medicineEntity = medicinedto.Adapt<Medicine>();
                medicineEntity = await _medicineService.InsertAsync(medicineEntity);
                if (medicineEntity == null)
                    throw new Exception("save failed");
                return medicineEntity;
            }
            catch (Exception ex)
            {
                throw new InfraException(ex.Message, (int)ExceptionType.UnknownInfra);
            }
        }

        // public async Task<bool> EditMedicine(long medicineId, Medicindto medicindto)
        // {
        //     var medicineEntity = await _medicineService.GetByIdAsync(long medicineId);
        //     var medicine
            
        // }

        public async Task<List<Medicine>> GetAllMedicines()
        {
            try
            {
                var medicines = await _medicineService.GetAllAsync();
                return medicines;
            }
            catch (Exception ex)
            {
                throw new InfraException(ex.Message, (int)ExceptionType.UnknownInfra);
            }
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