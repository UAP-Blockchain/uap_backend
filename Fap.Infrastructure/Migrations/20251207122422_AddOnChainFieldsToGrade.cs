using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fap.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddOnChainFieldsToGrade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "OnChainBlockNumber",
                table: "Grades",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OnChainChainId",
                table: "Grades",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OnChainContractAddress",
                table: "Grades",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OnChainGradeId",
                table: "Grades",
                type: "decimal(20,0)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OnChainTxHash",
                table: "Grades",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OnChainBlockNumber",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "OnChainChainId",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "OnChainContractAddress",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "OnChainGradeId",
                table: "Grades");

            migrationBuilder.DropColumn(
                name: "OnChainTxHash",
                table: "Grades");
        }
    }
}
