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
    public class CreateOrderService :ICreateOrderService
    {
        private readonly IOrderCommand command;
        private readonly IDishQuery dishQuery;
        private readonly IStatusQuery statusQuery;
        private readonly IDeliveryTypeQuery deliveryTypeQuery;

        public CreateOrderService(IOrderCommand command ,IDishQuery dishQuery,
            IStatusQuery statusQuery,IDeliveryTypeQuery deliveryTypeQuery)
        {
            this.command = command;
            this.dishQuery = dishQuery;
            this.statusQuery = statusQuery;
            this.deliveryTypeQuery = deliveryTypeQuery;
        }

        public async Task<CreateOrderResponce> CreateAsync(CreateOrderRequest request)
        {

            //deliverytype
            var deliveryType = await deliveryTypeQuery.GetByIdAsync(request.Delivery.Id);
            if (deliveryType == null)
                throw new DeliverytypeException();

            //order
            var now = DateTime.UtcNow;
            var order = new Order
            {
                Notes = request.Notes,
                Price = 0, //se calcula despues
                CreateDate = now,
                UpdateDate = now,
                DeliveryType = request.Delivery.Id,
                DeliveryTo = request.Delivery.To!,
                OverallStatus = 1,
                OrderItems = request.Items?.Select(i => new OrderItem
                {
                    Quantity = i.Quantity,
                    Notes = i.Notes,
                    CreateDate = now,
                    Dish = i.Dish,
                    Status = 1
                }).ToList() ?? new List<OrderItem>()
            };
            

            //order items
            decimal totalPrice = 0;
            var orderItemResponses = new List<OrderItemResponse>();

            if (order.OrderItems.Any())
            {
                foreach (var item in order.OrderItems)
                {
                    if (item.Quantity<=0)
                    {
                        throw new QuantityException();
                    }
                    //plato
                    var dish = await dishQuery.GetByIdAsync(item.Dish);
                    if (dish == null || dish.Available ==false)
                        throw new ActiveDishException();

                    //status
                    var statusItem = await statusQuery.GetByIdAsync(item.Status);


                    //precio
                    totalPrice += dish.Price * item.Quantity;

                }
            }

            // Asignar el precio al order
            order.Price = totalPrice;

            //status
            var status = await statusQuery.GetByIdAsync(order.OverallStatus);

            var OrderId= await command.InsertAsync(order);
            var orderResponce= new CreateOrderResponce(
                OrderId,
                order.Price,
                order.CreateDate
            );

            return orderResponce;
        }
    }
}
