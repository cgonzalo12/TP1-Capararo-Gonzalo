using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class CreateStatusRequest
    {
        [Required]
        [MaxLength(25)]
        public string? Name { get; init; }

    }
}
