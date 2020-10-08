using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class removem2m : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SearchRequestAndRepositories");

            migrationBuilder.AddColumn<int>(
                name: "SearchId",
                table: "Repositories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Repositories_SearchId",
                table: "Repositories",
                column: "SearchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Repositories_SearchRequests_SearchId",
                table: "Repositories",
                column: "SearchId",
                principalTable: "SearchRequests",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Repositories_SearchRequests_SearchId",
                table: "Repositories");

            migrationBuilder.DropIndex(
                name: "IX_Repositories_SearchId",
                table: "Repositories");

            migrationBuilder.DropColumn(
                name: "SearchId",
                table: "Repositories");

            migrationBuilder.CreateTable(
                name: "SearchRequestAndRepositories",
                columns: table => new
                {
                    SearchRequestID = table.Column<int>(type: "integer", nullable: false),
                    RepositoryID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchRequestAndRepositories", x => new { x.SearchRequestID, x.RepositoryID });
                    table.ForeignKey(
                        name: "FK_SearchRequestAndRepositories_Repositories_RepositoryID",
                        column: x => x.RepositoryID,
                        principalTable: "Repositories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SearchRequestAndRepositories_SearchRequests_SearchRequestID",
                        column: x => x.SearchRequestID,
                        principalTable: "SearchRequests",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SearchRequestAndRepositories_RepositoryID",
                table: "SearchRequestAndRepositories",
                column: "RepositoryID");
        }
    }
}
