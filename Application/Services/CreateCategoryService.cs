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
    public class CreateCategoryService : ICreateCategoryService
    {
        private readonly ICategoryCommand command;

        public CreateCategoryService(ICategoryCommand command)
        {
            this.command = command;
        }

        public async Task<CategoryResponse> CreateAsync(CreateCategoryRequest request)
        {
            var category = new Category
            {
                Name = request.Name,
                Description = request.Description,
                Order = request.Order

            };
            var categoryId= await command.InsertAsync(category);
            return new CategoryResponse
            (
                categoryId,
                category.Name ?? string.Empty,
                category.Description,
                category.Order
            );
        }
    }
}
