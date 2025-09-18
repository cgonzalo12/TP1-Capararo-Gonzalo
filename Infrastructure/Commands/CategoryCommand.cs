using Application.DTOs;
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

   
    public class CategoryCommand : ICategoryCommand
    {
        private readonly ApplicationDbContext context;

        public CategoryCommand(ApplicationDbContext context)
        {
            this.context = context;
        }
        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> InsertAsync(Category category)
        {
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
            return category.Id;
        }

        public Task UpdateAsync(Category category)
        {
            throw new NotImplementedException();
        }
    }
}
