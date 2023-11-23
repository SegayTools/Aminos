using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aminos.Migrations.MaimaiDXDBMigrations
{
    /// <inheritdoc />
    public partial class V4_Fix_SDEZ_Tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MaimaiDX_UserActs",
                table: "MaimaiDX_UserActs");

            migrationBuilder.DropIndex(
                name: "IX_MaimaiDX_UserActs_Id",
                table: "MaimaiDX_UserActs");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "MaimaiDX_UserActs",
                newName: "id");

            migrationBuilder.AlterColumn<ulong>(
                name: "id",
                table: "MaimaiDX_UserActs",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(ulong),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<ulong>(
                name: "UserActId",
                table: "MaimaiDX_UserActs",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0ul)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaimaiDX_UserActs",
                table: "MaimaiDX_UserActs",
                column: "UserActId");

            migrationBuilder.CreateIndex(
                name: "IX_MaimaiDX_UserActs_UserActId",
                table: "MaimaiDX_UserActs",
                column: "UserActId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MaimaiDX_UserActs",
                table: "MaimaiDX_UserActs");

            migrationBuilder.DropIndex(
                name: "IX_MaimaiDX_UserActs_UserActId",
                table: "MaimaiDX_UserActs");

            migrationBuilder.DropColumn(
                name: "UserActId",
                table: "MaimaiDX_UserActs");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "MaimaiDX_UserActs",
                newName: "Id");

            migrationBuilder.AlterColumn<ulong>(
                name: "Id",
                table: "MaimaiDX_UserActs",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(ulong),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaimaiDX_UserActs",
                table: "MaimaiDX_UserActs",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_MaimaiDX_UserActs_Id",
                table: "MaimaiDX_UserActs",
                column: "Id");
        }
    }
}
