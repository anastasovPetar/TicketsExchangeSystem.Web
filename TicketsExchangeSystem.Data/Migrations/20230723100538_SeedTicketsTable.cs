using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketsExchangeSystem.Data.Migrations
{
    public partial class SeedTicketsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "OrderId",
                table: "Tickets",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "Address1", "Address2", "BuyerId", "CategoryId", "City", "Country", "CreatedOn", "CurrencyId", "EventDate", "ImageUrl", "OrderId", "PlaceOfEvent", "PricePerTicket", "Quantity", "SellerId", "Title", "isActive" },
                values: new object[] { new Guid("02621aa9-0cf5-4a5a-b1dd-6aff9eaa4039"), null, null, null, 4, "Sofia", "Bulgaria", new DateTime(2023, 7, 23, 10, 5, 38, 337, DateTimeKind.Utc).AddTicks(5808), 1, new DateTime(2023, 7, 25, 10, 5, 38, 337, DateTimeKind.Utc).AddTicks(5808), "https://picsum.photos/id/117/600/400", null, "National Theatre Ivan Vazov", 25.00m, 2, new Guid("a061ba74-b8be-4802-ac71-a5a7fa839815"), "ORPHEUS", true });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "Address1", "Address2", "BuyerId", "CategoryId", "City", "Country", "CreatedOn", "CurrencyId", "EventDate", "ImageUrl", "OrderId", "PlaceOfEvent", "PricePerTicket", "Quantity", "SellerId", "Title", "isActive" },
                values: new object[] { new Guid("3ca2369e-43f0-48b3-a415-736a2b8de7be"), null, null, null, 2, "Sofia", "Bulgaria", new DateTime(2023, 7, 22, 10, 5, 38, 337, DateTimeKind.Utc).AddTicks(5776), 1, new DateTime(2023, 7, 23, 10, 5, 38, 337, DateTimeKind.Utc).AddTicks(5774), "https://picsum.photos/id/117/600/400", null, "Valis Levski stadium", 30.00m, 5, new Guid("a061ba74-b8be-4802-ac71-a5a7fa839815"), "Concert 1", true });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "Address1", "Address2", "BuyerId", "CategoryId", "City", "Country", "CreatedOn", "CurrencyId", "EventDate", "ImageUrl", "OrderId", "PlaceOfEvent", "PricePerTicket", "Quantity", "SellerId", "Title", "isActive" },
                values: new object[] { new Guid("81899cd7-560e-45a9-9e09-2df649fc8b9f"), null, null, null, 2, "Sofia", "Bulgaria", new DateTime(2023, 7, 23, 10, 5, 38, 337, DateTimeKind.Utc).AddTicks(5803), 1, new DateTime(2023, 7, 23, 10, 5, 38, 337, DateTimeKind.Utc).AddTicks(5802), "https://picsum.photos/id/117/600/400", null, "Stadium Junak", 20.00m, 2, new Guid("a061ba74-b8be-4802-ac71-a5a7fa839815"), "Concert 2", true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("02621aa9-0cf5-4a5a-b1dd-6aff9eaa4039"));

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("3ca2369e-43f0-48b3-a415-736a2b8de7be"));

            migrationBuilder.DeleteData(
                table: "Tickets",
                keyColumn: "Id",
                keyValue: new Guid("81899cd7-560e-45a9-9e09-2df649fc8b9f"));

            migrationBuilder.AlterColumn<Guid>(
                name: "OrderId",
                table: "Tickets",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }
    }
}
