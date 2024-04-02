using ManagerLayer.Interfaces;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Services
{
    public class WishListManager:IWishListManager
    {
        private readonly IWishListRepository repository;
        public WishListManager(IWishListRepository repository)
        {
            this.repository = repository;
        }
        public WishListEntity AddtoWishList(int UserId, int BookId)
        {
            return repository.AddtoWishList(UserId, BookId);
        }
        public WishListEntity RemoveFromWishList(int UserId, int BookId)
        {
            return repository.RemoveFromWishList(UserId, BookId);
        }
        public List<WishListEntity> GetWishList(int UserId)
        {
            return repository.GetWishList(UserId);
        }
    }
}
