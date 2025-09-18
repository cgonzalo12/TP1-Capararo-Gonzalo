using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CreateDeliveryTypeService : ICreateDeliveryTypeService
    {
        private readonly IDeliveryTypeCommand command;

        public CreateDeliveryTypeService(IDeliveryTypeCommand command)
        {
            this.command = command;
        }

        public async Task<DeliveryTypeResponce> CreateAsync(CreateDeliveryTypeRequest request)
        {
            var deliveryType = new DeliveryType
            {
                Name = request.Name
            };
            var deliveryTypeId = await command.InsertAsync(deliveryType);
            return new DeliveryTypeResponce
            (
                deliveryTypeId,
                deliveryType.Name ?? string.Empty
            );
        }
    }
}
