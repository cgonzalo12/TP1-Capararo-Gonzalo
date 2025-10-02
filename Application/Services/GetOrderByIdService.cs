using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class GetOrderByIdService : IGetOrderByIdService
    {
        private readonly IOrderQuery query;
        private readonly IStatusQuery statusQuery;
        private readonly IDishQuery dishQuery;

        public GetOrderByIdService(IOrderQuery query,IStatusQuery statusQuery,IDishQuery dishQuery)
        {
            this.query = query;
            this.statusQuery = statusQuery;
            this.dishQuery = dishQuery;
        }

        public async Task<OrderDetailsResponse?> GetByIdAsync(long orderId)
        {
            var order = await query.GetByIdAsync(orderId);
            if (order == null)
                throw new OrderNotFundException();

            // Evitar null en OrderItems
            var orderItems = order.OrderItems ?? Enumerable.Empty<OrderItem>();

            decimal totalAmountDecimal = 0;
            var items = new List<OrderItemResponse>();
            var itemId = 1;
            foreach (var item in orderItems)
            {
                var dish = await dishQuery.GetByIdAsync(item.Dish);
                if (dish == null)
                    throw new ActiveDishException();

                var status = await statusQuery.GetByIdAsync(item.Status);
                if (status == null)
                    throw new Exception($"Status {item.Status} no encontrado");

                totalAmountDecimal += dish.Price * item.Quantity;

                items.Add(new OrderItemResponse(
                    itemId++,
                    item.Quantity,
                    item.Notes,
                    new GenericResponce(status.Id, status.Name),
                    new DishShortResponce(dish.DishId, dish.Name, dish.ImageUrl)
                ));
            }

            var orderStatus = await statusQuery.GetByIdAsync(order.OverallStatus);

            double totalAmount = (double)totalAmountDecimal;

            return new OrderDetailsResponse(
                order.OrderId,
                totalAmount,
                order.DeliveryTo,
                order.Notes,
                new GenericResponce(orderStatus?.Id ?? 0, orderStatus?.Name ?? "Sin estado"),
                new GenericResponce(order.DeliveryTypeNav.Id, order.DeliveryTypeNav.Name),
                items,
                order.CreateDate,
                order.UpdateDate
            );
        }

    }
}
