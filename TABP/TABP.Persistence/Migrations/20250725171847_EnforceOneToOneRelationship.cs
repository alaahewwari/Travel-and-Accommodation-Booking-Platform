using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TABP.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EnforceOneToOneRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "Bookings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "InvoiceId",
                table: "Bookings",
                type: "bigint",
                nullable: true);
        }
    }
}
