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
                DeliveryTypeId = request.DeliveyTypeId,
                DeliveryTo = request.DeliveryTo!,
                OverallStatusId = 1
            };
            var OrderId= await command.InsertAsync(order);
            return new OrderResponse(
                OrderId,
                order.DeliveryType.Name,
                order.DeliveryTo,
                order.Status.Name,
                order.Notes,
                order.Price,
                order.CreateDate,
                order.UpdateDate
            );
        }
    }
}
