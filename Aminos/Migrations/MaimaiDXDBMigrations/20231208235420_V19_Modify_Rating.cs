using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aminos.Migrations.MaimaiDXDBMigrations
{
    /// <inheritdoc />
    public partial class V19_Modify_Rating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "userRating",
                table: "MaimaiDX_UserDetails",
                type: "BLOB",
                maxLength: 8192,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 8192,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "userRating",
                table: "MaimaiDX_UserDetails",
                type: "TEXT",
                maxLength: 8192,
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "BLOB",
                oldMaxLength: 8192,
                oldNullable: true);
        }
    }
}
