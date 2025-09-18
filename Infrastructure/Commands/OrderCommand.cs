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
    public class OrderCommand :IOrderCommand
    {
        private readonly ApplicationDbContext context;

        public OrderCommand(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<long> InsertAsync(Order order)
        {
            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();
            return order.OrderId;
        }

       
    }
}
