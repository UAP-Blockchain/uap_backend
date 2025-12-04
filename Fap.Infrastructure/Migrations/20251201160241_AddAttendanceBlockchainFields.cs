using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fap.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAttendanceBlockchainFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOnBlockchain",
                table: "Attendances",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "OnChainRecordId",
                table: "Attendances",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OnChainTransactionHash",
                table: "Attendances",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOnBlockchain",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "OnChainRecordId",
                table: "Attendances");

            migrationBuilder.DropColumn(
                name: "OnChainTransactionHash",
                table: "Attendances");
        }
    }
}
