using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyProFile.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRefreshTokenUserProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RefreshToken",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Token = table.Column<string>(type: "TEXT", nullable: false),
                    Expires = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Created = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CreatedByIp = table.Column<string>(type: "TEXT", nullable: false),
                    Revoked = table.Column<DateTime>(type: "TEXT", nullable: true),
                    RevokedByIp = table.Column<string>(type: "TEXT", nullable: false),
                    ReplacedByToken = table.Column<string>(type: "TEXT", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b4797f2c-09fb-4776-950b-fb92a2398854", "AQAAAAIAAYagAAAAEFctgpr4nQUokvKuEpBh/nAh8H7GRgPuLFLHv8mYV44IEVlp9PmV7Voz+5fKeKtAbw==", "41e5af45-40fa-493d-8820-0068694151a9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9dfe57ef-b492-421f-b764-fd2b4f4958cc", "AQAAAAIAAYagAAAAEJ/JT9KEYqlXHYXOIJ3xYF8T3IA6/qzli27IE4nxgUAoQmi9q5ol+utMQCfyr9WIkg==", "b8a8b791-ce0d-4a0d-970c-621706ee2e14" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f85b0067-4c28-4c3a-a27f-20d44d0da648", "AQAAAAIAAYagAAAAEMxRpYH+2Yvej5jQTThM+nvSAe1NY5EJ9ZKsuGa8ba3zpypqpCafvLbZQ0ZEeK/caw==", "a62dd5fd-ce9a-4242-9154-5832214b5555" });

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserId",
                table: "RefreshToken",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RefreshToken");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5ad1d308-0721-4e3f-b5b9-441d89849850", "AQAAAAIAAYagAAAAEPYA6QPHEeI30s8QHsk/jSpFwLnUBjhP/VHyjwwTia1IE3PqGx2MQm69Zt6Zzy8i0g==", "f0075263-425b-4d79-b925-1d5b1a5b727e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c9e082c8-7d8e-4d31-a6d0-db948f9607ed", "AQAAAAIAAYagAAAAEDFkvpwI8Bk3i6qRUliu09YdI1Mglf2GslBD5oN4FfDyKD3TWFbAVPL4rv73bWMO8Q==", "cba65039-e55d-4521-a8b1-f311e0b76ad3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d417a953-7c02-405f-af43-17228700b7ad", "AQAAAAIAAYagAAAAEPBXS7gITnI4lyRBcQJdmTYOkuj08FiaofRkcRsAKewbkjDJilh4dr5t5ckANRRHzg==", "b7b9e45c-afc2-4159-9ed1-6ea2174ccf1d" });
        }
    }
}
