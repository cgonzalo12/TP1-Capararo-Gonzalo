using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public record CreateOrderResponce(
        long orderNumber,
        decimal totalAmount,
        DateTime createdAT
    );
}
