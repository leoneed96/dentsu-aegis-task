using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Repositories",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    AuthorLogin = table.Column<string>(nullable: true),
                    AuthorAvatar = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true),
                    CodeLanguage = table.Column<string>(nullable: true),
                    Stars = table.Column<int>(nullable: false),
                    Forks = table.Column<int>(nullable: false),
                    LastUpdate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repositories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SearchRequests",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SearchString = table.Column<string>(nullable: true),
                    ExecutionDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchRequests", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SearchRequestAndRepositories",
                columns: table => new
                {
                    SearchRequestID = table.Column<int>(nullable: false),
                    RepositoryID = table.Column<int>(nullable: false)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SearchRequestAndRepositories");

            migrationBuilder.DropTable(
                name: "Repositories");

            migrationBuilder.DropTable(
                name: "SearchRequests");
        }
    }
}
