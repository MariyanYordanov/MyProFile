using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyProFile.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Mentors",
                columns: new[] { "Id", "FullName", "SubjectArea" },
                values: new object[,]
                {
                    { 1, "Васил Петров", "Програмиране" },
                    { 2, "Мария Николова", "UI/UX дизайн" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "AverageGrade", "Class", "FullName", "MentorId", "ProfilePicturePath", "Rating", "Speciality" },
                values: new object[,]
                {
                    { 1, 5.4000000000000004, "10А", "Иван Иванов", 1, "ivan.jpg", "напреднал", "Софтуерни технологии" },
                    { 2, 5.9000000000000004, "10Б", "Елица Георгиева", 2, "elitsa.jpg", "начинаещ", "Графичен дизайн" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Mentors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Mentors",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
