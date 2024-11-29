using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace System_Erp.Migrations
{
    /// <inheritdoc />
    public partial class AtualizaTabelaUsuarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Alergias",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Altura",
                table: "Usuarios",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "AtualizacaoDeInformacoes",
                table: "Usuarios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CargoDoUsuario",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "EspecialidadeDoMedico",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Peso",
                table: "Usuarios",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "SolicitacaoDeCargo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    CargoSolicitado = table.Column<int>(type: "int", nullable: false),
                    DataSolicitacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SolicitacaoDeCargo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SolicitacaoDeCargo_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SolicitacaoDeCargo_UsuarioId",
                table: "SolicitacaoDeCargo",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SolicitacaoDeCargo");

            migrationBuilder.DropColumn(
                name: "Alergias",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Altura",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "AtualizacaoDeInformacoes",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "CargoDoUsuario",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "EspecialidadeDoMedico",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Peso",
                table: "Usuarios");
        }
    }
}
