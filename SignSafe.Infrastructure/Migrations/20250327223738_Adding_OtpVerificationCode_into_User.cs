using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SignSafe.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Adding_OtpVerificationCode_into_User : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OtpVerificationCode",
                table: "User",
                type: "nchar(6)",
                fixedLength: true,
                maxLength: 6,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OtpVerificationCodeExpiration",
                table: "User",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OtpVerificationCode",
                table: "User");

            migrationBuilder.DropColumn(
                name: "OtpVerificationCodeExpiration",
                table: "User");
        }
    }
}
