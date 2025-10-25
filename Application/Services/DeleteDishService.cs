using Application.DTOs;
using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application.Services
{
    public class DeleteDishService : IDeleteDishService
    {
        private readonly IDishCommand dishCommand;
        private readonly IDishQuery dishQuery;

        public DeleteDishService(IDishCommand dishCommand,IDishQuery dishQuery)
        {
            this.dishCommand = dishCommand;
            this.dishQuery = dishQuery;
        }
        public async Task<DishShortResponce?> DeleteAsync(Guid id)
        {
            var dish = await dishQuery.GetByIdAsync(id);
            if (dish == null)
                return null;

            await dishCommand.DeleteAsync(dish);

            return new DishShortResponce(
                id,
                dish.Name,
                dish.Description!

            );
            
                
            
        }
    }
}
