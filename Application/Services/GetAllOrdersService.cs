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
    public class GetAllOrdersService : IGetAllOrdersService
    {
        private readonly IOrderQuery query;
        private readonly IDishQuery dishQuery;
        private readonly IStatusQuery statusQuery;
        private readonly IDeliveryTypeQuery deliveryTypeQuery;

        public GetAllOrdersService(IOrderQuery query,IDishQuery dishQuery,IStatusQuery statusQuery,IDeliveryTypeQuery deliveryTypeQuery)
        {
            this.query = query;
            this.dishQuery = dishQuery;
            this.statusQuery = statusQuery;
            this.deliveryTypeQuery = deliveryTypeQuery;
        }
        public async Task<IEnumerable<OrderResponse>> GetAllAsync()
        {
            var orders = await query.GetAllAsync();
            var orderResponses = new List<OrderResponse>();

            foreach (var o in orders)
            {
                // Traer status del order
                var status = await statusQuery.GetByIdAsync(o.OverallStatus);
                if (status == null)
                    throw new Exception($"Status con Id {o.OverallStatus} no existe.");

                // Traer deliveryType del order
                var deliveryType = await deliveryTypeQuery.GetByIdAsync(o.DeliveryType);
                if (deliveryType == null)
                    throw new Exception($"DeliveryType con Id {o.DeliveryType} no existe.");

                // Procesar items
                var itemResponses = new List<OrderItemResponse>();
                foreach (var oi in o.OrderItems)
                {
                    // status item
                    var statusItem = await statusQuery.GetByIdAsync(oi.Status);
                    if (statusItem == null)
                        throw new Exception($"Status con Id {oi.Status} no existe.");

                    // dish
                    var dish = await dishQuery.GetByIdAsync(oi.Dish);
                    if (dish == null)
                        throw new Exception($"Dish con Id {oi.Dish} no existe.");

                    itemResponses.Add(new OrderItemResponse(
                        oi.OrderItemId,
                        oi.Quantity,
                        oi.Notes,
                        new StatusResponce(statusItem.Id, statusItem.Name),
                        new DishByOrderItemResponce(dish.DishId, dish.Name, dish.ImageUrl!)
                    ));
                }

                orderResponses.Add(new OrderResponse(
                    o.OrderId,
                    o.Price,
                    o.DeliveryTo,
                    o.Notes,
                    new StatusResponce(status.Id, status.Name),
                    new DeliveryTypeResponce(deliveryType.Id, deliveryType.Name),
                    itemResponses,
                    o.CreateDate,
                    o.UpdateDate
                ));
            }

            return orderResponses;
        }



        //public async Task<IEnumerable<OrderResponse>> GetAllAsync()
        //{
        //    var orders = await query.GetAllAsync();

        //    return orders.Select(o => new OrderResponse(

        //        o.OrderId,
        //        o.Price,
        //        o.DeliveryTo,
        //        o.Notes,
        //        new StatusResponce(o.StatusNav!.Id, o.StatusNav.Name),
        //        new DeliveryTypeResponce(o.DeliveryTypeNav.Id, o.DeliveryTypeNav.Name),
        //        o.OrderItems.Select(oi => new OrderItemResponse(
        //            oi.OrderItemId,
        //            oi.Quantity,
        //            oi.Notes,
        //            new StatusResponce(oi.StatusNav!.Id, oi.StatusNav.Name),
        //            new DishByOrderItemResponce(oi.DishNav.DishId, oi.DishNav.Name, oi.DishNav.ImageUrl!)
        //            ))
        //        .ToList(),
        //        o.CreateDate,
        //        o.UpdateDate
        //    ));

        //}
    }
}
