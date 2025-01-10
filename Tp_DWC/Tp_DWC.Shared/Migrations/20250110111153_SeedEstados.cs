using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tp_DWC.Shared.Migrations
{
    /// <inheritdoc />
    public partial class SeedEstados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Estados",
                columns: new[] { "PK_Estado", "Descricao" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "nova" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "em execução" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "à espera de material" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "resolvido" },
                    { new Guid("55555555-5555-5555-5555-555555555555"), "para entregar" },
                    { new Guid("66666666-6666-6666-6666-666666666666"), "entregue" },
                    { new Guid("77777777-7777-7777-7777-777777777777"), "pago" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Estados",
                keyColumn: "PK_Estado",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Estados",
                keyColumn: "PK_Estado",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Estados",
                keyColumn: "PK_Estado",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "Estados",
                keyColumn: "PK_Estado",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "Estados",
                keyColumn: "PK_Estado",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"));

            migrationBuilder.DeleteData(
                table: "Estados",
                keyColumn: "PK_Estado",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"));

            migrationBuilder.DeleteData(
                table: "Estados",
                keyColumn: "PK_Estado",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"));
        }
    }
}
