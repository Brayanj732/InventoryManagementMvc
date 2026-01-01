using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryManagementMvc.Migrations
{
    /// <inheritdoc />
    public partial class SerialCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SerialNumbers_Items_ItemId",
                table: "SerialNumbers");

            migrationBuilder.AddForeignKey(
                name: "FK_SerialNumbers_Items_ItemId",
                table: "SerialNumbers",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SerialNumbers_Items_ItemId",
                table: "SerialNumbers");

            migrationBuilder.AddForeignKey(
                name: "FK_SerialNumbers_Items_ItemId",
                table: "SerialNumbers",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id");
        }
    }
}
