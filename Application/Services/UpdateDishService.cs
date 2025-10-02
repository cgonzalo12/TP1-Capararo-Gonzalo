using Application.DTOs;
using Application.Interfaces;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UpdateDishService : IUpdateDishService
    {
        private readonly IDishQuery query;
        private readonly IDishCommand command;

        public UpdateDishService(IDishQuery query,IDishCommand command)
        {
            this.query = query;
            this.command = command;
        }
        public async Task<DishResponse?> UpdateAsync(Guid id, DishUpdateRequest request)
        {
            var dish = await query.GetByIdAsync(id);
            // si no existe el dish
            if (dish == null)
            {
                throw new DishNotFoundException(id);
            }

            // si el nombre ya existe en otro plato
            var existing = (await query.GetAllAsync())
            .Where(d => d.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase));
            if (existing.Any())
            {
                throw new DishNameAlreadyExistingException(request.Name!);
            }
            if (request.Price<=0)
            {
                throw new InvalidPriceException(request.Price);
            }

            //cambios
            dish.Name = request.Name ?? dish.Name;
            dish.Description = request.Description ?? dish.Description;
            dish.Price = request.Price ?? dish.Price;
            dish.Available = request.IsActive ?? dish.Available;
            dish.ImageUrl = request.Image ?? dish.ImageUrl;
            dish.Category = request.Category ?? dish.Category;
            dish.UpdateDate = DateTime.UtcNow;

            await command.UpdateAsync(dish);

            return new DishResponse(
                dish.DishId,
                dish.Name,
                dish.Description,
                dish.Price,
                new GenericResponce(
                    dish.CategoryNav.Id,
                    dish.CategoryNav.Name
                ),
                dish.ImageUrl,
                dish.Available,
                dish.CreateDate,
                dish.UpdateDate
            );

        }
    }
}
