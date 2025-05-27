using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.DataAccess.DTOs;
using Pharmacy.DataAccess.Services;
using Pharmacy.Infra.BusinessLogics;

namespace Pharmacy.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShoppingController : ControllerBase
    {
        private readonly IShopLogic _shopLogic;
        private readonly ICustomerService _customerService;
        public ShoppingController(IShopLogic shopLogic, ICustomerService customerService)
        {
            _shopLogic = shopLogic;
            _customerService = customerService;
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> RegisterOrder([FromBody] List<OrderItemdto> orderItemdtos, [FromQuery(Name = "national-code")] string nationalCode)
        {
            var customer = await _customerService.GetByNationalCodeASync(nationalCode.Trim());
            if (customer != null)
            {
                var isOrderRegistered = await _shopLogic.RegisterOrder(orderItemdtos, customer);
                return Ok();
            }
            else
            {
                return NotFound("National code not found!");
            }
        }
    }
}