using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.DataAccess.Services;
using Pharmacy.Infra.BusinessLogics;

namespace Pharmacy.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicineInventoryController : ControllerBase
    {
        private readonly IMedicineService _medicineService;
        private readonly IMedicineInventoryService _medicineInventoryService;

        private readonly IMedicineLogic _medicineLogic;
        public MedicineInventoryController(IMedicineService medicineService, IMedicineInventoryService medicineInventoryService,
            IMedicineLogic medicineLogic)
        {
            _medicineService = medicineService;
            _medicineInventoryService = medicineInventoryService;
            _medicineLogic = medicineLogic;
        }

        [HttpGet("AddMedicineInventory")]
        public async Task AddMedicineInventory(string expirationString, string medicineName, int count)
        {

            var medicine = await _medicineService.GetByNameAsync(medicineName);
            var expirationPersianDate = PersianDateTime.Parse(expirationString);
            await _medicineLogic.AddMedicineInventory(new DateTime());
            //var medicineInventory = await _medicineInventoryService.

        }
    }
}