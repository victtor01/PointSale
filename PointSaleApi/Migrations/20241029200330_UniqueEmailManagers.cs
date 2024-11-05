using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PointSaleApi.Migrations
{
  /// <inheritdoc />
  public partial class UniqueEmailManagers : Migration
  {
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropIndex(name: "IX_Managers_Email", table: "Managers");

      migrationBuilder.CreateIndex(
        name: "IX_Managers_Email",
        table: "Managers",
        column: "Email",
        unique: true
      );
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DropIndex(name: "IX_Managers_Email", table: "Managers");

      migrationBuilder.CreateIndex(name: "IX_Managers_Email", table: "Managers", column: "Email");
    }
  }
}
