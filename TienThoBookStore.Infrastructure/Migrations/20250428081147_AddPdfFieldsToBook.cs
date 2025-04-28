using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TienThoBookStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPdfFieldsToBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "contentFull", "contentSample" },
                values: new object[] { "11111111-1111-1111-1111-111111111111.pdf", "11111111-1111-1111-1111-111111111111.pdf" });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "contentFull", "contentSample" },
                values: new object[] { "22222222-2222-2222-2222-222222222222.pdf", "22222222-2222-2222-2222-222222222222.pdf" });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "contentFull", "contentSample" },
                values: new object[] { "33333333-3333-3333-3333-333333333333.pdf", "33333333-3333-3333-3333-333333333333.pdf" });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                columns: new[] { "contentFull", "contentSample" },
                values: new object[] { "44444444-4444-4444-4444-444444444444.pdf", "44444444-4444-4444-4444-444444444444.pdf" });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "contentFull", "contentSample" },
                values: new object[] { "55555555-5555-5555-5555-555555555555.pdf", "55555555-5555-5555-5555-555555555555.pdf" });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                columns: new[] { "contentFull", "contentSample" },
                values: new object[] { "66666666-6666-6666-6666-666666666666.pdf", "66666666-6666-6666-6666-666666666666.pdf" });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"),
                columns: new[] { "contentFull", "contentSample" },
                values: new object[] { "77777777-7777-7777-7777-777777777777.pdf", "77777777-7777-7777-7777-777777777777.pdf" });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                columns: new[] { "contentFull", "contentSample" },
                values: new object[] { "88888888-8888-8888-8888-888888888888.pdf", "88888888-8888-8888-8888-888888888888.pdf" });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"),
                columns: new[] { "contentFull", "contentSample" },
                values: new object[] { "99999999-9999-9999-9999-999999999999.pdf", "99999999-9999-9999-9999-999999999999.pdf" });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "contentFull", "contentSample" },
                values: new object[] { "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa.pdf", "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa.pdf" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                columns: new[] { "contentFull", "contentSample" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                columns: new[] { "contentFull", "contentSample" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                columns: new[] { "contentFull", "contentSample" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                columns: new[] { "contentFull", "contentSample" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                columns: new[] { "contentFull", "contentSample" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                columns: new[] { "contentFull", "contentSample" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"),
                columns: new[] { "contentFull", "contentSample" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                columns: new[] { "contentFull", "contentSample" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"),
                columns: new[] { "contentFull", "contentSample" },
                values: new object[] { "", "" });

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                columns: new[] { "contentFull", "contentSample" },
                values: new object[] { "", "" });
        }
    }
}
