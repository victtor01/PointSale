using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PointSaleApi.Migrations
{
  /// <inheritdoc />
  public partial class Init : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.CreateTable(
        name: "Managers",
        columns: table => new
        {
          Id = table.Column<Guid>(type: "uuid", nullable: false),
          Name = table.Column<string>(type: "text", nullable: false),
          Email = table.Column<string>(type: "text", nullable: false),
          Password = table.Column<string>(type: "text", nullable: false),
        },
        constraints: table =>
        {
          table.PrimaryKey("PK_Managers", x => x.Id);
        }
      );

      migrationBuilder.CreateIndex(name: "IX_Managers_Email", table: "Managers", column: "Email");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropTable(name: "Managers");
    }
  }
}
