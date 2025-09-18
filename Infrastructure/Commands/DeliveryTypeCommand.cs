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
    public class DeliveryTypeCommand : IDeliveryTypeCommand
    {
        private readonly ApplicationDbContext context;

        public DeliveryTypeCommand(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<int> InsertAsync(DeliveryType deliveryType)
        {
            await context.DeliveryTypes.AddAsync(deliveryType);
            await context.SaveChangesAsync();
            return deliveryType.Id;
        }
    }
}
