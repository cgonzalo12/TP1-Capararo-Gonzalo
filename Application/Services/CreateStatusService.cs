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
    public class CreateStatusService : ICreateStatusService
    {
        private readonly IStatusCommand command;

        public CreateStatusService(IStatusCommand command)
        {
            this.command = command;
        }
        public async Task<StatusResponce> CreateAsync(CreateStatusRequest request)
        {
            var status= new Status
            {
                Name = request.Name
            };
            var statusId = await command.InsertAsync(status);
            return new StatusResponce
            (
                statusId,
                status.Name ?? string.Empty
            );
        }
    }
}
