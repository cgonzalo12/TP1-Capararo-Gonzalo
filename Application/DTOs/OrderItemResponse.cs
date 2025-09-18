using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record OrderItemResponse(
        long OrderItemId,
        long OrderId,
        Guid DishId,
        string DishName,
        decimal Price,
        int Quantity,
        string? Notes,
        int StatusId,
        string StatusName,
        DateTime CreateDate

    );
    
}
