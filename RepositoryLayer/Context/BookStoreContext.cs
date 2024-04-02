using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Context
{
    public class BookStoreContext: DbContext
    {
        public BookStoreContext(DbContextOptions options) : base(options)
        { }

        public DbSet<UserEntity> UserTable { get; set; }
        public DbSet<BookEntity> BookTable { get; set; }        
        public DbSet<CartEntity> CartTable { get; set; }
        public DbSet<WishListEntity> WishListTable { get; set;}
    }
}
