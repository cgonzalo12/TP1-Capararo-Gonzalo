using Application.DTOs;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class GetDishByIdService : IGetDishByIdService
    {
        private readonly IDishQuery query;

        public GetDishByIdService(IDishQuery query)
        {
            this.query = query;
        }
        public async Task<DishResponse?> GetByIdAsync(Guid id)
        {
            var dish = await query.GetByIdAsync(id);
            if (dish == null) return null;
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
