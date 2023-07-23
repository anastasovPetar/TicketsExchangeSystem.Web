using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketsExchangeSystem.Data.Migrations
{
    public partial class AddDateColumnToSeller : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AgreementDate",
                table: "Sellers",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AgreementDate",
                table: "Sellers");
        }
    }
}
