using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pharmacy.DataAccess.Data;
using Pharmacy.Infra.DTOs;
using Pharmacy.DataAccess.Services;

namespace Pharmacy.Infra.BusinessLogics
{
    public interface IShopLogic
    {
        Task<bool> RegisterOrder(List<OrderItemdto> orderItemdtos, Customer customer);
    }
    public class ShopLogic : IShopLogic
    {
        
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IShoppingCartItemService _shoppingCartItemService;
        private readonly IMedicineService _medicineService;
        private readonly IMedicineInventoryService _medicineInventoryService;
        public ShopLogic(IShoppingCartService shoppingCartService, IShoppingCartItemService shoppingCartItemService, IMedicineService medicineService,
                        IMedicineInventoryService medicineInventoryService)
        {
            _shoppingCartService = shoppingCartService;
            _shoppingCartItemService = shoppingCartItemService;
            _medicineService = medicineService;
            _medicineInventoryService = medicineInventoryService;
        }
        public async Task<bool> RegisterOrder(List<OrderItemdto> orderItemdtos, Customer customer)
        {
            var medicines = await _medicineService.GetAllAsync();
            var shoppingCart = new Shoppingcart()
            {
                CreationDate = DateTime.UtcNow,
                CustomerId = customer.Id,
            };
            shoppingCart = await _shoppingCartService.InsertAsync(shoppingCart);
            if(shoppingCart == null)
                return false;
            var shoppingCartItems = new List<Shoppingcartitem>();
            foreach (var item in orderItemdtos)
            {
                var medicine = medicines.FirstOrDefault(o => o.Name == item.MedicineName);
                if (medicine != null)
                {
                    var shoppingCartItem = new Shoppingcartitem
                    {
                        MedicineId = medicine.Id,
                        ShoppingCartId = shoppingCart.Id,
                        Count = item.ItemCount,
                    };

                    shoppingCartItem = await _shoppingCartItemService.InsertAsync(shoppingCartItem);
                    
                    
                }
            }

            return true;
        }
    }
}