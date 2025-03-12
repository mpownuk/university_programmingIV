using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RENT_A_TOOL.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeysToWypozyczenie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "dataWypożyczenia",
                table: "Wypożyczenia");

            migrationBuilder.RenameColumn(
                name: "dataZwrotu",
                table: "Wypożyczenia",
                newName: "DataZwrotu");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataZwrotu",
                table: "Wypożyczenia",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataWypozyczenia",
                table: "Wypożyczenia",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ID_Klienta",
                table: "Wypożyczenia",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ID_Sprzet",
                table: "Wypożyczenia",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Wypożyczenia_ID_Klienta",
                table: "Wypożyczenia",
                column: "ID_Klienta");

            migrationBuilder.CreateIndex(
                name: "IX_Wypożyczenia_ID_Sprzet",
                table: "Wypożyczenia",
                column: "ID_Sprzet");

            migrationBuilder.AddForeignKey(
                name: "FK_Wypożyczenia_Sprzęt_ID_Sprzet",
                table: "Wypożyczenia",
                column: "ID_Sprzet",
                principalTable: "Sprzęt",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wypożyczenia_Użytkownicy_ID_Klienta",
                table: "Wypożyczenia",
                column: "ID_Klienta",
                principalTable: "Użytkownicy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wypożyczenia_Sprzęt_ID_Sprzet",
                table: "Wypożyczenia");

            migrationBuilder.DropForeignKey(
                name: "FK_Wypożyczenia_Użytkownicy_ID_Klienta",
                table: "Wypożyczenia");

            migrationBuilder.DropIndex(
                name: "IX_Wypożyczenia_ID_Klienta",
                table: "Wypożyczenia");

            migrationBuilder.DropIndex(
                name: "IX_Wypożyczenia_ID_Sprzet",
                table: "Wypożyczenia");

            migrationBuilder.DropColumn(
                name: "DataWypozyczenia",
                table: "Wypożyczenia");

            migrationBuilder.DropColumn(
                name: "ID_Klienta",
                table: "Wypożyczenia");

            migrationBuilder.DropColumn(
                name: "ID_Sprzet",
                table: "Wypożyczenia");

            migrationBuilder.RenameColumn(
                name: "DataZwrotu",
                table: "Wypożyczenia",
                newName: "dataZwrotu");

            migrationBuilder.AlterColumn<string>(
                name: "dataZwrotu",
                table: "Wypożyczenia",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "dataWypożyczenia",
                table: "Wypożyczenia",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
