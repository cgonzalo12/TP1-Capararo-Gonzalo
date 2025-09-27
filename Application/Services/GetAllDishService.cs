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
    public class GetAllDishService : IGetAllDishesService
    {
        private readonly IDishQuery query;

        public GetAllDishService(IDishQuery query)
        {
            this.query = query;
        }
        public async Task<IEnumerable<DishResponse>> GetAllAsync(string? name, int? category, string? sortByPrice, bool? onlyActive)
        {
            var dishes = await query.GetAllAsync();

            if (!string.IsNullOrWhiteSpace(name))
            {
                dishes = dishes.Where(d => d.Name.ToLower().Contains(name.ToLower()));
            }

            if (category.HasValue)
            {
                dishes = dishes.Where(d => d.Category == category.Value);
            }

            if (onlyActive.HasValue)
            {
                dishes = dishes.Where(d => d.Available == onlyActive.Value);
            }

            if (!string.IsNullOrWhiteSpace(sortByPrice)) 
            { 
                dishes = sortByPrice.ToLower() switch 
                { "asc" => dishes.OrderBy(d => d.Price), 
                  "desc" => dishes.OrderByDescending(d => d.Price),
                    _ => throw new SortingParametersException(sortByPrice)
                }; 
            }

            return dishes.Select(dish => new DishResponse(
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
            ));
        }
    }
}
