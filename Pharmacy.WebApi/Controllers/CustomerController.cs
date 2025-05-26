using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.DataAccess.Data;
using Pharmacy.DataAccess.DTOs;
using Pharmacy.DataAccess.Services;

namespace Pharmacy.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("AddCustomer")]
        public async Task<IActionResult> AddCustomer([FromForm] Customerdto customerdto)
        {
            var customerEntity = customerdto.Adapt<Customer>();
            customerEntity = await _customerService.InsertAsync(customerEntity);
            if (customerEntity != null)
                return Ok();
            else return BadRequest();
        }

        [HttpGet("GetAllCustomers")]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerService.GetAllAsync();
            return Ok(customers);
        }
    }
}