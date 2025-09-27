using Application.DTOs;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class GetAllCategoryService : IGetAllCategoryService
    {
        private readonly ICategoryQuery query;

        public GetAllCategoryService(ICategoryQuery query)
        {
            this.query = query;
        }

        public async Task<IEnumerable<CategoryResponse>> GetAllAsync()
        {
            var categories =await query.GetAllAsync();
            return categories.Select(cat => new CategoryResponse(
                cat.Id,
                cat.Name,
                cat.Description,
                cat.Order
            ));
        }
    }
}
