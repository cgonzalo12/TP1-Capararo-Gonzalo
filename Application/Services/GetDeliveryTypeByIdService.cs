using Application.DTOs;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class GetDeliveryTypeByIdService : IGetDeliveryTypeByIdService
    {
        private readonly IDeliveryTypeQuery query;

        public GetDeliveryTypeByIdService(IDeliveryTypeQuery query)
        {
            this.query = query;
        }
        public async Task<DeliveryTypeResponce?> GetNameByIdAsync(int id)
        {
            var deliverytype = await query.GetNameByIdAsync(id);
            if (deliverytype == null) return null;
            return new DeliveryTypeResponce(
                deliverytype.Id,
                deliverytype.Name
            );
        }
    }
}
