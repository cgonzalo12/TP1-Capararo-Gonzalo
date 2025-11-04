using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IOrderCommand
    {
        Task<long> InsertAsync(Order order);

        Task<long> PatchAsync(Order order);

        Task<long> PatchItemsAsync(Order order);

    }
}
