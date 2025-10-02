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
        public async Task<OrderItemResponse> CreateAsync(Items request)
        {
            var orderItem = new OrderItem
            {
                Dish = request.Dish,
                Quantity = request.Quantity,
                Notes = request.Notes,
                Status = 1,

            };
            var orderItemId = await command.InsertAsync(orderItem);
            return new OrderItemResponse(
                orderItemId,
                orderItem.Quantity,
                orderItem.Notes,
                new GenericResponce(orderItem.StatusNav!.Id, orderItem.StatusNav.Name),
                new DishShortResponce(orderItem.DishNav.DishId, orderItem.DishNav.Name, orderItem.DishNav.ImageUrl!)
            );
        }
    }
}
