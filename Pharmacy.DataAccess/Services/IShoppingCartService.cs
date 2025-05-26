using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pharmacy.DataAccess.Data;

namespace Pharmacy.DataAccess.Services
{
    public interface IShoppingCartService
    {
        Task<Shoppingcart?> InsertAsync(Shoppingcart shoppingcart);
        Task<List<Shoppingcart>> GetAllAsync();
        Task<bool> RemoveAsync(Shoppingcart shoppingcart);
        Task<bool> UpdateAsync(Shoppingcart shoppingcart);
        Task<Shoppingcart?> GetByIdAsync(long id);
    }

    public class ShoppingCartService : IShoppingCartService
    {
        private readonly PharmacyContext _dbContext;
        public ShoppingCartService(PharmacyContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Shoppingcart?> InsertAsync(Shoppingcart shoppingcart)
        {
            try
            {
                await _dbContext.Shoppingcarts.AddAsync(shoppingcart);
                await _dbContext.SaveChangesAsync();
                return shoppingcart;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> RemoveAsync(Shoppingcart shoppingcart)
        {
            try
            {
                _dbContext.Shoppingcarts.Remove(shoppingcart);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<Shoppingcart?> GetByIdAsync(long id)
        {
            return await _dbContext.Shoppingcarts.FindAsync(id);
        }

        public async Task<List<Shoppingcart>> GetAllAsync()
        {
            return await _dbContext.Shoppingcarts.AsNoTracking().ToListAsync();
        }

        public async Task<bool> UpdateAsync(Shoppingcart shoppingcart)
        {
            try
            {

                _dbContext.Shoppingcarts.Update(shoppingcart);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}