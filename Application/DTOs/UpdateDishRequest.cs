using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UpdateDishRequest
    {
        [MaxLength(255)]
        public string? Name { get; init; }
        [MaxLength(500)]
        public string? Description { get; init; }
        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a cero ")]
        public decimal? Price { get; init; }
        public bool? Available { get; init; }
        public int? CategoryId { get; init; }
        public string? Image { get; init; }
       
    }
}
