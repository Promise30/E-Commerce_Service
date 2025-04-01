using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerceService.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateAndSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailVerificationCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailVerificationExpiry = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PhoneCountryCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false),
                    UserId1 = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    RoleId1 = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId1",
                        column: x => x.RoleId1,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId1",
                        column: x => x.UserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Carts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderStatus = table.Column<int>(type: "int", nullable: false),
                    PaymentStatus = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DiscountApplied = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    TaxAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PaymentReference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StockQuantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    CartId = table.Column<int>(type: "int", nullable: false),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartItems_Carts_CartId",
                        column: x => x.CartId,
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    SubTotal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    LastModifiedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedDate = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "Description", "LastModifiedDate", "Name" },
                values: new object[,]
                {
                    { 1, new DateTimeOffset(new DateTime(2024, 4, 21, 8, 44, 1, 404, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Electronic Items", new DateTimeOffset(new DateTime(2024, 10, 29, 23, 42, 32, 257, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Electronics" },
                    { 2, new DateTimeOffset(new DateTime(2024, 3, 31, 10, 52, 1, 851, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Clothing Items", new DateTimeOffset(new DateTime(2024, 8, 29, 20, 23, 48, 276, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Clothes" },
                    { 3, new DateTimeOffset(new DateTime(2024, 8, 4, 11, 35, 57, 321, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Books Items", new DateTimeOffset(new DateTime(2024, 4, 15, 11, 59, 59, 911, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Books" },
                    { 4, new DateTimeOffset(new DateTime(2025, 1, 27, 17, 23, 47, 710, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Devices and gadgets for everyday use", new DateTimeOffset(new DateTime(2025, 3, 1, 18, 16, 13, 237, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Devices" },
                    { 5, new DateTimeOffset(new DateTime(2025, 2, 15, 0, 58, 40, 542, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Home and office furnishing items", new DateTimeOffset(new DateTime(2024, 7, 20, 5, 59, 17, 406, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Furniture" },
                    { 6, new DateTimeOffset(new DateTime(2025, 1, 28, 23, 29, 24, 349, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Apparel for all occasions", new DateTimeOffset(new DateTime(2025, 3, 10, 8, 32, 8, 541, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Outfits" },
                    { 7, new DateTimeOffset(new DateTime(2024, 5, 29, 9, 24, 19, 607, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Literature and educational materials", new DateTimeOffset(new DateTime(2024, 9, 10, 22, 7, 37, 645, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Journals" },
                    { 8, new DateTimeOffset(new DateTime(2024, 9, 6, 14, 2, 40, 873, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Household appliances for convenience", new DateTimeOffset(new DateTime(2025, 2, 17, 8, 11, 22, 417, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Appliances" },
                    { 9, new DateTimeOffset(new DateTime(2024, 12, 12, 2, 7, 47, 504, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Add-ons for tech and lifestyle", new DateTimeOffset(new DateTime(2024, 11, 10, 16, 52, 2, 205, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Accessories" },
                    { 10, new DateTimeOffset(new DateTime(2024, 9, 22, 14, 36, 51, 720, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Gear and equipment for sports enthusiasts", new DateTimeOffset(new DateTime(2024, 4, 18, 12, 34, 25, 639, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Sports" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedDate", "Description", "ImageUrl", "LastModifiedDate", "Name", "Price", "StockQuantity" },
                values: new object[,]
                {
                    { 1, 1, new DateTimeOffset(new DateTime(2024, 6, 2, 8, 4, 10, 564, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "A rich and well-equipped high performance machine", null, new DateTimeOffset(new DateTime(2024, 8, 30, 15, 34, 45, 103, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Laptop", 1000m, 100 },
                    { 2, 1, new DateTimeOffset(new DateTime(2024, 7, 26, 20, 5, 29, 376, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "A pocket friendly and highly efficient telecommunication device", null, new DateTimeOffset(new DateTime(2024, 12, 27, 5, 0, 5, 616, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Mobile", 500m, 100 },
                    { 3, 2, new DateTimeOffset(new DateTime(2024, 10, 3, 19, 42, 44, 451, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "A warm and cold weather outfit for many genders", null, new DateTimeOffset(new DateTime(2025, 3, 22, 17, 22, 43, 471, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "T-Shirt", 20m, 100 },
                    { 4, 2, new DateTimeOffset(new DateTime(2024, 6, 22, 5, 30, 7, 507, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "A lower body covering suitable for various activities", null, new DateTimeOffset(new DateTime(2025, 2, 4, 6, 28, 4, 692, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Jeans", 50m, 100 },
                    { 5, 3, new DateTimeOffset(new DateTime(2024, 10, 9, 18, 39, 53, 719, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "The Philosopher's stone which looks at the life of Harry", null, new DateTimeOffset(new DateTime(2024, 5, 13, 13, 7, 30, 199, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Harry Potter: Volume 1", 10m, 100 },
                    { 6, 3, new DateTimeOffset(new DateTime(2024, 4, 24, 0, 10, 11, 232, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "A story starring a man with no fears", null, new DateTimeOffset(new DateTime(2024, 9, 24, 11, 55, 14, 257, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Mission Impossible", 15m, 100 },
                    { 7, 3, new DateTimeOffset(new DateTime(2025, 2, 4, 0, 52, 15, 23, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "A story of a determined and resilient warrior", null, new DateTimeOffset(new DateTime(2025, 3, 28, 5, 48, 32, 240, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "The Alchemist", 65m, 50 },
                    { 8, 3, new DateTimeOffset(new DateTime(2024, 8, 10, 22, 58, 59, 741, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "A sleek and powerful mobile device", null, new DateTimeOffset(new DateTime(2024, 6, 22, 20, 53, 14, 187, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Smartphone", 599.99m, 150 },
                    { 9, 5, new DateTimeOffset(new DateTime(2024, 12, 29, 5, 5, 57, 642, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Noise-canceling over-ear audio gear", null, new DateTimeOffset(new DateTime(2024, 5, 18, 14, 2, 49, 313, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Headphones", 129.50m, 75 },
                    { 10, 10, new DateTimeOffset(new DateTime(2024, 6, 6, 21, 8, 35, 358, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Ergonomic chair for long work hours", null, new DateTimeOffset(new DateTime(2025, 2, 8, 11, 21, 23, 231, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Desk Chair", 199.00m, 30 },
                    { 11, 1, new DateTimeOffset(new DateTime(2024, 11, 29, 9, 5, 0, 756, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "4K ultra-wide display for productivity", null, new DateTimeOffset(new DateTime(2024, 7, 15, 3, 3, 9, 746, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Monitor", 349.95m, 50 },
                    { 12, 7, new DateTimeOffset(new DateTime(2024, 7, 8, 4, 34, 13, 418, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Mechanical keyboard with RGB lighting", null, new DateTimeOffset(new DateTime(2025, 2, 24, 20, 54, 56, 671, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Keyboard", 89.99m, 200 },
                    { 13, 7, new DateTimeOffset(new DateTime(2025, 3, 28, 23, 17, 34, 584, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Precision gaming mouse with adjustable DPI", null, new DateTimeOffset(new DateTime(2024, 8, 10, 7, 19, 56, 940, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Mouse", 49.99m, 180 },
                    { 14, 3, new DateTimeOffset(new DateTime(2025, 2, 13, 12, 18, 39, 333, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Lightweight tablet for media and work", null, new DateTimeOffset(new DateTime(2024, 8, 6, 2, 19, 37, 6, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Tablet", 299.00m, 90 },
                    { 15, 9, new DateTimeOffset(new DateTime(2024, 11, 18, 1, 55, 17, 207, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "All-in-one printer with wireless connectivity", null, new DateTimeOffset(new DateTime(2024, 7, 27, 16, 4, 43, 543, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Printer", 159.75m, 25 },
                    { 16, 8, new DateTimeOffset(new DateTime(2024, 6, 15, 8, 12, 47, 564, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Durable backpack for travel and tech", null, new DateTimeOffset(new DateTime(2024, 4, 25, 7, 19, 8, 488, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Backpack", 79.50m, 60 },
                    { 17, 4, new DateTimeOffset(new DateTime(2024, 10, 21, 16, 35, 16, 974, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Fitness tracker with heart rate monitor", null, new DateTimeOffset(new DateTime(2024, 12, 12, 21, 38, 1, 9, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Smartwatch", 199.99m, 120 },
                    { 18, 6, new DateTimeOffset(new DateTime(2024, 4, 8, 13, 0, 21, 980, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "High-resolution DSLR for photography", null, new DateTimeOffset(new DateTime(2024, 4, 16, 2, 35, 22, 503, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Camera", 799.00m, 15 },
                    { 19, 5, new DateTimeOffset(new DateTime(2024, 4, 7, 12, 20, 29, 937, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Bluetooth speakers with deep bass", null, new DateTimeOffset(new DateTime(2024, 5, 15, 1, 21, 40, 484, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Speakers", 99.95m, 80 },
                    { 20, 8, new DateTimeOffset(new DateTime(2025, 2, 20, 20, 43, 2, 442, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "High-speed Wi-Fi router for home", null, new DateTimeOffset(new DateTime(2024, 8, 20, 19, 46, 45, 354, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Router", 129.00m, 40 },
                    { 21, 10, new DateTimeOffset(new DateTime(2024, 10, 1, 0, 25, 47, 10, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "1TB portable SSD for storage", null, new DateTimeOffset(new DateTime(2024, 8, 12, 16, 39, 24, 260, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "External Drive", 149.99m, 100 },
                    { 22, 7, new DateTimeOffset(new DateTime(2024, 10, 5, 22, 59, 19, 419, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Automatic coffee machine with timer", null, new DateTimeOffset(new DateTime(2025, 3, 19, 19, 31, 41, 968, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Coffee Maker", 89.00m, 35 },
                    { 23, 2, new DateTimeOffset(new DateTime(2024, 4, 5, 10, 52, 3, 641, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Next-gen console for immersive gaming", null, new DateTimeOffset(new DateTime(2024, 4, 26, 10, 13, 37, 648, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Gaming Console", 499.99m, 20 },
                    { 24, 2, new DateTimeOffset(new DateTime(2024, 7, 15, 21, 14, 15, 872, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "HD projector for home theater", null, new DateTimeOffset(new DateTime(2024, 12, 28, 20, 29, 18, 894, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Projector", 399.50m, 10 },
                    { 25, 4, new DateTimeOffset(new DateTime(2024, 5, 19, 21, 2, 55, 54, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Lightweight band for step tracking", null, new DateTimeOffset(new DateTime(2024, 5, 10, 17, 19, 57, 786, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Fitness Band", 39.99m, 140 },
                    { 26, 4, new DateTimeOffset(new DateTime(2025, 1, 11, 2, 34, 27, 27, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Adjustable LED lamp for workspace", null, new DateTimeOffset(new DateTime(2024, 5, 27, 1, 23, 13, 772, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Desk Lamp", 29.95m, 70 },
                    { 27, 5, new DateTimeOffset(new DateTime(2024, 12, 14, 8, 11, 10, 59, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Compact microwave for quick meals", null, new DateTimeOffset(new DateTime(2025, 3, 26, 14, 27, 34, 762, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), "Microwave", 109.00m, 45 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId1",
                table: "AspNetUserRoles",
                column: "RoleId1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId1",
                table: "AspNetUserRoles",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_CartId",
                table: "CartItems",
                column: "CartId");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductId",
                table: "CartItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                table: "Carts",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                table: "Categories",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ProductId",
                table: "OrderItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Name",
                table: "Products",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CartItems");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
