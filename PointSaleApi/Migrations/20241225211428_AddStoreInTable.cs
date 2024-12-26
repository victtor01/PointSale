using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PointSaleApi.Migrations
{
    /// <inheritdoc />
    public partial class AddStoreInTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "StoreId",
                table: "Tables",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Tables_StoreId",
                table: "Tables",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tables_Stores_StoreId",
                table: "Tables",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tables_Stores_StoreId",
                table: "Tables");

            migrationBuilder.DropIndex(
                name: "IX_Tables_StoreId",
                table: "Tables");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "Tables");
        }
    }
}
