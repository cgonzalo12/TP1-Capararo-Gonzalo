using Application.DTOs;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class GetAllStatusService : IGetAllStatusService
    {
        private readonly IStatusQuery query;

        public GetAllStatusService(IStatusQuery query)
        {
            this.query = query;
        }
        public async Task<IEnumerable<StatusResponce>> GetAllAsync()
        {
            var statuses = await query.GetAllAsync();
            return statuses.Select(st => new StatusResponce(
                st.Id,
                st.Name
            ));
        }
    }
}
