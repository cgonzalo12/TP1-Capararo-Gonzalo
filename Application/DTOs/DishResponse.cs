using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record DishResponse(
        Guid DishId,
        string Name,
        string? Description,
        decimal Price,
        bool IsAvailable,
        string? Image,
        int CategoryId,
        string? CategoryName,
        DateTime CreateDate,
        DateTime UpdateDate
    );
}
