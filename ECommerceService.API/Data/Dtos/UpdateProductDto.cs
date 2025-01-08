﻿using System.ComponentModel.DataAnnotations;

namespace ECommerceService.API.Data.Dtos
{
    public class UpdateProductDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public int StockQuantity { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
