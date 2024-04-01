using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Services
{
    public class CartRepository: ICartRepository
    {
        private readonly BookStoreContext bookContext;
        public CartRepository(BookStoreContext bookContext)
        {
            this.bookContext = bookContext;
        }

        public async Task<CartEntity> AddToCart(int UserId, int BookId)
        {
            var user = await bookContext.UserTable.FirstOrDefaultAsync(x => x.UserId == UserId);
            if (user == null)
            {
                throw new Exception($"User does not exist");
            }
            var book = await bookContext.BookTable.FirstOrDefaultAsync(x => x.BookId == BookId);
            if (book == null)
            {
                throw new Exception($"Book does not exist");
            }

            var bookInCart = await bookContext.CartTable.FirstOrDefaultAsync(x => x.BookId == BookId);
            if (bookInCart != null)
            {
                bookInCart.Quantity++;
                await bookContext.SaveChangesAsync();
                return bookInCart;
            }

            CartEntity entity = new CartEntity();
            entity.UserId = UserId;
            entity.BookId = BookId;
            entity.BookAdded = book;
            entity.AddedBy = user;
            entity.Quantity = 1;
            entity.isPurchased = false;
            //entity.PurchaseTime = null;  

            bookContext.CartTable.Add(entity);
            await bookContext.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> RemovefromCart(int UserId, int BookId)
        {
            var user = await bookContext.UserTable.FirstOrDefaultAsync(x => x.UserId == UserId);
            if (user == null)
            {
                throw new Exception($"User with userId {UserId} does not exist");
            }
            var book = await bookContext.BookTable.FirstOrDefaultAsync(x => x.BookId == BookId);
            if (book == null)
            {
                throw new Exception($"Book with book id {BookId} does not exist");
            }

            var bookInCart = await bookContext.CartTable.FirstOrDefaultAsync(x => x.BookId == BookId);
            if (bookInCart == null)
            {
                throw new Exception("Book is not there in the cart");
            }

            bookContext.CartTable.Remove(bookInCart);
            await bookContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Increase_Decrease(int UserId, int BookId, bool increase)
        {
            var user = await bookContext.UserTable.FirstOrDefaultAsync(x => x.UserId == UserId);
            if (user == null)
            {
                throw new Exception($"User with userId {UserId} does not exist");
            }
            var book = await bookContext.BookTable.FirstOrDefaultAsync(x => x.BookId == BookId);
            if (book == null)
            {
                throw new Exception($"Book with book id {BookId} does not exist");
            }

            var bookInCart = await bookContext.CartTable.FirstOrDefaultAsync(x => x.BookId == BookId);
            if (bookInCart == null)
            {
                throw new Exception("Book is not there in the cart");
            }

            if (increase)
            {
                bookInCart.Quantity++;
            }
            else
            {
                if (bookInCart.Quantity == 1)
                {
                    throw new Exception("Use the remove option now as quantity is 1");
                }
                bookInCart.Quantity--;
            }
            await bookContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<CartEntity>> GetAllItems(int UserId)
        {
            var user = await bookContext.UserTable.FirstOrDefaultAsync(x => x.UserId == UserId);
            if (user == null)
            {
                throw new Exception($"User does not exist");
            }
            var list = await bookContext.CartTable.Where(x => x.UserId == UserId && x.isPurchased == false).ToListAsync();

            return list;
        }

        public async Task<float> TotalCost(int UserId)
        {
            var list = await GetAllItems(UserId);
            float sum = 0;

            foreach (var items in list)
            {
                sum += items.Quantity * items.BookAdded.Price;
            }
            return sum;
        }

        public async Task<bool> PurchaseItems(int UserId, bool paymentdone)
        {
            if (!paymentdone)
            {
                throw new Exception("Something went wrong!");
            }
            var list = await GetAllItems(UserId);
            foreach (var items in list)
            {
                items.isPurchased = true;
            }
            return true;
        }
    }
}
