using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CreateOrderItemService : ICreateOrderItemService
    {
        private readonly IOrderItemCommand command;

        public CreateOrderItemService(IOrderItemCommand command)
        {
            this.command = command;
        }
        public async Task<OrderItemResponse> CreateAsync(CreateOrderItemRequest request)
        {
            var orderItem = new OrderItem
            {
                OrderId = request.OrderId,
                DishId = request.DishId,
                Quantity = request.Quantity,
                Notes = request.Notes,
                StatusId = request.StatusId,

            };
            var orderItemId = await command.InsertAsync(orderItem);
            return new OrderItemResponse(
                orderItemId,
                orderItem.OrderId,
                orderItem.DishId,
                orderItem.Dish.Name,
                orderItem.Dish.Price,
                orderItem.Quantity,
                orderItem.Notes,
                orderItem.StatusId,
                orderItem.Status.Name,
                orderItem.CreateDate
            );
        }
    }
}
