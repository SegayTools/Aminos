using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aminos.Migrations.MaimaiDXDBMigrations
{
    /// <inheritdoc />
    public partial class V22_Refatory_UserActivities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaimaiDX_UserDetails_MaimaiDX_UserActivities_UserActivityId",
                table: "MaimaiDX_UserDetails");

            migrationBuilder.DropTable(
                name: "MaimaiDX_UserActs");

            migrationBuilder.DropIndex(
                name: "IX_MaimaiDX_UserDetails_UserActivityId",
                table: "MaimaiDX_UserDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MaimaiDX_UserActivities",
                table: "MaimaiDX_UserActivities");

            migrationBuilder.DropColumn(
                name: "UserActivityId",
                table: "MaimaiDX_UserDetails");

            migrationBuilder.AlterColumn<ulong>(
                name: "Id",
                table: "MaimaiDX_UserActivities",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(ulong),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<ulong>(
                name: "ActivityId",
                table: "MaimaiDX_UserActivities",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0ul)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<ulong>(
                name: "UserDetailId",
                table: "MaimaiDX_UserActivities",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "kind",
                table: "MaimaiDX_UserActivities",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "param1",
                table: "MaimaiDX_UserActivities",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "param2",
                table: "MaimaiDX_UserActivities",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "param3",
                table: "MaimaiDX_UserActivities",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "param4",
                table: "MaimaiDX_UserActivities",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "sortNumber",
                table: "MaimaiDX_UserActivities",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaimaiDX_UserActivities",
                table: "MaimaiDX_UserActivities",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_MaimaiDX_UserActivities_UserDetailId",
                table: "MaimaiDX_UserActivities",
                column: "UserDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaimaiDX_UserActivities_MaimaiDX_UserDetails_UserDetailId",
                table: "MaimaiDX_UserActivities",
                column: "UserDetailId",
                principalTable: "MaimaiDX_UserDetails",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MaimaiDX_UserActivities_MaimaiDX_UserDetails_UserDetailId",
                table: "MaimaiDX_UserActivities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MaimaiDX_UserActivities",
                table: "MaimaiDX_UserActivities");

            migrationBuilder.DropIndex(
                name: "IX_MaimaiDX_UserActivities_UserDetailId",
                table: "MaimaiDX_UserActivities");

            migrationBuilder.DropColumn(
                name: "ActivityId",
                table: "MaimaiDX_UserActivities");

            migrationBuilder.DropColumn(
                name: "UserDetailId",
                table: "MaimaiDX_UserActivities");

            migrationBuilder.DropColumn(
                name: "kind",
                table: "MaimaiDX_UserActivities");

            migrationBuilder.DropColumn(
                name: "param1",
                table: "MaimaiDX_UserActivities");

            migrationBuilder.DropColumn(
                name: "param2",
                table: "MaimaiDX_UserActivities");

            migrationBuilder.DropColumn(
                name: "param3",
                table: "MaimaiDX_UserActivities");

            migrationBuilder.DropColumn(
                name: "param4",
                table: "MaimaiDX_UserActivities");

            migrationBuilder.DropColumn(
                name: "sortNumber",
                table: "MaimaiDX_UserActivities");

            migrationBuilder.AddColumn<ulong>(
                name: "UserActivityId",
                table: "MaimaiDX_UserDetails",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AlterColumn<ulong>(
                name: "Id",
                table: "MaimaiDX_UserActivities",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(ulong),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaimaiDX_UserActivities",
                table: "MaimaiDX_UserActivities",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "MaimaiDX_UserActs",
                columns: table => new
                {
                    UserActId = table.Column<ulong>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserActivityId = table.Column<ulong>(type: "INTEGER", nullable: true),
                    UserActivityId1 = table.Column<ulong>(type: "INTEGER", nullable: true),
                    id = table.Column<ulong>(type: "INTEGER", nullable: false),
                    kind = table.Column<int>(type: "INTEGER", nullable: false),
                    param1 = table.Column<int>(type: "INTEGER", nullable: false),
                    param2 = table.Column<int>(type: "INTEGER", nullable: false),
                    param3 = table.Column<int>(type: "INTEGER", nullable: false),
                    param4 = table.Column<int>(type: "INTEGER", nullable: false),
                    sortNumber = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaimaiDX_UserActs", x => x.UserActId);
                    table.ForeignKey(
                        name: "FK_MaimaiDX_UserActs_MaimaiDX_UserActivities_UserActivityId",
                        column: x => x.UserActivityId,
                        principalTable: "MaimaiDX_UserActivities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MaimaiDX_UserActs_MaimaiDX_UserActivities_UserActivityId1",
                        column: x => x.UserActivityId1,
                        principalTable: "MaimaiDX_UserActivities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MaimaiDX_UserDetails_UserActivityId",
                table: "MaimaiDX_UserDetails",
                column: "UserActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_MaimaiDX_UserActs_UserActId",
                table: "MaimaiDX_UserActs",
                column: "UserActId");

            migrationBuilder.CreateIndex(
                name: "IX_MaimaiDX_UserActs_UserActivityId",
                table: "MaimaiDX_UserActs",
                column: "UserActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_MaimaiDX_UserActs_UserActivityId1",
                table: "MaimaiDX_UserActs",
                column: "UserActivityId1");

            migrationBuilder.AddForeignKey(
                name: "FK_MaimaiDX_UserDetails_MaimaiDX_UserActivities_UserActivityId",
                table: "MaimaiDX_UserDetails",
                column: "UserActivityId",
                principalTable: "MaimaiDX_UserActivities",
                principalColumn: "Id");
        }
    }
}
