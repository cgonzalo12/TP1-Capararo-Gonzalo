using Application.DTOs;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class GetDishesByCategoryService : IGetDishesByCategoryService
    {
        private readonly IDishQuery query;

        public GetDishesByCategoryService(IDishQuery query)
        {
            this.query = query;
        }
        public async Task<IEnumerable<DishResponse>> GetByCategoryAsync(int categoryId)
        {
            var dishes = await query.GetByCategoryAsync(categoryId);
            return dishes.Select(dish => new DishResponse(
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
