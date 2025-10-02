using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IOrderQuery
    {
        Task<IEnumerable<Order>> GetAllAsync(); 
        Task<Order?> GetByIdAsync(long id);
    }
}
