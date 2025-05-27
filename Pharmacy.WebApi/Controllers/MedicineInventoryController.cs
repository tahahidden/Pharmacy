using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.DataAccess.DTOs;
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

        [HttpPost("[action]")]
        public async Task<IActionResult> AddMedicineInventory([FromQuery(Name = "expiration-string")] string expirationString,[FromQuery(Name = "medicine-name")] string medicineName, int count)
        {
            var medicine = await _medicineService.GetByNameAsync(medicineName);
            if (medicine != null)
            {
                var expirationPersianDate = PersianDateTime.Parse(expirationString);
                var isAddInventorySuccess = await _medicineLogic.AddMedicineInventory(expirationPersianDate.ToDateTime(), medicine.Id, count);
                if (isAddInventorySuccess)
                    return Ok();
                else return BadRequest();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetLessMedicine()
        {
            var lessMedicines = await _medicineLogic.GetLessMedicine();
            return Ok(lessMedicines);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetWarningsMedicine(int warn)
        {
            var warningMedicines = await _medicineLogic.GetWarningsMedicines(warn);
            return Ok(warningMedicines);
        }
    }
}