using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pharmacy.DataAccess.Data;

namespace Pharmacy.DataAccess.Services
{
    public interface IMedicineInventoryService
    {
        Task<Medicinesinventory?> InsertAsync(Medicinesinventory medicinesinventory);
        Task<List<Medicinesinventory>> GetAllAsync();
        Task<bool> RemoveAsync(Medicinesinventory medicinesinventory);
        Task<bool> UpdateAsync(Medicinesinventory medicinesinventory);
        Task<Medicinesinventory?> GetByIdAsync(long id);
        Task<Medicinesinventory?> GetByMedicineIdAndExpirationDate(long medicineId, DateTime expirationDate);
    }

    public class MedicineInventoryService : IMedicineInventoryService
    {
        private readonly PharmacyContext _dbContext;
        public MedicineInventoryService(PharmacyContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Medicinesinventory?> InsertAsync(Medicinesinventory medicinesinventory)
        {
            try
            {
                await _dbContext.Medicinesinventories.AddAsync(medicinesinventory);
                await _dbContext.SaveChangesAsync();
                return medicinesinventory;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> RemoveAsync(Medicinesinventory medicinesinventory)
        {
            try
            {
                _dbContext.Medicinesinventories.Remove(medicinesinventory);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<Medicinesinventory?> GetByIdAsync(long id)
        {
            return await _dbContext.Medicinesinventories.FindAsync(id);
        }

        public async Task<List<Medicinesinventory>> GetAllAsync()
        {
            return await _dbContext.Medicinesinventories.AsNoTracking().ToListAsync();
        }

        public async Task<bool> UpdateAsync(Medicinesinventory medicinesinventory)
        {
            try
            {

                _dbContext.Medicinesinventories.Update(medicinesinventory);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<Medicinesinventory?> GetByMedicineIdAndExpirationDate(long medicineId, DateTime expirationDate)
        {
            return await _dbContext.Medicinesinventories.FirstOrDefaultAsync(o => o.MedicineId == medicineId
                && o.ExpirationDate.Year == expirationDate.Year && o.ExpirationDate.Month == expirationDate.Month
                && o.ExpirationDate.Day == expirationDate.Day);
        }
    }
}