using Application.DTOs;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class GetDishesByNameService : IGetDishesByNameServices
    {
        private readonly IDishQuery query;

        public GetDishesByNameService(IDishQuery query)
        {
            this.query = query;
        }
        public async Task<IEnumerable<DishResponse>> GetByNameAsync(string name)
        {
            var dishes = await query.GetByNameAsync(name);
            return  dishes.Select(dish => new DishResponse(
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
             ));
        }
    }
}
