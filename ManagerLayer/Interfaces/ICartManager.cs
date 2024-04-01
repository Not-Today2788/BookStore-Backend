using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ManagerLayer.Interfaces
{
    public interface ICartManager
    {
        Task<CartEntity> AddToCart(int UserId, int BookId);
        Task<bool> RemovefromCart(int UserId, int BookId);
        Task<bool> Increase_Decrease(int UserId, int BookId, bool increase);
        Task<List<CartEntity>> GetAllItems(int UserId);
        Task<bool> PurchaseItems(int UserId, bool paymentdone);
        Task<float> TotalCost(int UserId);
    }
}
