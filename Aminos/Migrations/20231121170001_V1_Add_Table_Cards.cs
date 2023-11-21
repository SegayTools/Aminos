using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aminos.Migrations
{
	/// <inheritdoc />
	public partial class V1_Add_Table_Cards : Migration
	{
		/// <inheritdoc />
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.CreateTable(
				name: "General.Cards",
				columns: table => new
				{
					Id = table.Column<int>(type: "INTEGER", nullable: false)
						.Annotation("Sqlite:Autoincrement", true),
					ExtId = table.Column<string>(type: "TEXT", nullable: true),
					Luid = table.Column<string>(type: "TEXT", nullable: true),
					RegisterTime = table.Column<DateTime>(type: "TEXT", nullable: false),
					AccessTime = table.Column<DateTime>(type: "TEXT", nullable: false)
				},
				constraints: table =>
				{
					table.PrimaryKey("PK_General.Cards", x => x.Id);
				});

			migrationBuilder.CreateIndex(
				name: "IX_General.Cards_ExtId",
				table: "General.Cards",
				column: "ExtId",
				unique: true);

			migrationBuilder.CreateIndex(
				name: "IX_General.Cards_Luid",
				table: "General.Cards",
				column: "Luid",
				unique: true);
		}

		/// <inheritdoc />
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			migrationBuilder.DropTable(
				name: "General.Cards");
		}
	}
}
