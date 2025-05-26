using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Pharmacy.DataAccess.Data;

namespace Pharmacy.DataAccess.Services
{
    public interface IShoppingCartItemService
    {
        Task<Shoppingcartitem?> InsertAsync(Shoppingcartitem shoppingcartitem);
        Task<List<Shoppingcartitem>> GetAllAsync();
        Task<bool> RemoveAsync(Shoppingcartitem shoppingcartitem);
        Task<bool> UpdateAsync(Shoppingcartitem shoppingcartitem);
        Task<Shoppingcartitem?> GetByIdAsync(long id);
    }

    public class ShoppingCartItemService : IShoppingCartItemService
    {
        private readonly PharmacyContext _dbContext;
        public ShoppingCartItemService(PharmacyContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Shoppingcartitem?> InsertAsync(Shoppingcartitem shoppingcartitem)
        {
            try
            {
                await _dbContext.Shoppingcartitems.AddAsync(shoppingcartitem);
                await _dbContext.SaveChangesAsync();
                return shoppingcartitem;
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> RemoveAsync(Shoppingcartitem shoppingcartitem)
        {
            try
            {
                _dbContext.Shoppingcartitems.Remove(shoppingcartitem);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public async Task<Shoppingcartitem?> GetByIdAsync(long id)
        {
            return await _dbContext.Shoppingcartitems.FindAsync(id);
        }

        public async Task<List<Shoppingcartitem>> GetAllAsync()
        {
            return await _dbContext.Shoppingcartitems.AsNoTracking().ToListAsync();
        }

        public async Task<bool> UpdateAsync(Shoppingcartitem shoppingcartitem)
        {
            try
            {

                _dbContext.Shoppingcartitems.Update(shoppingcartitem);
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