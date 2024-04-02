using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace RepositoryLayer.Entity
{
    public class WishListEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int WishListId { get; set; }
        [ForeignKey("WishListBy")]

        public int UserId { get; set; }
        public virtual UserEntity WishListBy { get; set; }
        [ForeignKey("WishListFor")]
        public int BookId { get; set; }
        public virtual BookEntity WishListFor { get; set; }
    }
}
