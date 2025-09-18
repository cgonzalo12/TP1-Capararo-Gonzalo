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
    public class DeliveryTypeQuery :IDeliveryTypeQuery
    {
        private readonly ApplicationDbContext context;

        public DeliveryTypeQuery(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<DeliveryType>> GetAllAsync()
        {
            var deliverytypes = await context.DeliveryTypes
                .AsNoTracking()
                .ToListAsync();
            return deliverytypes;
        }

        public async Task<DeliveryType?> GetNameByIdAsync(int id)
        {
            var deliverytype =await context.DeliveryTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return deliverytype;
        }
    }
}
