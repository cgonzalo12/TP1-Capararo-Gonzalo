using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        public async Task<IEnumerable<OrderDetailsResponse>> GetAllAsync(long? statusId = null,
            DateTime? fechaInicio =null,DateTime? fechaFin = null)
        {
            // Validación de rango
            if (fechaInicio.HasValue && fechaFin.HasValue && fechaInicio > fechaFin)
            {
                throw new DateRangeException();
            }


            var orders = await query.GetAllAsync();
            if (statusId.HasValue)
            {
                orders = orders.Where(o => o.OverallStatus == statusId.Value);
            }

            // Filtro por rango de fechas si corresponde
            if (fechaInicio.HasValue)
            {
                orders = orders.Where(o => o.CreateDate >= fechaInicio.Value);
            }
            if (fechaFin.HasValue)
            {
                orders = orders.Where(o => o.CreateDate <= fechaFin.Value);
            }

            var orderResponses = new List<OrderDetailsResponse>();

            foreach (var o in orders)
            {
                // Traer status del order
                var status = await statusQuery.GetByIdAsync(o.OverallStatus);
                
                // Traer deliveryType del order
                var deliveryType = await deliveryTypeQuery.GetByIdAsync(o.DeliveryType);

                // Procesar items
                var itemResponses = new List<OrderItemResponse>();
                var itemId = 1;
                foreach (var oi in o.OrderItems)
                {
                    // status item
                    var statusItem = await statusQuery.GetByIdAsync(oi.Status);

                    // dish
                    var dish = await dishQuery.GetByIdAsync(oi.Dish);

                    itemResponses.Add(new OrderItemResponse(
                        itemId++,
                        oi.Quantity,
                        oi.Notes,
                        new GenericResponce(statusItem!.Id, statusItem.Name),
                        new DishShortResponce(dish!.DishId, dish.Name, dish.ImageUrl!)
                    ));
                }

                orderResponses.Add(new OrderDetailsResponse(
                    o.OrderId,
                    (double)o.Price,
                    o.DeliveryTo,
                    o.Notes,
                    new GenericResponce(status!.Id, status.Name),
                    new GenericResponce(deliveryType!.Id, deliveryType.Name),
                    itemResponses,
                    o.CreateDate,
                    o.UpdateDate
                ));
            }

            return orderResponses;
        }
    }
}
