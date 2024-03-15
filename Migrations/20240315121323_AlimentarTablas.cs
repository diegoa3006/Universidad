using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiApiUniversidad.Migrations
{
    /// <inheritdoc />
    public partial class AlimentarTablas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Materias",
                columns: new[] { "Id", "Creditos", "Nombre" },
                values: new object[,]
                {
                    { 1, 3, "Creditos 1" },
                    { 2, 4, "Creditos 2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Materias",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Materias",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
