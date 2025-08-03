using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TABP.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddForeignKeysToModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "RoomClassId1",
                table: "Rooms",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BriefDescription",
                table: "RoomClasses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "RoomClasses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "RoomClasses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "CityId1",
                table: "Hotels",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OwnerId1",
                table: "Hotels",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "HotelId1",
                table: "HotelImages",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CityId1",
                table: "CityImages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "RoomId1",
                table: "CartItems",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UserId1",
                table: "CartItems",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RoomClassId1",
                table: "Rooms",
                column: "RoomClassId1");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_CityId1",
                table: "Hotels",
                column: "CityId1");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_OwnerId1",
                table: "Hotels",
                column: "OwnerId1");

            migrationBuilder.CreateIndex(
                name: "IX_HotelImages_HotelId1",
                table: "HotelImages",
                column: "HotelId1");

            migrationBuilder.CreateIndex(
                name: "IX_CityImages_CityId1",
                table: "CityImages",
                column: "CityId1");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_RoomId1",
                table: "CartItems",
                column: "RoomId1");

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_UserId1",
                table: "CartItems",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Rooms_RoomId1",
                table: "CartItems",
                column: "RoomId1",
                principalTable: "Rooms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Users_UserId1",
                table: "CartItems",
                column: "UserId1",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CityImages_Cities_CityId1",
                table: "CityImages",
                column: "CityId1",
                principalTable: "Cities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelImages_Hotels_HotelId1",
                table: "HotelImages",
                column: "HotelId1",
                principalTable: "Hotels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_Cities_CityId1",
                table: "Hotels",
                column: "CityId1",
                principalTable: "Cities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_Owners_OwnerId1",
                table: "Hotels",
                column: "OwnerId1",
                principalTable: "Owners",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_RoomClasses_RoomClassId1",
                table: "Rooms",
                column: "RoomClassId1",
                principalTable: "RoomClasses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Rooms_RoomId1",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Users_UserId1",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CityImages_Cities_CityId1",
                table: "CityImages");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelImages_Hotels_HotelId1",
                table: "HotelImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_Cities_CityId1",
                table: "Hotels");

            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_Owners_OwnerId1",
                table: "Hotels");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_RoomClasses_RoomClassId1",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_RoomClassId1",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Hotels_CityId1",
                table: "Hotels");

            migrationBuilder.DropIndex(
                name: "IX_Hotels_OwnerId1",
                table: "Hotels");

            migrationBuilder.DropIndex(
                name: "IX_HotelImages_HotelId1",
                table: "HotelImages");

            migrationBuilder.DropIndex(
                name: "IX_CityImages_CityId1",
                table: "CityImages");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_RoomId1",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_UserId1",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "RoomClassId1",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "BriefDescription",
                table: "RoomClasses");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "RoomClasses");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "RoomClasses");

            migrationBuilder.DropColumn(
                name: "CityId1",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "OwnerId1",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "HotelId1",
                table: "HotelImages");

            migrationBuilder.DropColumn(
                name: "CityId1",
                table: "CityImages");

            migrationBuilder.DropColumn(
                name: "RoomId1",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "CartItems");
        }
    }
}
