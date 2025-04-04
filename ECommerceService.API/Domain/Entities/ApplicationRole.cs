﻿using Microsoft.AspNetCore.Identity;

namespace ECommerceService.API.Domain.Entities
{
    public class ApplicationRole : IdentityRole
    {
        public string? Description { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset LastModifiedDate { get; set; }
    }
}
