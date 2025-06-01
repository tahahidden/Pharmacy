using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.DataAccess.Data;
using Pharmacy.Infra.DTOs;
using Pharmacy.DataAccess.Services;
using Pharmacy.Infra.BusinessLogics;
using Pharmacy.DataAccess.Exceptions;
using Pharmacy.Infra.Exceptions;

namespace Pharmacy.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly ICustomerLogic _customerLogic;
        public CustomerController(ICustomerService customerService, ICustomerLogic customerLogic)
        {
            _customerService = customerService;
            _customerLogic = customerLogic;
        }

        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromForm] Customerdto customerdto)
        {
            try
            {
                var isSuccess = await _customerLogic.AddCustomer(customerdto);
                if (isSuccess)
                    return Ok();
                else return BadRequest();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (DatabaseException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (DataAccessException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (InfraException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                var customers = await _customerService.GetAllAsync();
                return Ok(customers);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (DatabaseException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (DataAccessException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (InfraException ex)
            {
                return StatusCode(400, ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(400, ex.Message);
            }

        }
    }
}