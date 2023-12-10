using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aminos.Migrations.MaimaiDXDBMigrations
{
    /// <inheritdoc />
    public partial class V20_Modify_UserDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "charaSlot",
                table: "MaimaiDX_UserDetails",
                type: "BLOB",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "charaLockSlot",
                table: "MaimaiDX_UserDetails",
                type: "BLOB",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 256,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "charaSlot",
                table: "MaimaiDX_UserDetails",
                type: "TEXT",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "BLOB",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "charaLockSlot",
                table: "MaimaiDX_UserDetails",
                type: "TEXT",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "BLOB",
                oldMaxLength: 256,
                oldNullable: true);
        }
    }
}
