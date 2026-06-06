using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlackJack.Migrations
{
    /// <inheritdoc />
    public partial class AddSpielStatistik : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Decks");

            migrationBuilder.DropColumn(
                name: "Punkte",
                table: "Karten");

            migrationBuilder.RenameColumn(
                name: "Farbe",
                table: "Karten",
                newName: "Beschreibung");

            migrationBuilder.AlterColumn<int>(
                name: "Wert",
                table: "Karten",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Art",
                table: "Karten",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "SpielStatistiken",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    GewinnVerlust = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Gewonnen = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpielStatistiken", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpielStatistiken");

            migrationBuilder.DropColumn(
                name: "Art",
                table: "Karten");

            migrationBuilder.RenameColumn(
                name: "Beschreibung",
                table: "Karten",
                newName: "Farbe");

            migrationBuilder.AlterColumn<string>(
                name: "Wert",
                table: "Karten",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Punkte",
                table: "Karten",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Decks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnzahlKarten = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Decks", x => x.Id);
                });
        }
    }
}
