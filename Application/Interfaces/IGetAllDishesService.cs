using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IGetAllDishesService
    {
        Task<IEnumerable<DishResponse>> GetAllAsync(string? name, int? category, string? sortByPrice, bool? onlyActive);
    }
}
