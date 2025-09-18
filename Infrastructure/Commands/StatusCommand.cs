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
    public class StatusCommand : IStatusCommand
    {
        private readonly ApplicationDbContext context;

        public StatusCommand(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<int> InsertAsync(Status status)
        {
            await context.Statuses.AddAsync(status);
            await context.SaveChangesAsync();
            return status.Id;
        }
    }
}
