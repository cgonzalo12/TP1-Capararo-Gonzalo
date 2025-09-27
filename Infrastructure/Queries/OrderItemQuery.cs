using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Queries
{
    public class OrderItemQuery : IOrderItemQuery
    {
        private readonly ApplicationDbContext context;

        public OrderItemQuery(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<OrderItem>> GetAllAsync()
        {
            var orderItems = await context.OrderItem
                .AsNoTracking()
                .Include(oi => oi.DishNav)
                .Include(oi => oi.StatusNav)
                .ToListAsync();
            return orderItems;
        }
    }
}
