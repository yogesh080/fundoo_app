using Microsoft.EntityFrameworkCore.Migrations;

namespace RepositoryLayer.Migrations
{
    public partial class third_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CollaboratorTable",
                columns: table => new
                {
                    CollaboratorID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollaboratedEmail = table.Column<string>(nullable: true),
                    NotesId = table.Column<long>(nullable: false),
                    UserId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollaboratorTable", x => x.CollaboratorID);
                    table.ForeignKey(
                        name: "FK_CollaboratorTable_NotesTable_NotesId",
                        column: x => x.NotesId,
                        principalTable: "NotesTable",
                        principalColumn: "NotesId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CollaboratorTable_UserTable_UserId",
                        column: x => x.UserId,
                        principalTable: "UserTable",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorTable_NotesId",
                table: "CollaboratorTable",
                column: "NotesId");

            migrationBuilder.CreateIndex(
                name: "IX_CollaboratorTable_UserId",
                table: "CollaboratorTable",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollaboratorTable");
        }
    }
}
