using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.App.EF.Migrations
{
    public partial class InitialDbCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DependenceTypes",
                columns: table => new
                {
                    DependenceTypeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ParentToChildName = table.Column<string>(maxLength: 30, nullable: true),
                    ChildToParentName = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DependenceTypes", x => x.DependenceTypeId);
                });

            migrationBuilder.CreateTable(
                name: "FormRoles",
                columns: table => new
                {
                    FormRoleId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormRoles", x => x.FormRoleId);
                });

            migrationBuilder.CreateTable(
                name: "Forms",
                columns: table => new
                {
                    FormId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Year = table.Column<int>(nullable: false),
                    FormNumber = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Forms", x => x.FormId);
                });

            migrationBuilder.CreateTable(
                name: "GradeType",
                columns: table => new
                {
                    GradeTypeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeType", x => x.GradeTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    PersonId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 30, nullable: true),
                    LastName = table.Column<string>(maxLength: 30, nullable: true),
                    PersonalCode = table.Column<string>(maxLength: 15, nullable: true),
                    Sex = table.Column<string>(nullable: false),
                    Birthdate = table.Column<DateTime>(nullable: false),
                    Role = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonId);
                });

            migrationBuilder.CreateTable(
                name: "RemarkTypes",
                columns: table => new
                {
                    RemarkTypeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RemarkTypes", x => x.RemarkTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    SubjectId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 30, nullable: true),
                    Description = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.SubjectId);
                });

            migrationBuilder.CreateTable(
                name: "AbsenceReasons",
                columns: table => new
                {
                    AbsenceReasonId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StudentId = table.Column<int>(nullable: false),
                    CreatorId = table.Column<int>(nullable: false),
                    From = table.Column<DateTime>(nullable: false),
                    To = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Text = table.Column<string>(maxLength: 150, nullable: true),
                    IsAccepted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AbsenceReasons", x => x.AbsenceReasonId);
                    table.ForeignKey(
                        name: "FK_AbsenceReasons_Persons_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AbsenceReasons_Persons_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dependences",
                columns: table => new
                {
                    DependenceId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ParentId = table.Column<int>(nullable: false),
                    ChildId = table.Column<int>(nullable: false),
                    DependenceTypeId = table.Column<int>(nullable: false),
                    From = table.Column<DateTime>(nullable: false),
                    To = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dependences", x => x.DependenceId);
                    table.ForeignKey(
                        name: "FK_Dependences_Persons_ChildId",
                        column: x => x.ChildId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dependences_DependenceTypes_DependenceTypeId",
                        column: x => x.DependenceTypeId,
                        principalTable: "DependenceTypes",
                        principalColumn: "DependenceTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Dependences_Persons_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonForms",
                columns: table => new
                {
                    PersonFormId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FormRoleId = table.Column<int>(nullable: false),
                    PersonId = table.Column<int>(nullable: false),
                    From = table.Column<DateTime>(nullable: false),
                    To = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonForms", x => x.PersonFormId);
                    table.ForeignKey(
                        name: "FK_PersonForms_FormRoles_FormRoleId",
                        column: x => x.FormRoleId,
                        principalTable: "FormRoles",
                        principalColumn: "FormRoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonForms_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Remarks",
                columns: table => new
                {
                    RemarkId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SenderId = table.Column<int>(nullable: false),
                    RecipientId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Text = table.Column<string>(maxLength: 200, nullable: true),
                    RemarkTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Remarks", x => x.RemarkId);
                    table.ForeignKey(
                        name: "FK_Remarks_Persons_RecipientId",
                        column: x => x.RecipientId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Remarks_RemarkTypes_RemarkTypeId",
                        column: x => x.RemarkTypeId,
                        principalTable: "RemarkTypes",
                        principalColumn: "RemarkTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Remarks_Persons_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubjectGroups",
                columns: table => new
                {
                    SubjectGroupId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SubjectId = table.Column<int>(nullable: false),
                    Capacity = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectGroups", x => x.SubjectGroupId);
                    table.ForeignKey(
                        name: "FK_SubjectGroups_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "SubjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    GradeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<int>(nullable: false),
                    IsAbsent = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    AbsenceReasonId = table.Column<int>(nullable: false),
                    StudentId = table.Column<int>(nullable: false),
                    TeacherId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.GradeId);
                    table.ForeignKey(
                        name: "FK_Grades_AbsenceReasons_AbsenceReasonId",
                        column: x => x.AbsenceReasonId,
                        principalTable: "AbsenceReasons",
                        principalColumn: "AbsenceReasonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Grades_Persons_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Grades_Persons_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GradeColumns",
                columns: table => new
                {
                    GradeColumnId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    GradeTypeId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    LessonNumber = table.Column<int>(nullable: false),
                    Theme = table.Column<string>(maxLength: 100, nullable: true),
                    SubjectGroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GradeColumns", x => x.GradeColumnId);
                    table.ForeignKey(
                        name: "FK_GradeColumns_GradeType_GradeTypeId",
                        column: x => x.GradeTypeId,
                        principalTable: "GradeType",
                        principalColumn: "GradeTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GradeColumns_SubjectGroups_SubjectGroupId",
                        column: x => x.SubjectGroupId,
                        principalTable: "SubjectGroups",
                        principalColumn: "SubjectGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Homeworks",
                columns: table => new
                {
                    HomeworkId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SubjectGroupId = table.Column<int>(nullable: false),
                    Added = table.Column<DateTime>(nullable: false),
                    Deadline = table.Column<DateTime>(nullable: false),
                    TeacherId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Homeworks", x => x.HomeworkId);
                    table.ForeignKey(
                        name: "FK_Homeworks_SubjectGroups_SubjectGroupId",
                        column: x => x.SubjectGroupId,
                        principalTable: "SubjectGroups",
                        principalColumn: "SubjectGroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Homeworks_Persons_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonGroups",
                columns: table => new
                {
                    PersonGroupId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PersonId = table.Column<int>(nullable: false),
                    SubjectGroupId = table.Column<int>(nullable: false),
                    From = table.Column<DateTime>(nullable: false),
                    To = table.Column<DateTime>(nullable: false),
                    Comment = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonGroups", x => x.PersonGroupId);
                    table.ForeignKey(
                        name: "FK_PersonGroups_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonGroups_SubjectGroups_SubjectGroupId",
                        column: x => x.SubjectGroupId,
                        principalTable: "SubjectGroups",
                        principalColumn: "SubjectGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AbsenceReasons_CreatorId",
                table: "AbsenceReasons",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AbsenceReasons_StudentId",
                table: "AbsenceReasons",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Dependences_ChildId",
                table: "Dependences",
                column: "ChildId");

            migrationBuilder.CreateIndex(
                name: "IX_Dependences_DependenceTypeId",
                table: "Dependences",
                column: "DependenceTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Dependences_ParentId",
                table: "Dependences",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_GradeColumns_GradeTypeId",
                table: "GradeColumns",
                column: "GradeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GradeColumns_SubjectGroupId",
                table: "GradeColumns",
                column: "SubjectGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_AbsenceReasonId",
                table: "Grades",
                column: "AbsenceReasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_StudentId",
                table: "Grades",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_TeacherId",
                table: "Grades",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Homeworks_SubjectGroupId",
                table: "Homeworks",
                column: "SubjectGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Homeworks_TeacherId",
                table: "Homeworks",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonForms_FormRoleId",
                table: "PersonForms",
                column: "FormRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonForms_PersonId",
                table: "PersonForms",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonGroups_PersonId",
                table: "PersonGroups",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonGroups_SubjectGroupId",
                table: "PersonGroups",
                column: "SubjectGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Remarks_RecipientId",
                table: "Remarks",
                column: "RecipientId");

            migrationBuilder.CreateIndex(
                name: "IX_Remarks_RemarkTypeId",
                table: "Remarks",
                column: "RemarkTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Remarks_SenderId",
                table: "Remarks",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectGroups_SubjectId",
                table: "SubjectGroups",
                column: "SubjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dependences");

            migrationBuilder.DropTable(
                name: "Forms");

            migrationBuilder.DropTable(
                name: "GradeColumns");

            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "Homeworks");

            migrationBuilder.DropTable(
                name: "PersonForms");

            migrationBuilder.DropTable(
                name: "PersonGroups");

            migrationBuilder.DropTable(
                name: "Remarks");

            migrationBuilder.DropTable(
                name: "DependenceTypes");

            migrationBuilder.DropTable(
                name: "GradeType");

            migrationBuilder.DropTable(
                name: "AbsenceReasons");

            migrationBuilder.DropTable(
                name: "FormRoles");

            migrationBuilder.DropTable(
                name: "SubjectGroups");

            migrationBuilder.DropTable(
                name: "RemarkTypes");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Subjects");
        }
    }
}
