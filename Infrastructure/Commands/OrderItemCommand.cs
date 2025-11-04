using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Commands
{
    public class OrderItemCommand : IOrderItemCommand
    {
        private readonly ApplicationDbContext context;

        public OrderItemCommand(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<long> DeleteAsync(OrderItem orderItem)
        {
            context.OrderItem.Remove(orderItem);
            await context.SaveChangesAsync();
            return orderItem.OrderItemId;
        }

        public async Task<long> InsertAsync(OrderItem orderItem)
        {
            context.OrderItem.Add(orderItem);
            await context.SaveChangesAsync();
            return (orderItem.OrderItemId);
        }
    }
}
