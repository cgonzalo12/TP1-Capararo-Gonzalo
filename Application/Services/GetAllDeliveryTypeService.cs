using Application.DTOs;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class GetAllDeliveryTypeService : IGetAllDeliveryTypeService
    {
        private readonly IDeliveryTypeQuery query;

        public GetAllDeliveryTypeService(IDeliveryTypeQuery query)
        {
            this.query = query;
        }
        public async Task<IEnumerable<DeliveryTypeResponce>> GetAllAsync()
        {
            var deliveryTypes = await query.GetAllAsync();

            return deliveryTypes.Select(dlt => new DeliveryTypeResponce(
                dlt.Id,
                dlt.Name
            ));
        }
    }
}
