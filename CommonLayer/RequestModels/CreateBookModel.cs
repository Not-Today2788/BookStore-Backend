using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.RequestModels
{
    public class CreateBookModel
    {
        public string BookName { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string BookImage { get; set; }
        public float Price { get; set; }
        public float DiscountPrice { get; set; }
        public int Quantity { get; set; }
        public float Rating { get; set; }

    }
}
