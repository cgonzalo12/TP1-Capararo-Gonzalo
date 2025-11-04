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
    public class UpdateOrderService : IUpdateOrderService
    {
        private readonly IOrderQuery query;
        private readonly IOrderCommand command;
        private readonly IDishQuery dishQuery;

        public UpdateOrderService(IOrderQuery query,IOrderCommand command,IDishQuery dishQuery)
        {
            this.query = query;
            this.command = command;
            this.dishQuery = dishQuery;
        }


        public async Task<OrderUpdateResponse> UpdateAsync(long orderId, OrderUpdateRequest orderUpdateRequest)
        {
            var order = await query.GetByIdAsync(orderId);

            if (order == null)
                throw new OrderNotFundException();

            if (order.StatusNav!.Id != 1)
                throw new ClosedOrderException();

            // Copia de los items actuales
            var orderItems = new List<OrderItem>(order.OrderItems);
            var itemsToDelete = new List<OrderItem>();
            var updatedOrderItems = new List<OrderItem>();



            if (orderUpdateRequest.Items!.Any())
            {
                foreach (var item in orderUpdateRequest.Items!)
                {
                    var dish = await dishQuery.GetByIdAsync(item.Dish);
                    if (dish == null)
                        throw new DishNotFoundException(item.Dish);
                    if (dish.Available==false)
                    {
                        throw new DishNotAvailableException();
                    }
                    var existingItem = orderItems.FirstOrDefault(oi => oi.Dish == item.Dish);

                    if (existingItem != null)
                    {
                        existingItem.Quantity = item.Quantity;
                        if (!string.IsNullOrWhiteSpace(item.Notes))
                            existingItem.Notes = item.Notes;
                        updatedOrderItems.Add(existingItem);
                    }
                    else
                    {
                        var newItem = new OrderItem
                        {
                            Dish = dish.DishId,
                            DishNav = dish,
                            Quantity = item.Quantity,
                            Notes = item.Notes,
                            CreateDate = DateTime.UtcNow,
                            Status = 1, // pendiente
                            Order = order.OrderId,
                            OrderNav = order
                        };

                        updatedOrderItems.Add(newItem);
                    }
                    itemsToDelete = orderItems
                        .Where(originalItem => !updatedOrderItems.Any(updatedItem => updatedItem.Dish == originalItem.Dish))
                        .ToList();

                }
            }
            else
            {
                itemsToDelete = orderItems;
                updatedOrderItems.Clear();
            }

            order.OrderItems = updatedOrderItems;

            // Recalculo el precio total
            order.Price = orderItems.Sum(oi => oi.DishNav.Price * oi.Quantity);

            // Actualizo la fecha de modificación
            order.UpdateDate = DateTime.UtcNow;

            // Persisto cambios
            await command.PatchItemsAsync(order);

            return new OrderUpdateResponse
            {
                orderNumber = order.OrderId,
                totalAmount = (double)order.Price,
                updateAt = order.UpdateDate
            };
        }

        //public async Task<OrderUpdateResponse> UpdateAsync( long orderId,OrderUpdateRequest orderUpdateRequest)
        //{
        //    var order = await query.GetByIdAsync(orderId);
        //    if (order == null)
        //    {
        //        throw new OrderNotFundException();
        //    }
        //    if (order.StatusNav!.Id !=1)
        //    {
        //        throw new ClosedOrderException();
        //    }
        //    var orderItems = new List<OrderItem>(order.OrderItems);





        //}


    }
}
