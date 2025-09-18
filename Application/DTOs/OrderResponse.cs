using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record OrderResponse(
        long OrderId,
        string DeliveryType,
        string DeliveryTo,
        string OverallStatus,
        string? Notes,
        decimal Price,
        DateTime CreateDate,
        DateTime? UpdateDate
    );


}
