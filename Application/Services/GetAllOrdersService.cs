using Application.DTOs;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class GetAllOrdersService : IGetAllOrdersService
    {
        private readonly IOrderQuery query;

        public GetAllOrdersService(IOrderQuery query)
        {
            this.query = query;
        }
        public async Task<IEnumerable<OrderResponse>> GetAllAsync()
        {
            var orders = await query.GetAllAsync();
            return orders.Select(o => new OrderResponse(

                o.OrderId,
                o.DeliveryType.Name,
                o.DeliveryTo,
                o.Status.Name,
                o.Notes,
                o.Price,
                o.CreateDate,
                o.UpdateDate
            ));

        }
    }
}
