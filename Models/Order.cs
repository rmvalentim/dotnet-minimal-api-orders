
using System;
using System.Collections.Generic;

namespace MinimalApis.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public List<Product> Products { get; set; } = new();
    }
}
