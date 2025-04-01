using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECommerceService.API.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId1",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId1",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_RoleId1",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_UserId1",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "RoleId1",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "AspNetUserRoles");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 30, 4, 8, 25, 880, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 28, 5, 19, 24, 390, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 23, 17, 14, 25, 479, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 26, 8, 27, 18, 748, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 5, 4, 58, 41, 655, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 25, 5, 28, 17, 460, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 7, 8, 57, 9, 768, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 7, 29, 20, 39, 35, 921, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 7, 22, 13, 58, 6, 256, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 26, 12, 30, 56, 296, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 9, 23, 7, 18, 156, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 7, 10, 9, 42, 684, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 25, 9, 2, 5, 609, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 7, 17, 10, 0, 23, 694, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 18, 3, 25, 53, 602, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 7, 7, 23, 19, 8, 428, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 15, 17, 27, 7, 60, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 8, 22, 23, 57, 56, 947, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 26, 20, 40, 1, 403, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 12, 16, 2, 36, 32, 536, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 16, 15, 17, 12, 361, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 30, 9, 20, 20, 821, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 7, 7, 41, 24, 940, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 2, 21, 14, 37, 28, 56, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 26, 15, 2, 27, 79, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 22, 2, 49, 58, 491, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 5, 12, 55, 33, 56, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 15, 20, 59, 21, 245, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 8, 6, 12, 55, 35, 954, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 22, 0, 34, 26, 590, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 7, 16, 0, 16, 591, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 3, 30, 16, 17, 21, 148, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 17, 6, 56, 26, 72, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 8, 18, 6, 9, 44, 932, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 6, 10, 7, 6, 18, 214, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 8, 10, 56, 48, 102, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 7, 7, 11, 42, 25, 120, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 26, 8, 14, 45, 789, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 6, 23, 22, 36, 1, 570, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 8, 22, 1, 7, 58, 2, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 2, 15, 12, 13, 56, 888, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 6, 29, 10, 8, 13, 669, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 22, 23, 26, 13, 307, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 28, 0, 51, 0, 354, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 7, 24, 17, 10, 27, 629, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 20, 4, 8, 27, 84, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 12, 12, 23, 2, 19, 527, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 23, 14, 3, 18, 192, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 8, 30, 20, 15, 0, 638, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 4, 14, 10, 11, 51, 671, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 7, 4, 16, 15, 55, 474, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 15, 4, 44, 22, 911, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 18, 1, 46, 47, 509, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 7, 2, 1, 4, 15, 348, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 26, 9, 44, 45, 192, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 7, 12, 6, 43, 52, 565, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 6, 1, 9, 40, 5, 100, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 2, 12, 10, 40, 45, 561, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 6, 27, 2, 41, 29, 67, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 1, 11, 19, 59, 50, 554, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 8, 16, 43, 27, 989, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 12, 5, 2, 26, 959, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 9, 10, 3, 18, 38, 687, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 8, 4, 9, 5, 54, 748, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 5, 17, 39, 10, 382, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 4, 7, 33, 31, 163, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 12, 20, 9, 33, 33, 795, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 8, 11, 46, 42, 811, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 6, 29, 6, 21, 39, 717, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 1, 20, 18, 44, 12, 210, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 26, 9, 11, 24, 542, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 5, 18, 16, 0, 474, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 7, 1, 15, 10, 41, 526, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 2, 25, 13, 3, 0, 232, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUserRoles",
                type: "nvarchar(34)",
                maxLength: 34,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RoleId1",
                table: "AspNetUserRoles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "AspNetUserRoles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 4, 21, 8, 44, 1, 404, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 10, 29, 23, 42, 32, 257, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 3, 31, 10, 52, 1, 851, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 8, 29, 20, 23, 48, 276, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 8, 4, 11, 35, 57, 321, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 4, 15, 11, 59, 59, 911, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 1, 27, 17, 23, 47, 710, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 1, 18, 16, 13, 237, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 2, 15, 0, 58, 40, 542, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 7, 20, 5, 59, 17, 406, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 1, 28, 23, 29, 24, 349, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 10, 8, 32, 8, 541, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 29, 9, 24, 19, 607, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 10, 22, 7, 37, 645, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 9, 6, 14, 2, 40, 873, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 2, 17, 8, 11, 22, 417, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 12, 12, 2, 7, 47, 504, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 11, 10, 16, 52, 2, 205, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 9, 22, 14, 36, 51, 720, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 4, 18, 12, 34, 25, 639, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 6, 2, 8, 4, 10, 564, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 8, 30, 15, 34, 45, 103, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 7, 26, 20, 5, 29, 376, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 12, 27, 5, 0, 5, 616, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 3, 19, 42, 44, 451, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 22, 17, 22, 43, 471, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 6, 22, 5, 30, 7, 507, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 2, 4, 6, 28, 4, 692, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 9, 18, 39, 53, 719, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 13, 13, 7, 30, 199, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 4, 24, 0, 10, 11, 232, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 9, 24, 11, 55, 14, 257, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 2, 4, 0, 52, 15, 23, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 28, 5, 48, 32, 240, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 8, 10, 22, 58, 59, 741, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 6, 22, 20, 53, 14, 187, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 12, 29, 5, 5, 57, 642, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 18, 14, 2, 49, 313, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 6, 6, 21, 8, 35, 358, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 2, 8, 11, 21, 23, 231, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 11,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 29, 9, 5, 0, 756, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 7, 15, 3, 3, 9, 746, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 12,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 7, 8, 4, 34, 13, 418, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 2, 24, 20, 54, 56, 671, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 13,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 3, 28, 23, 17, 34, 584, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 8, 10, 7, 19, 56, 940, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 14,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 2, 13, 12, 18, 39, 333, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 8, 6, 2, 19, 37, 6, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 15,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 11, 18, 1, 55, 17, 207, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 7, 27, 16, 4, 43, 543, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 16,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 6, 15, 8, 12, 47, 564, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 4, 25, 7, 19, 8, 488, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 17,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 21, 16, 35, 16, 974, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 12, 12, 21, 38, 1, 9, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 18,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 4, 8, 13, 0, 21, 980, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 4, 16, 2, 35, 22, 503, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 19,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 4, 7, 12, 20, 29, 937, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 15, 1, 21, 40, 484, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 20,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 2, 20, 20, 43, 2, 442, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 8, 20, 19, 46, 45, 354, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 21,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 1, 0, 25, 47, 10, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 8, 12, 16, 39, 24, 260, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 22,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 10, 5, 22, 59, 19, 419, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 19, 19, 31, 41, 968, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 23,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 4, 5, 10, 52, 3, 641, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 4, 26, 10, 13, 37, 648, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 24,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 7, 15, 21, 14, 15, 872, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 12, 28, 20, 29, 18, 894, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 25,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 5, 19, 21, 2, 55, 54, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 10, 17, 19, 57, 786, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 26,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2025, 1, 11, 2, 34, 27, 27, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2024, 5, 27, 1, 23, 13, 772, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 27,
                columns: new[] { "CreatedDate", "LastModifiedDate" },
                values: new object[] { new DateTimeOffset(new DateTime(2024, 12, 14, 8, 11, 10, 59, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2025, 3, 26, 14, 27, 34, 762, DateTimeKind.Unspecified), new TimeSpan(0, 1, 0, 0, 0)) });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId1",
                table: "AspNetUserRoles",
                column: "RoleId1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId1",
                table: "AspNetUserRoles",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId1",
                table: "AspNetUserRoles",
                column: "RoleId1",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId1",
                table: "AspNetUserRoles",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
