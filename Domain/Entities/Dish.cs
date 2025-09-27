using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Dish
    {
        public Guid DishId { get; set; }
        public string Name { get; set; }=null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public bool Available { get; set; }
        public string? ImageUrl { get; set; }

        // timestamp
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        // Foreign key + Navigation property
        public int Category { get; set; }
        public Category CategoryNav { get; set; }= null!;

        //OrderItem
        public ICollection<OrderItem> OrdersItems { get; set; } = new List<OrderItem>();

    }
}
