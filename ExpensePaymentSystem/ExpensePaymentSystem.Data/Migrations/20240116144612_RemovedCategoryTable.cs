using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpensePaymentSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovedCategoryTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expense_ExpenseCategory_CategoryId",
                schema: "dbo",
                table: "Expense");

            migrationBuilder.DropTable(
                name: "ExpenseCategory",
                schema: "dbo");

            migrationBuilder.DropIndex(
                name: "IX_Expense_CategoryId",
                schema: "dbo",
                table: "Expense");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                schema: "dbo",
                table: "Expense");

            migrationBuilder.AlterColumn<string>(
                name: "RejectionReason",
                schema: "dbo",
                table: "Expense",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "dbo",
                table: "Expense",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<string>(
                name: "Category",
                schema: "dbo",
                table: "Expense",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                schema: "dbo",
                table: "Expense");

            migrationBuilder.AlterColumn<string>(
                name: "RejectionReason",
                schema: "dbo",
                table: "Expense",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "dbo",
                table: "Expense",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                schema: "dbo",
                table: "Expense",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ExpenseCategory",
                schema: "dbo",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExpenseCategory", x => x.CategoryId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expense_CategoryId",
                schema: "dbo",
                table: "Expense",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Expense_ExpenseCategory_CategoryId",
                schema: "dbo",
                table: "Expense",
                column: "CategoryId",
                principalSchema: "dbo",
                principalTable: "ExpenseCategory",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
