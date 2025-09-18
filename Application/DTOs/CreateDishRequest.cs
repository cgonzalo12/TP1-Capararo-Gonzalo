using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CreateDishRequest
    {
        [Required,MaxLength(255)]
        public string Name { get; init; }=default!;
        [MaxLength(500)]
        public string? Description { get; init; }
        [Required]
        [Range(0.01, double.MaxValue,ErrorMessage ="El precio debe ser mayor a cero ")]
        public decimal Price { get; init; }
        [Required]
        public int CategoryId { get; init; }
        public string? Image { get; init; }
    }
}
