using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CapTwitch.Api.Migrations
{
    public partial class addValidated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ValidatedAt",
                table: "StreamRequests",
                type: "datetime(6)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValidatedAt",
                table: "StreamRequests");
        }
    }
}
