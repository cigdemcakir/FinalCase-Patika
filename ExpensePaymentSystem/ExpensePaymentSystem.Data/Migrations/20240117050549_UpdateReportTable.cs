using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpensePaymentSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateReportTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expense_Report_ReportId",
                schema: "dbo",
                table: "Expense");

            migrationBuilder.DropForeignKey(
                name: "FK_Report_User_UserId",
                schema: "dbo",
                table: "Report");

            migrationBuilder.AlterColumn<int>(
                name: "ReportId",
                schema: "dbo",
                table: "Expense",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Expense_Report_ReportId",
                schema: "dbo",
                table: "Expense",
                column: "ReportId",
                principalSchema: "dbo",
                principalTable: "Report",
                principalColumn: "ReportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Report_User_UserId",
                schema: "dbo",
                table: "Report",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "User",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expense_Report_ReportId",
                schema: "dbo",
                table: "Expense");

            migrationBuilder.DropForeignKey(
                name: "FK_Report_User_UserId",
                schema: "dbo",
                table: "Report");

            migrationBuilder.AlterColumn<int>(
                name: "ReportId",
                schema: "dbo",
                table: "Expense",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Expense_Report_ReportId",
                schema: "dbo",
                table: "Expense",
                column: "ReportId",
                principalSchema: "dbo",
                principalTable: "Report",
                principalColumn: "ReportId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Report_User_UserId",
                schema: "dbo",
                table: "Report",
                column: "UserId",
                principalSchema: "dbo",
                principalTable: "User",
                principalColumn: "UserId");
        }
    }
}
