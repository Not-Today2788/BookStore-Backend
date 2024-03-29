using CommonLayer.RequestModels;
using ManagerLayer.Interfaces;
using RepositoryLayer.Entity;
using RepositoryLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ManagerLayer.Services
{
    public class BookManager: IBookManager
    {
        private readonly IBookRepository bookRepository;
        public BookManager(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }
        public BookEntity CreateBook(CreateBookModel bookModel)
        {
            return bookRepository.CreateBook(bookModel);
        }
        public List<BookEntity> GetAllBook()
        {
            return bookRepository.GetAllBook();
        }
        public List<BookEntity> GetBookId(int id)
        {
            return bookRepository.GetBookId(id);
        }
        public List<BookEntity> GetBySearch(string author, string bookname)
        {
            return bookRepository.GetBySearch(author, bookname);
        }
        public List<BookEntity> SortByPrice()
        {
            return bookRepository.SortByPrice();
        }
        public List<BookEntity> SortByPriceDes()
        {
            return bookRepository.SortByPriceDes();
        }
        public List<BookEntity> SortByArrivalAsc()
        {
            return bookRepository.SortByArrivalAsc();
        }
        public List<BookEntity> SortByArrivalDes()
        {
            return bookRepository.SortByArrivalDes();
        }

    }
}
