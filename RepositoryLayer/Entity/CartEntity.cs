using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace RepositoryLayer.Entity
{
    public class CartEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartId { get; set; }

        [ForeignKey("AddedBy")]
        public int UserId { get; set; }
    
    [JsonIgnore]
    public virtual UserEntity AddedBy { get; set; }

    [ForeignKey("AddedFor")]
        public int BookId { get; set; }

        [JsonIgnore]
        public virtual BookEntity BookAdded { get; set; }

        public int Quantity { get; set; }

        public bool isPurchased { get; set; }

        public DateTime PurchaseTime { get; set; }
    }
}
