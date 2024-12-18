using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerLibrary.Data.Migrations
{
    /// <inheritdoc />
    public partial class ProfileCompToPatient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Foods_Food_Name",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "Is_profile_completed",
                table: "Users");

            migrationBuilder.AddColumn<bool>(
                name: "Is_profile_completed",
                table: "Patients",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Protein",
                table: "Foods",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Food_Name",
                table: "Foods",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Fat",
                table: "Foods",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Carbohydrate",
                table: "Foods",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Calorie",
                table: "Foods",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Foods_Food_Name",
                table: "Foods",
                column: "Food_Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Foods_Food_Name",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "Is_profile_completed",
                table: "Patients");

            migrationBuilder.AddColumn<bool>(
                name: "Is_profile_completed",
                table: "Users",
                type: "bit",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Protein",
                table: "Foods",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "Food_Name",
                table: "Foods",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<double>(
                name: "Fat",
                table: "Foods",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "Carbohydrate",
                table: "Foods",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "Calorie",
                table: "Foods",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.CreateIndex(
                name: "IX_Foods_Food_Name",
                table: "Foods",
                column: "Food_Name",
                unique: true,
                filter: "[Food_Name] IS NOT NULL");
        }
    }
}
