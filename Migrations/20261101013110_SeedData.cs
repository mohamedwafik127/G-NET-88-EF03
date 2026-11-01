using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ConsoleApp11.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name", "ParentCategoryId" },
                values: new object[,]
                {
                    { 1, "Software development courses", "Programming", null },
                    { 2, "Design and creativity courses", "Design", null }
                });

            migrationBuilder.InsertData(
                table: "Instructors",
                columns: new[] { "Id", "BIO", "EmailAddress", "FullName", "JoinedDate" },
                values: new object[,]
                {
                    { 1, "Senior .NET developer with 10 years of experience.", "ahmed.hassan@example.com", "Ahmed Hassan", new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, null, "sara.ali@example.com", "Sara Ali", new DateTime(2023, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "Front-end specialist and UI designer.", "omar.khaled@example.com", "Omar Khaled", new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name", "ParentCategoryId" },
                values: new object[,]
                {
                    { 3, "Server-side development", "Backend", 1 },
                    { 4, "Client-side development", "Frontend", 1 }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CategoryId", "Description", "InstructorId", "IsPublished", "Level", "Price", "PublishedDate", "Title" },
                values: new object[,]
                {
                    { 1, 1, "Learn the basics of C#.", 1, true, 0, 300m, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "C# Fundamentals" },
                    { 5, 2, null, 2, true, 0, 400m, new DateTime(2024, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "UI/UX Design Principles" },
                    { 6, 2, "Create scalable design systems.", 2, false, 2, 850m, null, "Design Systems" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name", "ParentCategoryId" },
                values: new object[] { 5, "Web development with .NET", "ASP.NET", 3 });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CategoryId", "Description", "InstructorId", "IsPublished", "Level", "Price", "PublishedDate", "Title" },
                values: new object[,]
                {
                    { 3, 3, "Performance and advanced mapping.", 1, false, 2, 900m, null, "Advanced EF Core" },
                    { 4, 4, "Build your first web pages.", 3, true, 0, 250m, new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "HTML and CSS Basics" },
                    { 2, 5, "Build REST APIs with ASP.NET Core.", 1, true, 1, 750m, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "ASP.NET Core Web API" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Instructors",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
