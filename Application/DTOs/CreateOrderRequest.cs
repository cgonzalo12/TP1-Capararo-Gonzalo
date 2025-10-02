using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CreateOrderRequest
    {
        [Required]
        public Delivery Delivery { get; set; } = null!;
        [MaxLength(500)]
        public string? Notes { get; init; }
        
        public IEnumerable<Items> Items { get; init; } = new List<Items>();


    }
}
