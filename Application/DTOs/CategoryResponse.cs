using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record CategoryResponse(
            int Id,
            string? Name,
            string? Description,
            int Order
    );
}
