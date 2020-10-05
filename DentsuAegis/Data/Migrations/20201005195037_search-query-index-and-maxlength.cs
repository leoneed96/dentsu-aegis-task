using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class searchqueryindexandmaxlength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "SearchString",
                table: "SearchRequests",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SearchRequests_SearchString",
                table: "SearchRequests",
                column: "SearchString",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SearchRequests_SearchString",
                table: "SearchRequests");

            migrationBuilder.AlterColumn<string>(
                name: "SearchString",
                table: "SearchRequests",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);
        }
    }
}
