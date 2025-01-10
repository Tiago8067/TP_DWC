using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tp_DWC.Shared.Migrations
{
    /// <inheritdoc />
    public partial class GestaoAssistenciasNewBussState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegistoFotografico_Assistencias_AssistenciaId",
                table: "RegistoFotografico");

            migrationBuilder.DropForeignKey(
                name: "FK_RegistoMaoDeObra_Assistencias_AssistenciaId",
                table: "RegistoMaoDeObra");

            migrationBuilder.DropForeignKey(
                name: "FK_RegistoMaterial_Assistencias_AssistenciaId",
                table: "RegistoMaterial");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RegistoMaterial",
                table: "RegistoMaterial");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RegistoMaoDeObra",
                table: "RegistoMaoDeObra");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RegistoFotografico",
                table: "RegistoFotografico");

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Assistencias");

            migrationBuilder.RenameTable(
                name: "RegistoMaterial",
                newName: "RegistoMaterials");

            migrationBuilder.RenameTable(
                name: "RegistoMaoDeObra",
                newName: "RegistoMaoDeObras");

            migrationBuilder.RenameTable(
                name: "RegistoFotografico",
                newName: "RegistoFotograficos");

            migrationBuilder.RenameIndex(
                name: "IX_RegistoMaterial_AssistenciaId",
                table: "RegistoMaterials",
                newName: "IX_RegistoMaterials_AssistenciaId");

            migrationBuilder.RenameIndex(
                name: "IX_RegistoMaoDeObra_AssistenciaId",
                table: "RegistoMaoDeObras",
                newName: "IX_RegistoMaoDeObras_AssistenciaId");

            migrationBuilder.RenameIndex(
                name: "IX_RegistoFotografico_AssistenciaId",
                table: "RegistoFotograficos",
                newName: "IX_RegistoFotograficos_AssistenciaId");

            migrationBuilder.AddColumn<Guid>(
                name: "EstadoId",
                table: "Assistencias",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_RegistoMaterials",
                table: "RegistoMaterials",
                column: "PK_RegistoMaterial");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RegistoMaoDeObras",
                table: "RegistoMaoDeObras",
                column: "PK_RegistoMaoDeObra");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RegistoFotograficos",
                table: "RegistoFotograficos",
                column: "PK_RegistoFotografico");

            migrationBuilder.CreateTable(
                name: "Estados",
                columns: table => new
                {
                    PK_Estado = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estados", x => x.PK_Estado);
                });

            migrationBuilder.CreateTable(
                name: "MudancasEstado",
                columns: table => new
                {
                    PK_MudancaEstado = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EstadoAtualId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NovoEstadoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DataMudanca = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AssistenciaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MudancasEstado", x => x.PK_MudancaEstado);
                    table.ForeignKey(
                        name: "FK_MudancasEstado_Assistencias_AssistenciaId",
                        column: x => x.AssistenciaId,
                        principalTable: "Assistencias",
                        principalColumn: "NumeroInterno",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MudancasEstado_Estados_EstadoAtualId",
                        column: x => x.EstadoAtualId,
                        principalTable: "Estados",
                        principalColumn: "PK_Estado",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MudancasEstado_Estados_NovoEstadoId",
                        column: x => x.NovoEstadoId,
                        principalTable: "Estados",
                        principalColumn: "PK_Estado",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assistencias_EstadoId",
                table: "Assistencias",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_MudancasEstado_AssistenciaId",
                table: "MudancasEstado",
                column: "AssistenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_MudancasEstado_EstadoAtualId",
                table: "MudancasEstado",
                column: "EstadoAtualId");

            migrationBuilder.CreateIndex(
                name: "IX_MudancasEstado_NovoEstadoId",
                table: "MudancasEstado",
                column: "NovoEstadoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Assistencias_Estados_EstadoId",
                table: "Assistencias",
                column: "EstadoId",
                principalTable: "Estados",
                principalColumn: "PK_Estado",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RegistoFotograficos_Assistencias_AssistenciaId",
                table: "RegistoFotograficos",
                column: "AssistenciaId",
                principalTable: "Assistencias",
                principalColumn: "NumeroInterno",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RegistoMaoDeObras_Assistencias_AssistenciaId",
                table: "RegistoMaoDeObras",
                column: "AssistenciaId",
                principalTable: "Assistencias",
                principalColumn: "NumeroInterno",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RegistoMaterials_Assistencias_AssistenciaId",
                table: "RegistoMaterials",
                column: "AssistenciaId",
                principalTable: "Assistencias",
                principalColumn: "NumeroInterno",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Assistencias_Estados_EstadoId",
                table: "Assistencias");

            migrationBuilder.DropForeignKey(
                name: "FK_RegistoFotograficos_Assistencias_AssistenciaId",
                table: "RegistoFotograficos");

            migrationBuilder.DropForeignKey(
                name: "FK_RegistoMaoDeObras_Assistencias_AssistenciaId",
                table: "RegistoMaoDeObras");

            migrationBuilder.DropForeignKey(
                name: "FK_RegistoMaterials_Assistencias_AssistenciaId",
                table: "RegistoMaterials");

            migrationBuilder.DropTable(
                name: "MudancasEstado");

            migrationBuilder.DropTable(
                name: "Estados");

            migrationBuilder.DropIndex(
                name: "IX_Assistencias_EstadoId",
                table: "Assistencias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RegistoMaterials",
                table: "RegistoMaterials");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RegistoMaoDeObras",
                table: "RegistoMaoDeObras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RegistoFotograficos",
                table: "RegistoFotograficos");

            migrationBuilder.DropColumn(
                name: "EstadoId",
                table: "Assistencias");

            migrationBuilder.RenameTable(
                name: "RegistoMaterials",
                newName: "RegistoMaterial");

            migrationBuilder.RenameTable(
                name: "RegistoMaoDeObras",
                newName: "RegistoMaoDeObra");

            migrationBuilder.RenameTable(
                name: "RegistoFotograficos",
                newName: "RegistoFotografico");

            migrationBuilder.RenameIndex(
                name: "IX_RegistoMaterials_AssistenciaId",
                table: "RegistoMaterial",
                newName: "IX_RegistoMaterial_AssistenciaId");

            migrationBuilder.RenameIndex(
                name: "IX_RegistoMaoDeObras_AssistenciaId",
                table: "RegistoMaoDeObra",
                newName: "IX_RegistoMaoDeObra_AssistenciaId");

            migrationBuilder.RenameIndex(
                name: "IX_RegistoFotograficos_AssistenciaId",
                table: "RegistoFotografico",
                newName: "IX_RegistoFotografico_AssistenciaId");

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                table: "Assistencias",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RegistoMaterial",
                table: "RegistoMaterial",
                column: "PK_RegistoMaterial");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RegistoMaoDeObra",
                table: "RegistoMaoDeObra",
                column: "PK_RegistoMaoDeObra");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RegistoFotografico",
                table: "RegistoFotografico",
                column: "PK_RegistoFotografico");

            migrationBuilder.AddForeignKey(
                name: "FK_RegistoFotografico_Assistencias_AssistenciaId",
                table: "RegistoFotografico",
                column: "AssistenciaId",
                principalTable: "Assistencias",
                principalColumn: "NumeroInterno",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RegistoMaoDeObra_Assistencias_AssistenciaId",
                table: "RegistoMaoDeObra",
                column: "AssistenciaId",
                principalTable: "Assistencias",
                principalColumn: "NumeroInterno",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RegistoMaterial_Assistencias_AssistenciaId",
                table: "RegistoMaterial",
                column: "AssistenciaId",
                principalTable: "Assistencias",
                principalColumn: "NumeroInterno",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
