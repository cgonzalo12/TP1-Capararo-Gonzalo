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
            var dishes= await context.Dishes
                .AsNoTracking()
                .Include(d => d.Category)
                .ToListAsync();
            return dishes;
        }

        public async Task<IEnumerable<Dish>> GetByCategoryAsync(int categoryId)
        {
            return await context.Dishes
                .AsNoTracking()
                .Include(d => d.Category)
                .Where(d => d.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<Dish?> GetByIdAsync(Guid id)
        {
            return await context.Dishes
                .AsNoTracking()
                .Include(d => d.Category)
                .FirstOrDefaultAsync(d => d.DishId == id);
        }

        public async Task<IEnumerable<Dish>> GetByNameAsync(string name)
        {
            return await context.Dishes
                .AsNoTracking()
                .Include(d => d.Category)
                .Where(d => d.Name.Contains(name))
                .ToListAsync();
        }



        public async Task<IEnumerable<Dish>> GetOrderedByPriceAsync(bool asc)
        {
            return await context.Dishes
                .AsNoTracking()
                .Include(d => d.Category)
                .OrderBy(d => asc ? d.Price : -d.Price)
                .ToListAsync();
        }

        public async Task<IEnumerable<Dish>> GetByAvailabilityAsync(bool available)
        {
            return await context.Dishes
                .AsNoTracking()
                .Include(d => d.Category)
                .Where(d => d.Available == available)
                .ToListAsync();
        }

        public async Task<bool> ExistsByNameAsync(string name, Guid Id)
        {
            return await context.Dishes
                .AsNoTracking()
                .AnyAsync(d => d.Name == name && d.DishId != Id);
        }



        //-------
        public async Task<IEnumerable<Dish>> GetFilteredAsync(string? name, int? categoryId, bool orderByPriceAsc)
        {
            var query = context.Dishes
                .AsNoTracking()
                .Include(d => d.Category)
                .AsQueryable();
            if (!string.IsNullOrWhiteSpace(name))
            {
                query = query.Where(d => d.Name.Contains(name));
            }
            if (categoryId.HasValue)
            {
                query = query.Where(d => d.CategoryId == categoryId.Value);
            }
            if (orderByPriceAsc)
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
