﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICategoryQuery
    {
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category?> GetCategoryByIdAsync(int categoryId);
    }
}
