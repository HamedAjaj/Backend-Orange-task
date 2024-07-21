using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orange_task.Repository.Migrations
{
    /// <inheritdoc />
    public partial class RemoveExpiryContract : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Customer_ContractDates",
                table: "Customers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_Customer_ContractDates",
                table: "Customers",
                sql: "ContractExpiryDate > ContractDate");
        }
    }
}
