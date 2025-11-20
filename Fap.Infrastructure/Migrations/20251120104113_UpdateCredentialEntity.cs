using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fap.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCredentialEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CertificateType",
                table: "Credentials",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Classification",
                table: "Credentials",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CompletionDate",
                table: "Credentials",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Credentials",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "FinalGrade",
                table: "Credentials",
                type: "decimal(5,2)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastViewedAt",
                table: "Credentials",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LetterGrade",
                table: "Credentials",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PdfFilePath",
                table: "Credentials",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PdfUrl",
                table: "Credentials",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QRCodeData",
                table: "Credentials",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReviewNotes",
                table: "Credentials",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ReviewedAt",
                table: "Credentials",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ReviewedBy",
                table: "Credentials",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RevocationReason",
                table: "Credentials",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RevokedAt",
                table: "Credentials",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RevokedBy",
                table: "Credentials",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SemesterId",
                table: "Credentials",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShareableUrl",
                table: "Credentials",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Credentials",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "StudentRoadmapId",
                table: "Credentials",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SubjectId",
                table: "Credentials",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Credentials",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VerificationHash",
                table: "Credentials",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "Credentials",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "TemplateFileUrl",
                table: "CertificateTemplates",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "CertificateTemplates",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<string>(
                name: "BackgroundImagePath",
                table: "CertificateTemplates",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "CertificateTemplates",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CustomStyles",
                table: "CertificateTemplates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FooterImagePath",
                table: "CertificateTemplates",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeaderImagePath",
                table: "CertificateTemplates",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CertificateTemplates",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "CertificateTemplates",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSample",
                table: "CertificateTemplates",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LogoImagePath",
                table: "CertificateTemplates",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Orientation",
                table: "CertificateTemplates",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PageSize",
                table: "CertificateTemplates",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SignatureImagePath",
                table: "CertificateTemplates",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TemplateContent",
                table: "CertificateTemplates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TemplateType",
                table: "CertificateTemplates",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TemplateVariables",
                table: "CertificateTemplates",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "CertificateTemplates",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "CertificateTemplates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CredentialRequests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CertificateType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    SubjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SemesterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StudentRoadmapId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CompletionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FinalGrade = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    LetterGrade = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Classification = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsAutoGenerated = table.Column<bool>(type: "bit", nullable: false),
                    StudentNotes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ProcessedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProcessedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AdminNotes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CredentialId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CredentialRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CredentialRequests_Credentials_CredentialId",
                        column: x => x.CredentialId,
                        principalTable: "Credentials",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CredentialRequests_Semesters_SemesterId",
                        column: x => x.SemesterId,
                        principalTable: "Semesters",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CredentialRequests_StudentRoadmaps_StudentRoadmapId",
                        column: x => x.StudentRoadmapId,
                        principalTable: "StudentRoadmaps",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CredentialRequests_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CredentialRequests_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Credentials_SemesterId",
                table: "Credentials",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_Credentials_StudentRoadmapId",
                table: "Credentials",
                column: "StudentRoadmapId");

            migrationBuilder.CreateIndex(
                name: "IX_Credentials_SubjectId",
                table: "Credentials",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_CredentialRequests_CredentialId",
                table: "CredentialRequests",
                column: "CredentialId");

            migrationBuilder.CreateIndex(
                name: "IX_CredentialRequests_SemesterId",
                table: "CredentialRequests",
                column: "SemesterId");

            migrationBuilder.CreateIndex(
                name: "IX_CredentialRequests_StudentId",
                table: "CredentialRequests",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_CredentialRequests_StudentRoadmapId",
                table: "CredentialRequests",
                column: "StudentRoadmapId");

            migrationBuilder.CreateIndex(
                name: "IX_CredentialRequests_SubjectId",
                table: "CredentialRequests",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Credentials_Semesters_SemesterId",
                table: "Credentials",
                column: "SemesterId",
                principalTable: "Semesters",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Credentials_StudentRoadmaps_StudentRoadmapId",
                table: "Credentials",
                column: "StudentRoadmapId",
                principalTable: "StudentRoadmaps",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Credentials_Subjects_SubjectId",
                table: "Credentials",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Credentials_Semesters_SemesterId",
                table: "Credentials");

            migrationBuilder.DropForeignKey(
                name: "FK_Credentials_StudentRoadmaps_StudentRoadmapId",
                table: "Credentials");

            migrationBuilder.DropForeignKey(
                name: "FK_Credentials_Subjects_SubjectId",
                table: "Credentials");

            migrationBuilder.DropTable(
                name: "CredentialRequests");

            migrationBuilder.DropIndex(
                name: "IX_Credentials_SemesterId",
                table: "Credentials");

            migrationBuilder.DropIndex(
                name: "IX_Credentials_StudentRoadmapId",
                table: "Credentials");

            migrationBuilder.DropIndex(
                name: "IX_Credentials_SubjectId",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "CertificateType",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "Classification",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "CompletionDate",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "FinalGrade",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "LastViewedAt",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "LetterGrade",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "PdfFilePath",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "PdfUrl",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "QRCodeData",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "ReviewNotes",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "ReviewedAt",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "ReviewedBy",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "RevocationReason",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "RevokedAt",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "RevokedBy",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "SemesterId",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "ShareableUrl",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "StudentRoadmapId",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "VerificationHash",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "Credentials");

            migrationBuilder.DropColumn(
                name: "BackgroundImagePath",
                table: "CertificateTemplates");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "CertificateTemplates");

            migrationBuilder.DropColumn(
                name: "CustomStyles",
                table: "CertificateTemplates");

            migrationBuilder.DropColumn(
                name: "FooterImagePath",
                table: "CertificateTemplates");

            migrationBuilder.DropColumn(
                name: "HeaderImagePath",
                table: "CertificateTemplates");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CertificateTemplates");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "CertificateTemplates");

            migrationBuilder.DropColumn(
                name: "IsSample",
                table: "CertificateTemplates");

            migrationBuilder.DropColumn(
                name: "LogoImagePath",
                table: "CertificateTemplates");

            migrationBuilder.DropColumn(
                name: "Orientation",
                table: "CertificateTemplates");

            migrationBuilder.DropColumn(
                name: "PageSize",
                table: "CertificateTemplates");

            migrationBuilder.DropColumn(
                name: "SignatureImagePath",
                table: "CertificateTemplates");

            migrationBuilder.DropColumn(
                name: "TemplateContent",
                table: "CertificateTemplates");

            migrationBuilder.DropColumn(
                name: "TemplateType",
                table: "CertificateTemplates");

            migrationBuilder.DropColumn(
                name: "TemplateVariables",
                table: "CertificateTemplates");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "CertificateTemplates");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "CertificateTemplates");

            migrationBuilder.AlterColumn<string>(
                name: "TemplateFileUrl",
                table: "CertificateTemplates",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "CertificateTemplates",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);
        }
    }
}
