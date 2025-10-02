using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class DishUpdateRequest
    {
        
        [MaxLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres")]
        public string? Name { get; init; }

        [MaxLength(500, ErrorMessage = "La descripción no puede tener más de 500 caracteres")]
        public string? Description { get; init; }

        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a cero")]
        public decimal? Price { get; init; }

        public int? Category { get; init; }

        [Url(ErrorMessage = "La imagen debe ser una URL válida")]
        public string? Image { get; init; }

        public bool? IsActive { get; init; }

    }
}
