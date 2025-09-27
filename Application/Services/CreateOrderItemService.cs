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
                Order = request.OrderId,
                Dish = request.Dish,
                Quantity = request.Quantity,
                Notes = request.Notes,
                Status = request.Status,

            };
            var orderItemId = await command.InsertAsync(orderItem);
            return new OrderItemResponse(
                orderItemId,
                orderItem.Order,
                orderItem.Dish,
                orderItem.DishNav.Name,
                orderItem.DishNav.Price,
                orderItem.Quantity,
                orderItem.Notes,
                orderItem.Status,
                orderItem.StatusNav.Name,
                orderItem.CreateDate
            );
        }
    }
}
