using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record OrderResponse(
        long OrderNumber,
        decimal TotalAmount,
        string DeliveryTo,
        string? Notes,
        StatusResponce Status,
        DeliveryTypeResponce DeliveryType,
        List<OrderItemResponse> Items,
        DateTime CreateAt,
        DateTime? UpdateAt
    );


}
