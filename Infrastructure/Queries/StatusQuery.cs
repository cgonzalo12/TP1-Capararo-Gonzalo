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
    public class StatusQuery : IStatusQuery
    {
        private readonly ApplicationDbContext context;

        public StatusQuery(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Status>> GetAllAsync()
        {
            var statuses =await context.Status
                .AsNoTracking()
                .ToListAsync();
            return statuses;
        }

        public async Task<Status?> GetByIdAsync(int id)
        {
            var status = await context.Status
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);
            return status;
        }
    }
}
