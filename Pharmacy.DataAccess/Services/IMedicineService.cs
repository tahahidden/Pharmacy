using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pharmacy.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Pharmacy.DataAccess.Exceptions;
using Pharmacy.DataAccess.Enums;

namespace Pharmacy.DataAccess.Services
{
    public interface IMedicineService
    {
        Task<Medicine?> InsertAsync(Medicine medicine);
        Task<List<Medicine>> GetAllAsync();
        Task<bool> RemoveAsync(Medicine medicine);
        Task<bool> UpdateAsync(Medicine medicine);
        Task<Medicine?> GetByIdAsync(long id);
        Task<Medicine?> GetByNameAsync(string name);
    }

    public class MedicineService : IMedicineService
    {
        private readonly PharmacyContext _dbContext;

        public MedicineService(PharmacyContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Medicine?> InsertAsync(Medicine medicine)
        {
            try
            {
                await _dbContext.Medicines.AddAsync(medicine);
                await _dbContext.SaveChangesAsync();
                return medicine;
            }
            catch (DbUpdateException ex)
            {
                throw new DatabaseException(ex.Message, (int)ExceptionType.InsertItemToDatabase);
            }
            catch(Exception ex)
            {
                throw new DataAccessException(ex.Message, (int)ExceptionType.UnknownDataAccess);
            }

        }

        public async Task<List<Medicine>> GetAllAsync()
        {
            try
            {
                return await _dbContext.Medicines.AsNoTracking().ToListAsync();
            }
            catch
            {
                throw new Exception("");
            }
        }

        public async Task<bool> UpdateAsync(Medicine medicine)
        {
            try
            {

                _dbContext.Medicines.Update(medicine);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> RemoveAsync(Medicine medicine)
        {
            try
            {
                _dbContext.Medicines.Remove(medicine);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Medicine?> GetByIdAsync(long id)
        {
            var med = await _dbContext.Medicines.FindAsync(id);
            if (med == null)
                throw new NotFoundException("item not found", (int)ExceptionType.ItemNotFound);
            return med;
        }

        public async Task<Medicine?> GetByNameAsync(string name)
        {
            return await _dbContext.Medicines.FirstOrDefaultAsync(o => o.Name == name);
        }

    }
}