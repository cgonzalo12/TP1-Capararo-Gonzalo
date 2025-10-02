using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record OrderItemResponse(
        long Id,
        int Quantity,
        string? Notes,
        GenericResponce Status,
        DishShortResponce Dish
    );
    
}
