using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessEntities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0;
        public string Model { get; set; }
        public string Color { get; set; }
        public string Category { get; set; }
        public string Imageurl { get; set; }
    }
}