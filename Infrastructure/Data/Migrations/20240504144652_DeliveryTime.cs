using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Route.Talabat.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class DeliveryTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeliveyTime",
                table: "DelivreyMethods",
                newName: "DeliveryTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeliveryTime",
                table: "DelivreyMethods",
                newName: "DeliveyTime");
        }
    }
}
