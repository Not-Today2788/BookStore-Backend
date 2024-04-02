using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class WishListRepository : IWishListRepository
    {
        private readonly BookStoreContext context;
        
        public WishListRepository(BookStoreContext context)
        {
            this.context = context;
        }

        public WishListEntity AddtoWishList(int UserId, int BookId)
        {
            var existingbook = context.WishListTable.FirstOrDefault(x => x.UserId == UserId && x.BookId == BookId);
            if (existingbook == null)
            {
                WishListEntity book = new WishListEntity();
                book.UserId = UserId;
                book.BookId = BookId;
                context.WishListTable.Add(book);
                context.SaveChanges();
                return book;
            }
            throw new Exception("This book is already in the wishList");
        }


        public WishListEntity RemoveFromWishList(int UserId, int BookId)
        {
            var existingbook = context.WishListTable.FirstOrDefault(x => x.UserId == UserId && x.BookId == BookId);
            if (existingbook != null)
            {
                context.WishListTable.Remove(existingbook);
                context.SaveChanges();
                return existingbook;
            }
            throw new Exception("This book does not exist in the wishList");
        }

        public List<WishListEntity> GetWishList(int UserId)
        {
            List<WishListEntity> wishlist = context.WishListTable.Where(x => x.UserId == UserId).ToList();
            return wishlist;
        }

    }
}
