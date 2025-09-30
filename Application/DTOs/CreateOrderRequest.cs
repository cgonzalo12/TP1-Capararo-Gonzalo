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
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un tipo de entrega válido")]
        public int DeliveyType { get; init; }
        [Required]
        [MaxLength(255)]
        public string? DeliveryTo { get; init; }
        [MaxLength(500)]
        public string? Notes { get; init; }
        
        public IEnumerable<CreateOrderItemRequest> Items { get; init; } = new List<CreateOrderItemRequest>();


    }
}
