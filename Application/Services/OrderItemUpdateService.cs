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
    public class OrderItemUpdateService : IOrderItemUpdateService
    {
        private readonly IOrderQuery query;
        private readonly IOrderCommand command;
        private readonly IStatusQuery statusQuery;

        public OrderItemUpdateService(IOrderQuery query,IOrderCommand command,IStatusQuery statusQuery)
        {
            this.query = query;
            this.command = command;
            this.statusQuery = statusQuery;
        }

        public async Task<OrderUpdateResponse> UpdateOrderItemsAsync(long orderId, Guid idDish, OrderItemUpdateRequest itemUpdateRequest)
        {
            if (itemUpdateRequest.status != 2 && itemUpdateRequest.status != 3 && itemUpdateRequest.status != 4)
                throw new InvalidStatusException();

            var order = await query.GetByIdAsync(orderId);
            if (order == null)
                throw new OrderNotFundException();

            if (order.StatusNav!.Id != 1)
                throw new ClosedOrderException();
            var orderItems = new List<OrderItem>(order.OrderItems);

            var item = order.OrderItems.FirstOrDefault(x => x.Dish == idDish);
            if (item == null) 
                throw new ItemNotFundInTheOrderException();

            bool IsValidTransition(int current, int next) =>
            (current, next) switch
            {
                (1, 2) => true, // Pendiente -> En preparación
                (2, 3) => true, // En preparación -> Listo
                (3, 4) => true, // Listo -> Entregado
                _ => false
            };
            if (!IsValidTransition(item.Status, itemUpdateRequest.status))
            {
                var itemStatus = await statusQuery.GetByIdAsync(item.Status);
                var newStatus = await statusQuery.GetByIdAsync(itemUpdateRequest.status);
                throw new InvalidTransactionException(itemStatus!.Name!, newStatus!.Name!);
            }
                


            //status dish
            item.Status = itemUpdateRequest.status;
            order.UpdateDate = DateTime.UtcNow;

            // status order
            int CalculateOverallStatus(IEnumerable<OrderItem> items)
            {
                if (items.All(i => i.Status == 1))
                    return 1; // Pending
                if (items.Any(i => i.Status == 2))
                    return 2; // In Progress
                if (items.All(i => i.Status == 3))
                    return 3; // Ready
                if (items.All(i => i.Status == 4))
                    return 5; // Closed
                if (items.Any(i => i.Status == 4))
                    return 4; // Delivery parcial

                // Si hay mezcla de pendientes y listos
                return 2; // En preparación
            }
            order.OverallStatus = CalculateOverallStatus(order.OrderItems);
            order.StatusNav = null; // para que lo recargue
            await command.UpdateAsync(order);
            
            return new OrderUpdateResponse
            {
                orderNumber = order.OrderId,
                totalAmount = (double)order.Price,
                updateAt = order.UpdateDate
            };

        }
    }
}
