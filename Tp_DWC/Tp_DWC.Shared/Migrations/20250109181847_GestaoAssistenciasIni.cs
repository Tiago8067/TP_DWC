using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tp_DWC.Shared.Migrations
{
    /// <inheritdoc />
    public partial class GestaoAssistenciasIni : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DescricaoProblema",
                table: "Assistencias",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DescricaoProduto",
                table: "Assistencias",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Observacoes",
                table: "Assistencias",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "RegistoFotografico",
                columns: table => new
                {
                    PK_RegistoFotografico = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Foto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Observacoes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataRegisto = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssistenciaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistoFotografico", x => x.PK_RegistoFotografico);
                    table.ForeignKey(
                        name: "FK_RegistoFotografico_Assistencias_AssistenciaId",
                        column: x => x.AssistenciaId,
                        principalTable: "Assistencias",
                        principalColumn: "NumeroInterno",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegistoMaoDeObra",
                columns: table => new
                {
                    PK_RegistoMaoDeObra = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuantidadeHoras = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrecoUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrecoTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Observacoes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataRegisto = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssistenciaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistoMaoDeObra", x => x.PK_RegistoMaoDeObra);
                    table.ForeignKey(
                        name: "FK_RegistoMaoDeObra_Assistencias_AssistenciaId",
                        column: x => x.AssistenciaId,
                        principalTable: "Assistencias",
                        principalColumn: "NumeroInterno",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegistoMaterial",
                columns: table => new
                {
                    PK_RegistoMaterial = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuantidadeMaterial = table.Column<int>(type: "int", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrecoUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PrecoTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Observacoes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataRegisto = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssistenciaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegistoMaterial", x => x.PK_RegistoMaterial);
                    table.ForeignKey(
                        name: "FK_RegistoMaterial_Assistencias_AssistenciaId",
                        column: x => x.AssistenciaId,
                        principalTable: "Assistencias",
                        principalColumn: "NumeroInterno",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegistoFotografico_AssistenciaId",
                table: "RegistoFotografico",
                column: "AssistenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistoMaoDeObra_AssistenciaId",
                table: "RegistoMaoDeObra",
                column: "AssistenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_RegistoMaterial_AssistenciaId",
                table: "RegistoMaterial",
                column: "AssistenciaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegistoFotografico");

            migrationBuilder.DropTable(
                name: "RegistoMaoDeObra");

            migrationBuilder.DropTable(
                name: "RegistoMaterial");

            migrationBuilder.DropColumn(
                name: "DescricaoProblema",
                table: "Assistencias");

            migrationBuilder.DropColumn(
                name: "DescricaoProduto",
                table: "Assistencias");

            migrationBuilder.DropColumn(
                name: "Observacoes",
                table: "Assistencias");
        }
    }
}
