using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todo.Infrastructure.Persistences.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_menus",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IconUrl = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_menus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_todos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Descricao = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Tipo = table.Column<int>(type: "integer", nullable: false),
                    RepeticaoTipo = table.Column<int>(type: "integer", nullable: false),
                    DataConclusao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    DataHoraLembrar = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MenuId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_todos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_todos_tb_menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "tb_menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_todo_etapas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Descricao = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    DataExpiracao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Concluido = table.Column<bool>(type: "boolean", nullable: false),
                    TodoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_todo_etapas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tb_todo_etapas_tb_todos_TodoId",
                        column: x => x.TodoId,
                        principalTable: "tb_todos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_todo_etapas_TodoId",
                table: "tb_todo_etapas",
                column: "TodoId");

            migrationBuilder.CreateIndex(
                name: "IX_tb_todos_MenuId",
                table: "tb_todos",
                column: "MenuId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_todo_etapas");

            migrationBuilder.DropTable(
                name: "tb_todos");

            migrationBuilder.DropTable(
                name: "tb_menus");
        }
    }
}
