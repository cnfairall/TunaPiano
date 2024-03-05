using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TunaPiano.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Age = table.Column<int>(type: "integer", nullable: false),
                    Bio = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Songs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    ArtistId = table.Column<int>(type: "integer", nullable: false),
                    Album = table.Column<string>(type: "text", nullable: false),
                    Length = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Songs", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Artists",
                columns: new[] { "Id", "Age", "Bio", "Name" },
                values: new object[,]
                {
                    { 1, 40, "first fave", "No Doubt" },
                    { 2, 40, "stole my sister's tape", "Weezer" },
                    { 3, 40, "one album wonder", "Lauryn Hill" },
                    { 4, 80, "actually a diety", "Stevie Wonder" },
                    { 5, 50, "witch store vibes", "Tori Amos" },
                    { 6, 70, "former James Brown impersonator", "Charles Bradley" },
                    { 7, 35, "cool dude", "Kendrick Lamar" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Description" },
                values: new object[,]
                {
                    { 1, "Rock" },
                    { 2, "Hip-Hop" },
                    { 3, "R&B" }
                });

            migrationBuilder.InsertData(
                table: "Songs",
                columns: new[] { "Id", "Album", "ArtistId", "Length", "Title" },
                values: new object[,]
                {
                    { 1, "Tragic Kingdom", 1, 4, "Sunday Morning" },
                    { 2, "Tragic Kingdom", 1, 3, "Just a Girl" },
                    { 3, "Blue Album", 2, 5, "My Name is Jonas" },
                    { 4, "The Miseducation of Lauryn Hill", 3, 4, "That Thing" },
                    { 5, "Fulfillingness' First Finale", 4, 5, "They Won't Go When I Go" },
                    { 6, "Innervisions", 4, 6, "Higher Ground" },
                    { 7, "Under The Pink", 5, 4, "Cornflake Girl" },
                    { 8, "No Time For Dreaming", 6, 6, "Lovin' You Baby" },
                    { 9, "DAMN", 7, 4, "HUMBLE" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Artists");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Songs");
        }
    }
}
