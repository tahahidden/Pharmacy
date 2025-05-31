using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.DataAccess.Services;
using Pharmacy.Infra.DTOs;
using Mapster;
using Pharmacy.DataAccess.Data;
using Pharmacy.DataAccess.Exceptions;
using Pharmacy.Infra.BusinessLogics;
using Pharmacy.Infra.Exceptions;

namespace Pharmacy.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class MedicineController : ControllerBase
    {
        private readonly IMedicineService _medicineService;
        private readonly IMedicineLogic _medicineLogic;

        public MedicineController(IMedicineService medicineService, IMedicineLogic medicineLogic)
        {
            _medicineService = medicineService;
            _medicineLogic = medicineLogic;
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


            try
            {
                var medicineEntity = await _medicineLogic.AddMedicine(medicindto);
                return Ok(medicineEntity);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        public async Task<IActionResult> EditMedicine([FromQuery(Name = "medicine-id")] long medicineId, [FromBody] Medicindto medicindto)
        {
            try
            {
                var medicine = await _medicineService.GetByIdAsync(medicineId);
                return Ok(medicine);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
             catch (DatabaseException ex)
            {
                return StatusCode(404, ex.Message);
            } catch (DataAccessException ex)
            {
                return StatusCode(404, ex.Message);
            } catch (InfraException ex)
            {
                return StatusCode(404, ex.Message);
            } catch (Exception ex)
            {
                return StatusCode(404, ex.Message);
            }
        }
    }
}