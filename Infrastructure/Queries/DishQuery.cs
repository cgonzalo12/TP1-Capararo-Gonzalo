using Application.Interfaces;
using Application.Services;
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
    public class DishQuery :IDishQuery
    {
        private readonly ApplicationDbContext context;

        public DishQuery(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<Dish>> GetAllAsync()
        {
            var dishes= await context.Dish
                .AsNoTracking()
                .Include(d => d.CategoryNav)
                .ToListAsync();
            return dishes;
        }

        public async Task<IEnumerable<Dish>> GetByCategoryAsync(int categoryId)
        {
            return await context.Dish
                .AsNoTracking()
                .Include(d => d.Category)
                .Where(d => d.Category == categoryId)
                .ToListAsync();
        }

        public async Task<Dish?> GetByIdAsync(Guid id)
        {
            return await context.Dish
                .AsNoTracking()
                .Include(d => d.CategoryNav)
                .FirstOrDefaultAsync(d => d.DishId == id);
        }




        public async Task<IEnumerable<Dish>> GetOrderedByPriceAsync(bool asc)
        {
            return await context.Dish
                .AsNoTracking()
                .Include(d => d.CategoryNav)
                .OrderBy(d => asc ? d.Price : -d.Price)
                .ToListAsync();
        }

        public async Task<IEnumerable<Dish>> GetByAvailabilityAsync(bool available)
        {
            return await context.Dish
                .AsNoTracking()
                .Include(d => d.CategoryNav)
                .Where(d => d.Available == available)
                .ToListAsync();
        }

        public async Task<bool> ExistsByNameAsync(string name, Guid Id)
        {
            return await context.Dish
                .AsNoTracking()
                .AnyAsync(d => d.Name == name && d.DishId != Id);
        }

        public async Task<IEnumerable<Dish>> GetFilteredAsync(string? name, int? categoryId, bool? orderByPriceAsc)
        {
            var query = context.Dish
                .AsNoTracking()
                .Include(d => d.CategoryNav)
                .AsQueryable();
            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(d => d.Name.Contains(name));
            }
            if (categoryId.HasValue)
            {
                query = query.Where(d => d.Category == categoryId.Value);
            }
            if (orderByPriceAsc.HasValue)
            {
                query = query.OrderBy(d => d.Price);
            }
            else
            {
                query = query.OrderByDescending(d => d.Price);
            }
            return await query.ToListAsync();
        }

        
    }
}
