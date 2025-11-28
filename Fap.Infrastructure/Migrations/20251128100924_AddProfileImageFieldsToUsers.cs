using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fap.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProfileImageFieldsToUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GradeComponents_Subjects_SubjectId",
                table: "GradeComponents");

            migrationBuilder.AddColumn<string>(
                name: "ProfileImagePublicId",
                table: "Users",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfileImageUrl",
                table: "Users",
                type: "nvarchar(1024)",
                maxLength: 1024,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GradeComponents_Subjects_SubjectId",
                table: "GradeComponents",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GradeComponents_Subjects_SubjectId",
                table: "GradeComponents");

            migrationBuilder.DropColumn(
                name: "ProfileImagePublicId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ProfileImageUrl",
                table: "Users");

            migrationBuilder.AddForeignKey(
                name: "FK_GradeComponents_Subjects_SubjectId",
                table: "GradeComponents",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
