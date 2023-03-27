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
                name: "Dialogue",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Speaker = table.Column<string>(type: "TEXT", nullable: false),
                    Text = table.Column<string>(type: "TEXT", nullable: false),
                    Order = table.Column<int>(type: "INTEGER", nullable: false),
                    IsQuestion = table.Column<bool>(type: "INTEGER", nullable: false),
                    DialogueId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dialogue", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dialogue_Dialogue_DialogueId",
                        column: x => x.DialogueId,
                        principalTable: "Dialogue",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Scene",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    ProjectId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scene", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Scene_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DialogueBlocks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DialogueId = table.Column<int>(type: "INTEGER", nullable: false),
                    X = table.Column<int>(type: "INTEGER", nullable: false),
                    Y = table.Column<int>(type: "INTEGER", nullable: false),
                    Width = table.Column<int>(type: "INTEGER", nullable: false),
                    Height = table.Column<int>(type: "INTEGER", nullable: false),
                    Color = table.Column<string>(type: "TEXT", nullable: false),
                    SceneId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DialogueBlocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DialogueBlocks_Dialogue_DialogueId",
                        column: x => x.DialogueId,
                        principalTable: "Dialogue",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DialogueBlocks_Scene_SceneId",
                        column: x => x.SceneId,
                        principalTable: "Scene",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dialogue_DialogueId",
                table: "Dialogue",
                column: "DialogueId");

            migrationBuilder.CreateIndex(
                name: "IX_DialogueBlocks_DialogueId",
                table: "DialogueBlocks",
                column: "DialogueId");

            migrationBuilder.CreateIndex(
                name: "IX_DialogueBlocks_SceneId",
                table: "DialogueBlocks",
                column: "SceneId");

            migrationBuilder.CreateIndex(
                name: "IX_Scene_ProjectId",
                table: "Scene",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DialogueBlocks");

            migrationBuilder.DropTable(
                name: "Dialogue");

            migrationBuilder.DropTable(
                name: "Scene");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
