using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record OrderItemResponse(
        long OrderItemId,
        int Quantity,
        string? Notes,
        StatusResponce Status,
        DishByOrderItemResponce Dish
    );
    
}
