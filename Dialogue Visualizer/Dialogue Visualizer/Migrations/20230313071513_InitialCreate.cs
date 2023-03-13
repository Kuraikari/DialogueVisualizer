using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dialogue_Visualizer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "_scene",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__scene", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "_dialogues",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Speaker = table.Column<string>(type: "TEXT", nullable: false),
                    Text = table.Column<string>(type: "TEXT", nullable: false),
                    IsQuestion = table.Column<bool>(type: "INTEGER", nullable: false),
                    FollowUpTextId = table.Column<int>(type: "INTEGER", nullable: false),
                    SceneId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__dialogues", x => x.Id);
                    table.ForeignKey(
                        name: "FK__dialogues__scene_SceneId",
                        column: x => x.SceneId,
                        principalTable: "_scene",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX__dialogues_SceneId",
                table: "_dialogues",
                column: "SceneId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "_dialogues");

            migrationBuilder.DropTable(
                name: "_scene");
        }
    }
}
