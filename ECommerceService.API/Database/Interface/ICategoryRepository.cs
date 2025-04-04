﻿using ECommerceService.API.Domain.Entities;
using ECommerceService.API.Helpers;

namespace ECommerceService.API.Database.Interface
{
    public interface ICategoryRepository : IBaseRepository<Category, int>
    {
    }
}
