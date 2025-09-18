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
        public int DeliveryTypeId { get; set; }
        public DeliveryType DeliveryType { get; set; } = null!;

        public int OverallStatusId { get; set; }
        public Status? Status { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
