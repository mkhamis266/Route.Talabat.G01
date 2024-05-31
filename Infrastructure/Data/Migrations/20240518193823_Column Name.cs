using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Route.Talabat.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DelivreyMethods_DeliveyMethodId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "DeliveyMethodId",
                table: "Orders",
                newName: "DelivreyMethodId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_DeliveyMethodId",
                table: "Orders",
                newName: "IX_Orders_DelivreyMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DelivreyMethods_DelivreyMethodId",
                table: "Orders",
                column: "DelivreyMethodId",
                principalTable: "DelivreyMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DelivreyMethods_DelivreyMethodId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "DelivreyMethodId",
                table: "Orders",
                newName: "DeliveyMethodId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_DelivreyMethodId",
                table: "Orders",
                newName: "IX_Orders_DeliveyMethodId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DelivreyMethods_DeliveyMethodId",
                table: "Orders",
                column: "DeliveyMethodId",
                principalTable: "DelivreyMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
