using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SignSafe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Add_Enabled_Into_User : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "User");
        }
    }
}
