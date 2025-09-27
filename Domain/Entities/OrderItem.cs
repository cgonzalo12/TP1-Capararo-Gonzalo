using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class OrderItem
    {
        public long OrderItemId { get; set; }
        public int Quantity { get; set; }
        public string? Notes { get; set; }
        //
        public DateTime CreateDate { get; set; }
        // Foreign key + Navigation property
        public Guid Dish { get; set; }
        public Dish DishNav { get; set; } = null!;
        public long Order { get; set; }
        public Order OrderNav { get; set; } = null!;
        public int  Status { get; set; }
        public Status? StatusNav { get; set; }

    }
}
