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
    public class CategoryQuery : ICategoryQuery
    {
        private readonly ApplicationDbContext context;

        public CategoryQuery(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            var categories = await context.Category
                .AsNoTracking()
                .Include(c => c.Dishes)
                .ToListAsync();
            return categories;
        }

        public async Task<Category?> GetCategoryByIdAsync(int categoryId)
        {
            var category = await context.Category
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == categoryId);

            return category;
        }
    }
}
