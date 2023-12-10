using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aminos.Migrations.MaimaiDXDBMigrations
{
    /// <inheritdoc />
    public partial class V16_Fix_DataTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCopyright",
                table: "MaimaiDX_TitleDatas");

            migrationBuilder.AddColumn<string>(
                name: "NormText",
                table: "MaimaiDX_TitleDatas",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NormText",
                table: "MaimaiDX_TitleDatas");

            migrationBuilder.AddColumn<bool>(
                name: "IsCopyright",
                table: "MaimaiDX_TitleDatas",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
