using Application.DTOs;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class GetAllOrderItemService: IGetAllOrderItemService
    {
        private readonly IOrderItemQuery query;

        public GetAllOrderItemService(IOrderItemQuery query)
        {
            this.query = query;
        }

        public async Task<IEnumerable<OrderItemResponse>> GetAllAsync()
        {
            var orderItems = await query.GetAllAsync();
            return orderItems.Select(oi => new OrderItemResponse(
                oi.OrderItemId,
                oi.Order,
                oi.Dish,
                oi.DishNav.Name,
                oi.DishNav.Price,
                oi.Quantity,
                oi.Notes,
                oi.Status,
                oi.StatusNav!.Name!,
                oi.CreateDate
            ));
        }
    }
}
