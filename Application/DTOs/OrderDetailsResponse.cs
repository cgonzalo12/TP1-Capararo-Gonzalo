using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record OrderDetailsResponse(
        long OrderNumber,
        double TotalAmount,
        string? DeliveryTo,
        string? Notes,
        GenericResponce Status,
        GenericResponce DeliveryType,
        List<OrderItemResponse> Items,
        DateTime CreateAt,
        DateTime UpdateAt
    );


}
