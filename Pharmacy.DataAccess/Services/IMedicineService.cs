using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pharmacy.DataAccess.Data;
using Microsoft.EntityFrameworkCore;

namespace Pharmacy.DataAccess.Services
{
    public interface IMedicineService
    {
        Task<Medicine?> InsertAsync(Medicine medicine);
        Task<List<Medicine>> GetAllAsync();
        Task<bool> RemoveAsync(Medicine medicine);
        Task<bool> UpdateAsync(Medicine medicine);
        Task<Medicine?> GetProductByIdAsync(long id);
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
            catch
            {
                return null;
            }
           
        }

        public async Task<List<Medicine>> GetAllAsync()
        {
            return await _dbContext.Medicines.AsNoTracking().ToListAsync();
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

        public async Task<Medicine?> GetProductByIdAsync(long id)
        {
            return await _dbContext.Medicines.FindAsync(id);
        }

    }
}