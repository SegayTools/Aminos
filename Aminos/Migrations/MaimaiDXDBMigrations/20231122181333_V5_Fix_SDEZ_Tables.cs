using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aminos.Migrations.MaimaiDXDBMigrations
{
    /// <inheritdoc />
    public partial class V5_Fix_SDEZ_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "encountMapNpcList",
                table: "MaimaiDX_UserExtends",
                type: "TEXT",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "selectedCardList",
                table: "MaimaiDX_UserExtends",
                type: "TEXT",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "charaLockSlot",
                table: "MaimaiDX_UserDetails",
                type: "TEXT",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "charaSlot",
                table: "MaimaiDX_UserDetails",
                type: "TEXT",
                maxLength: 256,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "encountMapNpcList",
                table: "MaimaiDX_UserExtends");

            migrationBuilder.DropColumn(
                name: "selectedCardList",
                table: "MaimaiDX_UserExtends");

            migrationBuilder.DropColumn(
                name: "charaLockSlot",
                table: "MaimaiDX_UserDetails");

            migrationBuilder.DropColumn(
                name: "charaSlot",
                table: "MaimaiDX_UserDetails");
        }
    }
}
