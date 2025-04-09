using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyProFile.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixBug : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "256f1176-beae-437c-b4e1-466789c6f24b", "AQAAAAIAAYagAAAAEGpFS4phxRdXck4VNHr8//iXs5f4PPire5XFkq5lxPoOAGVM6dhVp9Nmd8bfZAAbEQ==", "4c1259a2-1bea-420e-8571-36c4a02aae6a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "76375aae-000f-4944-bbdb-0dd5aebd2bbe", "AQAAAAIAAYagAAAAEPfqw3QaChxdqnRnbLMCiJUbdmTBAB3ZePV6waB800LyUr9MRIO4xLK/Tpv2wCawKA==", "9e471a3c-0618-47e1-a055-bb16e809f0e4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "11c08303-fdb6-4cab-8057-e2e4ea590c64", "AQAAAAIAAYagAAAAEGapOlDqgsT+NIpeq1N6RIQ5+C7zho43DHXPc+lBf2xwhzXkgIeWBxdb/PI84lQJSw==", "2a2eb34e-1cfa-40c4-a161-fd1a3072c8a0" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
