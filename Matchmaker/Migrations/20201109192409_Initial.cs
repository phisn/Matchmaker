using Microsoft.EntityFrameworkCore.Migrations;

namespace Matchmaker.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Participants",
                columns: table => new
                {
                    ParticipantId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Mean = table.Column<double>(nullable: false),
                    StandardDeviation = table.Column<double>(nullable: false),
                    Rating = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participants", x => x.ParticipantId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Participants");
        }
    }
}
