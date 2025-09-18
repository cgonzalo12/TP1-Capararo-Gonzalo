using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CreateDishService : ICreateDishService
    {
        private readonly IDishCommand command;
        private readonly ICategoryQuery categoryQuery;
        private readonly IDishQuery query;

        public CreateDishService(IDishCommand command,ICategoryQuery categoryQuery,IDishQuery query)
        {
            this.command = command;
            this.categoryQuery = categoryQuery;
            this.query = query;
        }
        public async Task<DishResponse> CreateAsync(CreateDishRequest request)
        {
            var existing = await query.GetByNameAsync(request.Name);
            if (existing.Any())
            {
                throw new InvalidOperationException($"Ya existe un plato con el nombre '{request.Name}'.");
            }

            var now = DateTime.UtcNow;

            var dish= new Dish
            {
                DishId = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Available = true,
                ImageUrl = request.Image,
                CategoryId = request.CategoryId,
                CreateDate = now,
                UpdateDate = now
            };

            await command.InsertAsync(dish);

            var categoryName= await categoryQuery.GetCategoryNameByIdAsync(request.CategoryId) ?? string.Empty;

            return new DishResponse(
                dish.DishId,
                dish.Name,
                dish.Description,
                dish.Price,
                dish.Available,
                dish.ImageUrl,
                dish.CategoryId,
                categoryName,
                dish.CreateDate,
                dish.UpdateDate
            );
        }
    }
}
