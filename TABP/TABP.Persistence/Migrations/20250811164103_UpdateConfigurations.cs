using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TABP.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Amenities_Hotels_HotelId",
                table: "Amenities");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomClassAmenities_Amenities_AmenitiesId",
                table: "RoomClassAmenities");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomClassAmenities_RoomClasses_RoomClassesId",
                table: "RoomClassAmenities");

            migrationBuilder.DropIndex(
                name: "IX_Payments_BookingId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Amenities_HotelId",
                table: "Amenities");

            migrationBuilder.DropColumn(
                name: "HotelId",
                table: "Amenities");

            migrationBuilder.RenameColumn(
                name: "RoomClassesId",
                table: "RoomClassAmenities",
                newName: "RoomClassId");

            migrationBuilder.RenameColumn(
                name: "AmenitiesId",
                table: "RoomClassAmenities",
                newName: "AmenityId");

            migrationBuilder.RenameIndex(
                name: "IX_RoomClassAmenities_RoomClassesId",
                table: "RoomClassAmenities",
                newName: "IX_RoomClassAmenities_RoomClassId");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentMethodId",
                table: "Payments",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PaymentIntentId",
                table: "Payments",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FailureReason",
                table: "Payments",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ClientSecret",
                table: "Payments",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                table: "Invoices",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Invoices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Invoices",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BookingId",
                table: "Payments",
                column: "BookingId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomClassAmenities_Amenities_AmenityId",
                table: "RoomClassAmenities",
                column: "AmenityId",
                principalTable: "Amenities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomClassAmenities_RoomClasses_RoomClassId",
                table: "RoomClassAmenities",
                column: "RoomClassId",
                principalTable: "RoomClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomClassAmenities_Amenities_AmenityId",
                table: "RoomClassAmenities");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomClassAmenities_RoomClasses_RoomClassId",
                table: "RoomClassAmenities");

            migrationBuilder.DropIndex(
                name: "IX_Payments_BookingId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Invoices");

            migrationBuilder.RenameColumn(
                name: "RoomClassId",
                table: "RoomClassAmenities",
                newName: "RoomClassesId");

            migrationBuilder.RenameColumn(
                name: "AmenityId",
                table: "RoomClassAmenities",
                newName: "AmenitiesId");

            migrationBuilder.RenameIndex(
                name: "IX_RoomClassAmenities_RoomClassId",
                table: "RoomClassAmenities",
                newName: "IX_RoomClassAmenities_RoomClassesId");

            migrationBuilder.AlterColumn<string>(
                name: "PaymentMethodId",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PaymentIntentId",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "FailureReason",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ClientSecret",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalAmount",
                table: "Invoices",
                type: "decimal(10,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AddColumn<long>(
                name: "HotelId",
                table: "Amenities",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_BookingId",
                table: "Payments",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Amenities_HotelId",
                table: "Amenities",
                column: "HotelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Amenities_Hotels_HotelId",
                table: "Amenities",
                column: "HotelId",
                principalTable: "Hotels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomClassAmenities_Amenities_AmenitiesId",
                table: "RoomClassAmenities",
                column: "AmenitiesId",
                principalTable: "Amenities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomClassAmenities_RoomClasses_RoomClassesId",
                table: "RoomClassAmenities",
                column: "RoomClassesId",
                principalTable: "RoomClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
