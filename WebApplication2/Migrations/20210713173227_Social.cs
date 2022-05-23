using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplication2.Migrations
{
    public partial class Social : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(nullable: true),
                    surname = table.Column<string>(nullable: true),
                    login = table.Column<string>(nullable: true),
                    password = table.Column<string>(nullable: true),
                    photo = table.Column<string>(nullable: true),
                    type = table.Column<int>(nullable: false),
                    block = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Friends",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User1id = table.Column<int>(nullable: false),
                    User2id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friends", x => x.id);
                    table.ForeignKey(
                        name: "FK_Friends_Users_User1id",
                        column: x => x.User1id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Friends_Users_User2id",
                        column: x => x.User2id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Merjvacneries",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User1id = table.Column<int>(nullable: false),
                    User2id = table.Column<int>(nullable: false),
                    text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merjvacneries", x => x.id);
                    table.ForeignKey(
                        name: "FK_Merjvacneries_Users_User1id",
                        column: x => x.User1id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Merjvacneries_Users_User2id",
                        column: x => x.User2id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User1id = table.Column<int>(nullable: false),
                    User2id = table.Column<int>(nullable: false),
                    text = table.Column<string>(nullable: true),
                    status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.id);
                    table.ForeignKey(
                        name: "FK_Messages_Users_User1id",
                        column: x => x.User1id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Messages_Users_User2id",
                        column: x => x.User2id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(nullable: false),
                    photo = table.Column<string>(nullable: true),
                    desc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.id);
                    table.ForeignKey(
                        name: "FK_Photos_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User1id = table.Column<int>(nullable: false),
                    User2id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.id);
                    table.ForeignKey(
                        name: "FK_Requests_Users_User1id",
                        column: x => x.User1id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Requests_Users_User2id",
                        column: x => x.User2id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Friends_User1id",
                table: "Friends",
                column: "User1id");

            migrationBuilder.CreateIndex(
                name: "IX_Friends_User2id",
                table: "Friends",
                column: "User2id");

            migrationBuilder.CreateIndex(
                name: "IX_Merjvacneries_User1id",
                table: "Merjvacneries",
                column: "User1id");

            migrationBuilder.CreateIndex(
                name: "IX_Merjvacneries_User2id",
                table: "Merjvacneries",
                column: "User2id");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_User1id",
                table: "Messages",
                column: "User1id");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_User2id",
                table: "Messages",
                column: "User2id");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_userId",
                table: "Photos",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_User1id",
                table: "Requests",
                column: "User1id");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_User2id",
                table: "Requests",
                column: "User2id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Friends");

            migrationBuilder.DropTable(
                name: "Merjvacneries");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
