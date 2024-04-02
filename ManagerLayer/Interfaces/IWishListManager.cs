using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Interfaces
{
    public interface IWishListManager
    {
        public WishListEntity AddtoWishList(int UserId, int BookId);

        public WishListEntity RemoveFromWishList(int UserId, int BookId);

        public List<WishListEntity> GetWishList(int UserId);
    }
}
