using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Order
    {
        public long OrderId { get; set; }
        public string DeliveryTo { get; set; }= null!;
        public string? Notes { get; set; }
        public decimal Price { get; set; }

        //timestamp
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        // Foreign key + Navigation property
        public int DeliveryType { get; set; }
        public DeliveryType DeliveryTypeNav { get; set; } = null!;

        public int OverallStatus { get; set; }
        public Status? StatusNav { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
