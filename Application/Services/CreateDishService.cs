using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
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
            if (request.Price <= 0)
            {
                throw new InvalidPriceException(request.Price);
            }

            var category = await categoryQuery.GetCategoryByIdAsync(request.Category);
            if (category is null)
            {
                throw new ExistingCategoryException(request.Category);
            }

            var existing = (await query.GetAllAsync())
            .Where(d => d.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase));

            if (existing.Any())
            {
                throw new DishNameAlreadyExistingException(request.Name);
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
                Category = request.Category,
                CreateDate = now,
                UpdateDate = now
            };

            await command.InsertAsync(dish);

            var categoryResponce = new GenericResponce(
                category.Id,
                category.Name
            );

            return new DishResponse(
                dish.DishId,
                dish.Name,
                dish.Description,
                dish.Price,
                categoryResponce,
                dish.ImageUrl,
                dish.Available,
                dish.CreateDate,
                dish.UpdateDate
            );
        }
    }
}
