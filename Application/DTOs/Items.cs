using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class Items
    {

        [Required]
        public Guid Dish { get; init; }
        [Required]
        public int Quantity { get; init; }
        public string? Notes { get; init; }

    }
}
