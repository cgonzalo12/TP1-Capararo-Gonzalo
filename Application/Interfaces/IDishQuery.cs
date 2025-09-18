using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IDishQuery
    {
        Task<IEnumerable<Dish>> GetAllAsync();
        Task<Dish?> GetByIdAsync(Guid id);
        Task<IEnumerable<Dish>> GetByNameAsync(string name);
        Task<IEnumerable<Dish>> GetByCategoryAsync(int categoryId);
        Task<IEnumerable<Dish>> GetOrderedByPriceAsync(bool asc);
        Task<IEnumerable<Dish>> GetByAvailabilityAsync(bool available);
        Task<bool> ExistsByNameAsync(string name, Guid Id);

    }
}
