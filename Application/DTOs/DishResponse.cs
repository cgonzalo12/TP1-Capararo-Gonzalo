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
        GenericResponce Category,
        string? Image,
        bool isActive,
        DateTime CreateDate,
        DateTime UpdateDate
    );
}
