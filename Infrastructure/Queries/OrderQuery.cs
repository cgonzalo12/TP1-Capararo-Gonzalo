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
    public class OrderQuery : IOrderQuery
    {
        private readonly ApplicationDbContext context;

        public OrderQuery(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            var orders =await context.Order
                .AsNoTracking()
                .Include(o => o.DeliveryTypeNav)
                .Include(o => o.StatusNav)
                .Include(o => o.OrderItems)!
                    .ThenInclude(oi => oi.DishNav)
                .ToListAsync();
            return orders;
        }

        public Task<Order?> GetByIdAsync(long id)
        {
            var order = context.Order
                .AsNoTracking()
                .Include(o => o.DeliveryTypeNav)
                .Include(o => o.StatusNav)
                .Include(o => o.OrderItems)!
                    .ThenInclude(oi => oi.DishNav)
                .FirstOrDefaultAsync(o => o.OrderId == id);
            return order;
        }
    }
}
