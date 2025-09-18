using Application.DTOs;
using Application.Interfaces;
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
        public async Task<DishResponse?> UpdateAsync(Guid id, UpdateDishRequest request)
        {
            var dish = await query.GetByIdAsync(id);
            // si no existe el dish
            if (dish == null) return null;
            // si el nombre ya existe en otro plato
            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                bool exists = await query.ExistsByNameAsync(request.Name, id);
                if (exists)
                {
                    throw new Exception("El nombre del plato ya esta en uso!");

                }
            }

            //cambios
            dish.Name = request.Name ?? dish.Name;
            dish.Description = request.Description ?? dish.Description;
            dish.Price = request.Price ?? dish.Price;
            dish.Available = request.Available ?? dish.Available;
            dish.ImageUrl = request.Image ?? dish.ImageUrl;
            dish.CategoryId = request.CategoryId ?? dish.CategoryId;
            dish.UpdateDate = DateTime.UtcNow;

            await command.UpdateAsync(dish);

            return new DishResponse(
                dish.DishId,
                dish.Name,
                dish.Description,
                dish.Price,
                dish.Available,
                dish.ImageUrl,
                dish.CategoryId,
                dish.Category?.Name ?? string.Empty,
                dish.CreateDate,
                dish.UpdateDate
            );

        }
    }
}
