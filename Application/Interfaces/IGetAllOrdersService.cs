using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IGetAllOrdersService
    {
        Task<IEnumerable<OrderDetailsResponse>> GetAllAsync(long? statusId = null,
            DateTime? fechaInicio = null, DateTime? fechaFin = null);
    }
}
