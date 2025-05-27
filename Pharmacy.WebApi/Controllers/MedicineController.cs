using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.DataAccess.Services;
using Pharmacy.DataAccess.DTOs;
using Mapster;
using Pharmacy.DataAccess.Data;

namespace Pharmacy.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class MedicineController : ControllerBase
    {
        private readonly IMedicineService _medicineService;

        public MedicineController(IMedicineService medicineService)
        {
            _medicineService = medicineService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllMedicines()
        {
            var medicines = await _medicineService.GetAllAsync();
            return Ok(medicines);
        }

        [HttpPost()]
        public async Task<IActionResult> AddMedicine([FromForm] Medicindto medicindto)
        {
            var medicineEntity = medicindto.Adapt<Medicine>();
            medicineEntity = await _medicineService.InsertAsync(medicineEntity);

            if (medicineEntity != null)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }

        }
    }
}