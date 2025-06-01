using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mapster;
using Pharmacy.DataAccess.Data;
using Pharmacy.DataAccess.Enums;
using Pharmacy.DataAccess.Services;
using Pharmacy.Infra.DTOs;
using Pharmacy.Infra.Exceptions;

namespace Pharmacy.Infra.BusinessLogics
{
    public interface ICustomerLogic
    {
        Task<bool> AddCustomer(Customerdto customerdto);
    }

    public class CustomerLogic : ICustomerLogic
    {
        private readonly ICustomerService _customerService;

        public CustomerLogic(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public async Task<bool> AddCustomer(Customerdto customerdto)
        {
            try
            {
                var customerEntity = customerdto.Adapt<Customer>();
                customerEntity = await _customerService.InsertAsync(customerEntity);
                return true;
            }
            catch(Exception ex)
            {
                throw new InfraException(ex.Message, (int)ExceptionType.UnknownInfra);
            }
        }
    }
}