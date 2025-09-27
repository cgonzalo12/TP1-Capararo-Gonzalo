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
    public class CreateOrderService :ICreateOrderService
    {
        private readonly IOrderCommand command;

        public CreateOrderService(IOrderCommand command )
        {
            this.command = command;
        }

        public async Task<OrderResponse> CreateAsync(CreateOrderRequest request)
        {
            
            var now = DateTime.UtcNow;
            var order = new Order
            {
                Notes = request.Notes,
                Price = request.Price,
                CreateDate = now,
                UpdateDate = now,
                DeliveryType = request.DeliveyType,
                DeliveryTo = request.DeliveryTo!,
                OverallStatus = 1
            };
            var OrderId= await command.InsertAsync(order);
            return new OrderResponse(
                OrderId,
                order.DeliveryTypeNav.Name,
                order.DeliveryTo,
                order.StatusNav.Name,
                order.Notes,
                order.Price,
                order.CreateDate,
                order.UpdateDate
            );
        }
    }
}
