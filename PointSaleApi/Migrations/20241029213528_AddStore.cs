using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PointSaleApi.Migrations
{
  /// <inheritdoc />
  public partial class AddStore : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
        name: "Stores",
        columns: table => new
        {
          Id = table.Column<Guid>(type: "uuid", nullable: false),
          Name = table.Column<string>(type: "text", nullable: false),
          ManagerId = table.Column<Guid>(type: "uuid", nullable: false),
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Stores", x => x.Id);
          table.ForeignKey(
            name: "FK_Stores_Managers_ManagerId",
            column: x => x.ManagerId,
            principalTable: "Managers",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade
          );
        }
      );

      migrationBuilder.CreateIndex(
        name: "IX_Stores_ManagerId",
        table: "Stores",
        column: "ManagerId"
      );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(name: "Stores");
    }
  }
}
