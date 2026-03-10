using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PontosTuristicos.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estados",
                columns: table => new
                {
                    Sigla = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estados", x => x.Sigla);
                });

            migrationBuilder.CreateTable(
                name: "Cidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EstadoSigla = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cidades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cidades_Estados_EstadoSigla",
                        column: x => x.EstadoSigla,
                        principalTable: "Estados",
                        principalColumn: "Sigla",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PontosTuristicos",
                columns: table => new
                {
                    IdPontosTuristicos = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCidade = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Localizacao = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CEP = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: true),
                    DataInclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PontosTuristicos", x => x.IdPontosTuristicos);
                    table.ForeignKey(
                        name: "FK_PontosTuristicos_Cidades_IdCidade",
                        column: x => x.IdCidade,
                        principalTable: "Cidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Estados",
                columns: new[] { "Sigla", "Nome" },
                values: new object[,]
                {
                    { "MG", "Minas Gerais" },
                    { "PR", "Paraná" },
                    { "RJ", "Rio de Janeiro" },
                    { "RS", "Rio Grande do Sul" },
                    { "SC", "Santa Catarina" },
                    { "SP", "São Paulo" }
                });

            migrationBuilder.InsertData(
                table: "Cidades",
                columns: new[] { "Id", "EstadoSigla", "Nome" },
                values: new object[,]
                {
                    { 1, "SP", "São Paulo" },
                    { 2, "RJ", "Rio de Janeiro" },
                    { 3, "SP", "Pompeia" },
                    { 4, "SP", "Tupã" }
                });

            migrationBuilder.InsertData(
                table: "PontosTuristicos",
                columns: new[] { "IdPontosTuristicos", "Ativo", "CEP", "DataInclusao", "Descricao", "IdCidade", "Localizacao", "Nome" },
                values: new object[,]
                {
                    { 1, true, "01310200", new DateTime(2026, 3, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), "Famoso museu na Avenida Paulista com arquitetura icônica.", 1, "Avenida Paulista, 1578", "Museu de Arte de São Paulo (MASP)" },
                    { 2, true, "22241120", new DateTime(2026, 3, 2, 14, 30, 0, 0, DateTimeKind.Unspecified), "Estátua Jesus Cristo, símbolo do Rio de Janeiro.", 2, "Parque Nacional da Tijuca", "Cristo Redentor" },
                    { 3, true, "17580000", new DateTime(2026, 3, 5, 9, 15, 0, 0, DateTimeKind.Unspecified), "Um museu sobre a evolução dos sistemas e hardware.", 3, "Centro, perto da praça principal", "Museu de Tecnologia" },
                    { 4, true, "17600380", new DateTime(2026, 3, 8, 10, 15, 0, 0, DateTimeKind.Unspecified), "Bela praça com a igreja Matriz.", 4, "Centro, perto da Câmara Municipal", "Praça da Bandeira" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cidades_EstadoSigla",
                table: "Cidades",
                column: "EstadoSigla");

            migrationBuilder.CreateIndex(
                name: "IX_PontosTuristicos_IdCidade",
                table: "PontosTuristicos",
                column: "IdCidade");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PontosTuristicos");

            migrationBuilder.DropTable(
                name: "Cidades");

            migrationBuilder.DropTable(
                name: "Estados");
        }
    }
}
