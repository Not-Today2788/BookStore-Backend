using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IWishListRepository
    {
        public WishListEntity AddtoWishList(int UserId, int BookId);

        public WishListEntity RemoveFromWishList(int UserId, int BookId);

        public List<WishListEntity> GetWishList(int UserId);
    }
}
