﻿using BlazorEcommerce.Shared.Models;

namespace BlazorEcommerce.Server.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<ServiceResponse<List<Category>>> GetCategoriesAsync();

    }
}
