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
            // Validar status permitido
            if (!new[] { 2, 3, 4 }.Contains(itemUpdateRequest.status))
                throw new InvalidStatusException();

            // Obtener la orden
            var order = await query.GetByIdAsync(orderId);
            if (order == null)
                throw new OrderNotFundException();


            // Obtener el ítem específico
            var item = order.OrderItems.FirstOrDefault(x => x.Dish == idDish);
            if (item == null)
                throw new ItemNotFundInTheOrderException();

            // Validar transición de estado
            if (itemUpdateRequest.status != item.Status + 1 )
            {
                throw new InvalidTransactionException(item.Status.ToString(),itemUpdateRequest.status.ToString());
            }

            // Actualizar el estado del ítem
            item.Status = itemUpdateRequest.status;
            order.UpdateDate = DateTime.UtcNow;

            // Calcular estado general de la orden
            var previousStatus = order.OverallStatus;

            if (order.OrderItems.All(i => i.Status == 1))
                order.OverallStatus = 1;
            else if (order.OrderItems.All(i => i.Status == 2))
                order.OverallStatus = 2;
            else if (order.OrderItems.All(i => i.Status == 3))
                order.OverallStatus = 3;
            else if (order.OrderItems.All(i => i.Status == 4))
                order.OverallStatus = 4;
            else
                order.OverallStatus = previousStatus;

            // Para EF Core: resetear navegación
            order.StatusNav = null;

            // Guardar cambios
            await command.PatchAsync(order);
            

            return new OrderUpdateResponse
            {
                orderNumber = order.OrderId,
                totalAmount = (double)order.Price,
                updateAt = order.UpdateDate
            };
        }


        //public async Task<OrderUpdateResponse> UpdateOrderItemsAsync(long orderId, Guid idDish, OrderItemUpdateRequest itemUpdateRequest)
        //{
        //    if (itemUpdateRequest.status != 2 && itemUpdateRequest.status != 3 && itemUpdateRequest.status != 4)
        //        throw new InvalidStatusException();

        //    var order = await query.GetByIdAsync(orderId);
        //    if (order == null)
        //        throw new OrderNotFundException();

        //    if (order.StatusNav!.Id != 1)
        //        throw new ClosedOrderException();
        //    var orderItems = new List<OrderItem>(order.OrderItems);

        //    var item = order.OrderItems.FirstOrDefault(x => x.Dish == idDish);
        //    if (item == null) 
        //        throw new ItemNotFundInTheOrderException();

        //    bool IsValidTransition(int current, int next) =>
        //    (current, next) switch
        //    {
        //        (1, 2) => true, // Pendiente -> En preparación
        //        (2, 3) => true, // En preparación -> Listo
        //        (3, 4) => true, // Listo -> Entregado
        //        _ => false
        //    };
        //    if (!IsValidTransition(item.Status, itemUpdateRequest.status))
        //    {
        //        var itemStatus = await statusQuery.GetByIdAsync(item.Status);
        //        var newStatus = await statusQuery.GetByIdAsync(itemUpdateRequest.status);
        //        throw new InvalidTransactionException(itemStatus!.Name!, newStatus!.Name!);
        //    }



        //    //status dish
        //    item.Status = itemUpdateRequest.status;
        //    order.UpdateDate = DateTime.UtcNow;

        //    // status order
        //    int CalculateOverallStatus(IEnumerable<OrderItem> items)
        //    {
        //        // Todos pendientes → orden pendiente
        //        if (items.All(i => i.Status == 1))
        //            return 1; // Pending

        //        // Al menos uno en preparación → En preparación
        //        if (items.Any(i => i.Status == 2))
        //            return 2; // In Progress

        //        // Todos listos → Listo para entregar
        //        if (items.All(i => i.Status == 3))
        //            return 3; // Ready

        //        // Si algunos entregados y otros listos → Entrega parcial
        //        if (items.Any(i => i.Status == 4) && items.Any(i => i.Status != 4))
        //            return 4; // Partial Delivery

        //        // Todos entregados → Cerrada
        //        if (items.All(i => i.Status == 4))
        //            return 5; // Closed

        //        return 2; // default: En preparación
        //    }

        //    order.OverallStatus = CalculateOverallStatus(order.OrderItems);
        //    order.StatusNav = null; // para que lo recargue
        //    await command.UpdateAsync(order);

        //    return new OrderUpdateResponse
        //    {
        //        orderNumber = order.OrderId,
        //        totalAmount = (double)order.Price,
        //        updateAt = order.UpdateDate
        //    };

        //}
    }
}
