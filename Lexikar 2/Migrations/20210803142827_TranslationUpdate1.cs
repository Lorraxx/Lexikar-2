using Microsoft.EntityFrameworkCore.Migrations;

namespace Lexikar_2.Migrations
{
    public partial class TranslationUpdate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Zdroj",
                table: "Translations",
                newName: "Translated");

            migrationBuilder.RenameColumn(
                name: "Verb",
                table: "Translations",
                newName: "Verbal");

            migrationBuilder.RenameColumn(
                name: "Systematika",
                table: "Translations",
                newName: "Systematics");

            migrationBuilder.RenameColumn(
                name: "Poznamka",
                table: "Translations",
                newName: "Source");

            migrationBuilder.RenameColumn(
                name: "Ost",
                table: "Translations",
                newName: "Other");

            migrationBuilder.RenameColumn(
                name: "Nom",
                table: "Translations",
                newName: "Nominal");

            migrationBuilder.RenameColumn(
                name: "FR",
                table: "Translations",
                newName: "Original");

            migrationBuilder.RenameColumn(
                name: "Definice",
                table: "Translations",
                newName: "Note");

            migrationBuilder.RenameColumn(
                name: "CS",
                table: "Translations",
                newName: "Definition");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Verbal",
                table: "Translations",
                newName: "Verb");

            migrationBuilder.RenameColumn(
                name: "Translated",
                table: "Translations",
                newName: "Zdroj");

            migrationBuilder.RenameColumn(
                name: "Systematics",
                table: "Translations",
                newName: "Systematika");

            migrationBuilder.RenameColumn(
                name: "Source",
                table: "Translations",
                newName: "Poznamka");

            migrationBuilder.RenameColumn(
                name: "Other",
                table: "Translations",
                newName: "Ost");

            migrationBuilder.RenameColumn(
                name: "Original",
                table: "Translations",
                newName: "FR");

            migrationBuilder.RenameColumn(
                name: "Note",
                table: "Translations",
                newName: "Definice");

            migrationBuilder.RenameColumn(
                name: "Nominal",
                table: "Translations",
                newName: "Nom");

            migrationBuilder.RenameColumn(
                name: "Definition",
                table: "Translations",
                newName: "CS");
        }
    }
}
