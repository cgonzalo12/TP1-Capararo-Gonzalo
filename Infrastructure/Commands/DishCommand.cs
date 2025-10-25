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
            await context.Dish.AddAsync(dish);
            await context.SaveChangesAsync();
            return dish.DishId;
        }

        public async Task<Guid> UpdateAsync(Dish dish)
        {
            context.Dish.Update(dish);
            await context.SaveChangesAsync();
            return dish.DishId;
        }
        public async Task<Guid> DeleteAsync(Dish dish)
        {
            context.Dish.Remove(dish);
            await context.SaveChangesAsync();
            return dish.DishId;
        }
    }
}
