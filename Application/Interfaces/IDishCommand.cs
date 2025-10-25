using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IDishCommand
    {
        Task<Guid> InsertAsync(Dish dish);
        Task<Guid> UpdateAsync(Dish dish);
        Task<Guid> DeleteAsync(Dish dish);

    }
}
