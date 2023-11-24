using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aminos.Migrations.MaimaiDXDBMigrations
{
    /// <inheritdoc />
    public partial class V7_Detelte_UserId_For_UserAct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userId",
                table: "MaimaiDX_UserActs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "MaimaiDX_UserActs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
