using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CreateCategoryRequest
    {
        [Required]
        [MaxLength(25)]
        public string? Name { get; init; }
        [Required]
        [MaxLength(255)]
        public string? Description { get; init; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "El campo Order debe ser un número entero mayor o igual a 1.")]
        public int Order { get; init; }
    }
}
