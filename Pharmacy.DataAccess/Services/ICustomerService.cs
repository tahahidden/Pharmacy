using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pharmacy.DataAccess.Data;
using Pharmacy.DataAccess.Enums;
using Pharmacy.DataAccess.Exceptions;

namespace Pharmacy.DataAccess.Services
{
    public interface ICustomerService
    {
        Task<Customer?> InsertAsync(Customer customer);
        Task<List<Customer>> GetAllAsync();
        Task<bool> RemoveAsync(Customer customer);
        Task<bool> UpdateAsync(Customer customer);
        Task<Customer?> GetByIdAsync(long id);
        Task<Customer?> GetByNationalCodeASync(string nationalCode);
    }

    public class CustomerService : ICustomerService
    {
        private readonly PharmacyContext _dbContext;
        public CustomerService(PharmacyContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Customer?> InsertAsync(Customer customer)
        {
            try
            {
                await _dbContext.Customers.AddAsync(customer);
                await _dbContext.SaveChangesAsync();
                return customer;
            }
            catch (DbUpdateException ex)
            {
                throw new DatabaseException(ex.Message, (int)ExceptionType.InsertItemToDatabase);
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex.Message, (int)ExceptionType.UnknownDataAccess);
            }
        }

        public async Task<bool> RemoveAsync(Customer customer)
        {
            try
            {
                _dbContext.Customers.Remove(customer);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                throw new DatabaseException(ex.Message, (int)ExceptionType.InsertItemToDatabase);
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex.Message, (int)ExceptionType.UnknownDataAccess);
            }
        }
        public async Task<Customer?> GetByIdAsync(long id)
        {
            return await _dbContext.Customers.FindAsync(id);
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            try
            {
                var customers = await _dbContext.Customers.AsNoTracking().ToListAsync();
                return customers;
            }
            catch (DbUpdateException ex)
            {
                throw new DatabaseException(ex.Message, (int)ExceptionType.InsertItemToDatabase);
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex.Message, (int)ExceptionType.UnknownDataAccess);
            }
        }

        public async Task<bool> UpdateAsync(Customer customer)
        {
            try
            {

                _dbContext.Customers.Update(customer);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException ex)
            {
                throw new DatabaseException(ex.Message, (int)ExceptionType.InsertItemToDatabase);
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex.Message, (int)ExceptionType.UnknownDataAccess);
            }
        }

        public async Task<Customer?> GetByNationalCodeASync(string nationalCode)
        {
            return await _dbContext.Customers.FirstOrDefaultAsync(o => o.NationalCode == nationalCode);
        }
    }
}