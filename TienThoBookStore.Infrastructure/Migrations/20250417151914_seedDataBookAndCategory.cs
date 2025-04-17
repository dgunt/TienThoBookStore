using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TienThoBookStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seedDataBookAndCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "CategoryId", "name" },
                values: new object[,]
                {
                    { 1, "Tiểu thuyết" },
                    { 2, "Khoa học" },
                    { 3, "Kinh tế" },
                    { 4, "Lịch sử" }
                });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "BookId", "author", "CategoryId", "contentFull", "contentSample", "coverImage", "description", "price", "publishedDate", "title" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Tô Hoài", 1, "", "", "cover1.jpg", "", 120000m, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Dế Mèn Phiêu Lưu Ký" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Nam Cao", 1, "", "", "cover2.jpg", "", 150000m, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Chí Phèo" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "Nam Cao", 1, "", "", "cover3.jpg", "", 130000m, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lão Hạc" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "Ngô Tất Tố", 1, "", "", "cover4.jpg", "", 110000m, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tắt Đèn" },
                    { new Guid("55555555-5555-5555-5555-555555555555"), "Charles Darwin", 2, "", "", "cover5.jpg", "", 200000m, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "On the Origin of Species" },
                    { new Guid("66666666-6666-6666-6666-666666666666"), "Ernst Mach", 2, "", "", "cover6.jpg", "", 180000m, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Science of Mechanics" },
                    { new Guid("77777777-7777-7777-7777-777777777777"), "Adam Smith", 3, "", "", "cover7.jpg", "", 220000m, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Wealth of Nations" },
                    { new Guid("88888888-8888-8888-8888-888888888888"), "John Maynard Keynes", 3, "", "", "cover8.jpg", "", 210000m, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Economic Consequences of the Peace" },
                    { new Guid("99999999-9999-9999-9999-999999999999"), "Lê Tắc", 4, "", "", "cover9.jpg", "", 90000m, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "An Nam Chí Lược" },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "Trần Trọng Kim", 4, "", "", "cover10.jpg", "", 95000m, new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Việt Nam Sử Lược" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"));

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"));

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"));

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"));

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"));

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "CategoryId",
                keyValue: 4);
        }
    }
}
