using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TABP.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBookingModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Invoices",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "Bookings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "RoomId",
                table: "Bookings",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "CheckInDate", "CheckOutDate", "CreatedAt", "DeletedOn", "GuestRemarks", "HotelId", "InvoiceId", "IsDeleted", "PaymentMethod", "RoomId", "Status", "TotalPrice", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1L, new DateTime(2025, 7, 25, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 7, 27, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Remark 1", 1L, null, false, 1, 1L, (byte)0, 100m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1L },
                    { 2L, new DateTime(2025, 7, 26, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 7, 28, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Remark 2", 2L, null, false, 0, 2L, (byte)0, 200m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2L },
                    { 3L, new DateTime(2025, 7, 27, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 7, 29, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Remark 3", 3L, null, false, 1, 3L, (byte)0, 300m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3L },
                    { 4L, new DateTime(2025, 7, 28, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 7, 30, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Remark 4", 4L, null, false, 0, 4L, (byte)0, 400m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4L },
                    { 5L, new DateTime(2025, 7, 29, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2025, 7, 31, 0, 0, 0, 0, DateTimeKind.Local), new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Remark 5", 5L, null, false, 1, 5L, (byte)0, 500m, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5L }
                });

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "Id", "BookingId", "InvoiceNumber", "IssueDate", "Status", "TotalAmount" },
                values: new object[,]
                {
                    { 1L, 1L, "INV001", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 100m },
                    { 2L, 2L, "INV002", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 200m },
                    { 3L, 3L, "INV003", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 300m },
                    { 4L, 4L, "INV004", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 400m },
                    { 5L, 5L, "INV005", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 500m }
                });
        }
    }
}
