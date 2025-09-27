using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record OrderItemResponse(
        long OrderItemId,
        long Order,
        Guid Dish,
        string DishName,
        decimal Price,
        int Quantity,
        string? Notes,
        int Status,
        string StatusName,
        DateTime CreateDate

    );
    
}
