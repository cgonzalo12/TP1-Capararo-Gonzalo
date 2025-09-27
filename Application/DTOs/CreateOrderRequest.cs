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
        public int DeliveyType { get; init; }
        [Required]
        [MaxLength(255)]
        public string? DeliveryTo { get; init; }
        public string? Notes { get; init; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a cero ")]
        public decimal Price { get; init; }

    }
}
