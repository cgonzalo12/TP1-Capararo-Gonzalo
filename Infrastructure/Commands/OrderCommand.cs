using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
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
            await context.Order.AddAsync(order);
            await context.SaveChangesAsync();
            return order.OrderId;
        }

        public async Task<long> PatchAsync(Order order)
        {
            // Adjuntamos la entidad si no está siendo rastreada
            context.Attach(order);

            // Marcamos solo los campos que queremos que se actualicen
            context.Entry(order).Property(o => o.OverallStatus).IsModified = true;
            context.Entry(order).Property(o => o.UpdateDate).IsModified = true;

            // También marcamos los items modificados
            foreach (var item in order.OrderItems)
            {
                context.Attach(item); // adjunta si no está rastreado
                context.Entry(item).Property(i => i.Status).IsModified = true;
            }

            await context.SaveChangesAsync();

            //context.Order.Update(order);
            //await context.SaveChangesAsync();
            return order.OrderId;

        }

        public async Task<long> PatchItemsAsync(Order order)
        {
            context.Order.Update(order);
            await context.SaveChangesAsync();

            return order.OrderId;
        }

    }
}
