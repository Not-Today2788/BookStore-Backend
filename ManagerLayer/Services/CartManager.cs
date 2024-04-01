using ManagerLayer.Interfaces;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Services
{
    public class CartManager: ICartManager
    {
        private readonly ICartRepository cart;
        public CartManager(ICartRepository cart)
        {
            this.cart = cart;
        }
        public async Task<CartEntity> AddToCart(int UserId, int BookId)
        {
            return await cart.AddToCart(UserId, BookId);
        }
        public async Task<bool> RemovefromCart(int UserId, int BookId)
        {
            return await cart.RemovefromCart(UserId, BookId);
        }
        public async Task<bool> Increase_Decrease(int UserId, int BookId, bool increase)
        {
            return await cart.Increase_Decrease(UserId, BookId, increase);
        }
        public async Task<List<CartEntity>> GetAllItems(int UserId)
        {
            return await cart.GetAllItems(UserId);
        }
        public async Task<bool> PurchaseItems(int UserId, bool paymentdone)
        {
            return await cart.PurchaseItems(UserId, paymentdone);
        }
        public async Task<float> TotalCost(int UserId)
        {
            return await cart.TotalCost(UserId);
        }
    }
}
