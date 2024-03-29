using CommonLayer.RequestModels;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interfaces
{
    public interface IBookRepository
    {
        public BookEntity CreateBook(CreateBookModel bookModel);
        public List<BookEntity> GetAllBook();
        public List<BookEntity> GetBookId(int id);
        public List<BookEntity> GetBySearch(string author, string bookname);
        public List<BookEntity> SortByPrice();
        public List<BookEntity> SortByPriceDes();
        public List<BookEntity> SortByArrivalAsc();
        public List<BookEntity> SortByArrivalDes();
    }
}
