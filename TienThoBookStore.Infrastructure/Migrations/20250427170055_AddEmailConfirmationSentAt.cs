using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TienThoBookStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEmailConfirmationSentAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "emailConfirmationSentAt",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "emailConfirmationSentAt",
                table: "User");
        }
    }
}
