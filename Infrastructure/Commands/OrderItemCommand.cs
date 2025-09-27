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
        public async Task<long> InsertAsync(OrderItem orderItem)
        {
            await context.OrderItem.AddAsync(orderItem);
            return orderItem.OrderItemId;
        }
    }
}
