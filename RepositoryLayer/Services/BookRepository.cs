using CommonLayer.RequestModels;
using RepositoryLayer.Context;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class BookRepository: IBookRepository
    {
        private readonly BookStoreContext bookContext;
        public BookRepository(BookStoreContext bookContext)
        {
            this.bookContext = bookContext;
        }
        public BookEntity CreateBook(CreateBookModel bookModel)
        {
            BookEntity bookEntity = new BookEntity();
            bookEntity.BookName = bookModel.BookName;
            bookEntity.Description = bookModel.Description;
            bookEntity.Author = bookModel.Author;
            bookEntity.BookImage = bookModel.BookImage;
            bookEntity.Price = bookModel.Price;
            bookEntity.DiscountPrice = bookModel.DiscountPrice;
            bookEntity.Quantity = bookModel.Quantity;
            bookEntity.Rating = bookModel.Rating;
            bookEntity.CreatedAt = DateTime.Now;
            bookEntity.UpdatedAt = DateTime.Now;
            BookEntity user = bookContext.BookTable.FirstOrDefault(a => a.BookName == bookModel.BookName);
            if (user != null)
            {
                throw new Exception("Book Already Exists!!");
            }
            else
            {
                bookContext.BookTable.Add(bookEntity);
                bookContext.SaveChanges();
                return bookEntity;
            }
        }
        public List<BookEntity> GetAllBook()
        {
            return bookContext.BookTable.ToList();

        }
        public List<BookEntity> GetBookId(int id)
        {
            return bookContext.BookTable.Where(a => a.BookId == id).ToList();
        }
        public List<BookEntity> GetBySearch(string author, string bookname)
        {
            return bookContext.BookTable.Where(b => b.Author.Contains(author) || b.BookName.Contains(bookname)).ToList();
        }
        public List<BookEntity> SortByPrice()
        {
            return bookContext.BookTable.OrderBy(a => a.Price).ToList();

        }
        public List<BookEntity> SortByPriceDes()
        {
            return bookContext.BookTable.OrderByDescending(a => a.Price).ToList();

        }
        public List<BookEntity> SortByArrivalAsc()
        {

            return bookContext.BookTable.OrderBy(a => a.CreatedAt).ToList();
        }
        public List<BookEntity> SortByArrivalDes()
        {
            return bookContext.BookTable.OrderByDescending(a => a.CreatedAt).ToList();
        }
    }
}
