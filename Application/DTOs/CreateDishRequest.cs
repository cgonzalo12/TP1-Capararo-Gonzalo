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
        [Required(ErrorMessage ="El nombre del plato es obligartorio")]
        [MaxLength(255,ErrorMessage ="El nombre no puede tener mas de 255 caracteres")]
        public string Name { get; init; }=default!;
        [MaxLength(500)]
        public string? Description { get; init; }
        [Required(ErrorMessage = "El precio del plato es obligartorio")]
        [Range(0.01, double.MaxValue,ErrorMessage ="El precio debe ser mayor a cero ")]
        public decimal Price { get; init; }
        [Required(ErrorMessage = "La categoria del plato es obligartorio")]
        public int Category { get; init; }
        public string? Image { get; init; }
    }
}
