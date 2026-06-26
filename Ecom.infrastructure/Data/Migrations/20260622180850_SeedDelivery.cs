using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Ecom.infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedDelivery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "DeliveryMethod",
                columns: new[] { "Id", "DeliveryTime", "Description", "Name", "price" },
                values: new object[,]
                {
                    { 1, "Only a week", "The fast Delivery in the world", "DHL", 15m },
                    { 2, "Only take two week", "Make your product save", "XXXX", 12m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "DeliveryMethod",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "DeliveryMethod",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
