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
    public class DishCommand : IDishCommand
    {
        private readonly ApplicationDbContext context;

        public DishCommand(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<Guid> InsertAsync(Dish dish)
        {
            await context.Dishes.AddAsync(dish);
            await context.SaveChangesAsync();
            return dish.DishId;
        }

        public async Task<Guid> UpdateAsync(Dish dish)
        {
            context.Dishes.Update(dish);
            await context.SaveChangesAsync();
            return dish.DishId;
        }
    }
}
