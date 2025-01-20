using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class Patient_AssignDates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Assign_Date",
                table: "Patients",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Assign_Delete_Date",
                table: "Patients",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Assign_Update_Date",
                table: "Patients",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Assign_Date",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Assign_Delete_Date",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Assign_Update_Date",
                table: "Patients");
        }
    }
}
