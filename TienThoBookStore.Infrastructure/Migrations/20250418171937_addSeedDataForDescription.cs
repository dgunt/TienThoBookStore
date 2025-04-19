using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TienThoBookStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addSeedDataForDescription : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "description",
                value: "\r\n        “Dế Mèn Phiêu Lưu Ký” là hành trình kỳ thú của chú Dế Mèn dũng cảm, ham khám phá\r\n        và không ngừng vươn lên giữa bao thử thách. Từ những ngày an toàn trong hang ổ,\r\n        Dế Mèn lạc vào thế giới rộng lớn đầy phức tạp: gặp gỡ bạn bè như Dế Trũi,\r\n        trải qua giông bão thiên nhiên, đối mặt kẻ thù hung hãn và học được bài học\r\n        “lá lành đùm lá rách”. Tác phẩm kết hợp sự hóm hỉnh, ngôn từ giản dị nhưng\r\n        giàu tính nhân văn, giúp độc giả mọi lứa tuổi cảm nhận tình bạn, lòng quả\r\n        cảm và thái độ nhân ái giữa cuộc sống muôn màu.");

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "description",
                value: "\r\n        “Chí Phèo” là tác phẩm kinh điển khắc họa bi kịch xã hội đương thời qua hình\r\n        ảnh người nông dân bị xã hội cuốn vào vòng vây mất nhân dạng. Chí, từ một kẻ\r\n        chất phác, lương thiện, dần bị đẩy vào con đường tội lỗi sau bao bất công,\r\n        từng nếm trải sự đay nghiến, lênh đênh giữa say sưa và tuyệt vọng. Khi gặp\r\n        Thị Nở, Chí khát khao một giây phút bình yên, trở về kiếp người lương thiện.\r\n        Tác phẩm đặt ra câu hỏi về quyền sống, lòng thương xót và trách nhiệm của xã\r\n        hội đối với những mảnh đời bất hạnh.");

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "description",
                value: "\r\n        Câu chuyện “Lão Hạc” kể về người nông dân nghèo khổ, tảo tần nuôi con giữa\r\n        những cánh đồng gió lộng. Lão Hạc yêu thương cậu Vàng như con đẻ, nhưng chịu\r\n        áp lực cơm áo, ông buộc lòng bán đi vật nuôi duy nhất. Nỗi đau mất mát chất\r\n        chồng, cuộc sống càng khốn khó hơn khi xã hội không còn chỗ cho lòng hi sinh\r\n        trọn tình. Tác phẩm là bức tranh hiện thực đau đớn, lay động lòng người, phơi\r\n        bày bi kịch gia đình và thân phận con người trước thử thách khắc nghiệt.");

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "description",
                value: "\r\n        “Tắt Đèn” khai thác đề tài nông dân Việt Nam dưới ánh sáng luật pháp thực\r\n        dân – phong kiến. Nhân vật chị Dậu cùng chồng con vật lộn với cảnh đói rét,\r\n        hà khắc của chủ điền, luật thuế chồng chất. Từ hình ảnh người vợ gồng mình\r\n        bảo vệ con khỏi cảnh bị phạt chó đến khoảnh khắc thắt cổ tự vẫn, tác giả\r\n        vẽ nên bức tranh xã hội tàn nhẫn và khát khao bứt phá khỏi kiếp nô lệ.\r\n        Tác phẩm thôi thúc suy ngẫm về công lý, lương tri và giá trị của sinh mạng con người.");

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "description",
                value: "\r\n        “On the Origin of Species” (Nguồn gốc các loài) là công trình trọng đại mở ra\r\n        lý thuyết tiến hóa qua chọn lọc tự nhiên, giải thích cơ chế sinh học đưa muôn\r\n        loài đến sự đa dạng hiện nay. Darwin phân tích di truyền, chuyển đổi hình thái,\r\n        môi trường sống và sự cạnh tranh sinh tồn. Tác phẩm không chỉ thay đổi cách ta\r\n        nhìn về nguồn gốc con người mà còn đặt nền móng cho sinh học hiện đại, kích\r\n        thích tranh luận khoa học và triết học suốt nhiều thập kỷ.");

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                column: "description",
                value: "\r\n        Trong “The Science of Mechanics”, Ernst Mach trình bày quan điểm xây dựng\r\n        cơ học dựa trên quan sát thực nghiệm và cảm giác chủ quan. Ông chỉ ra rằng\r\n        khái niệm lực, chuyển động nên được hiểu như biểu hiện của trải nghiệm\r\n        người quan sát. Tác phẩm kết hợp vật lý lý thuyết và triết học khoa học,\r\n        thách thức cách tiếp cận Newton truyền thống, đồng thời mở đường cho sự\r\n        phát triển của tâm lý học nhận thức và thuyết tương đối sau này.");

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"),
                column: "description",
                value: "\r\n        “The Wealth of Nations” là nền tảng của kinh tế học cổ điển,\r\n        nơi Adam Smith phân tích cách thị trường tự do, phân công lao động và lợi ích\r\n        cá nhân dẫn đến thịnh vượng chung. Ông đề xuất “bàn tay vô hình” điều tiết\r\n        giá cả và sản xuất, kêu gọi hạn chế can thiệp chính phủ. Tác phẩm ảnh hưởng\r\n        sâu rộng đến chính sách kinh tế, tư tưởng tự do và học thuyết kinh tế hiện\r\n        đại, trở thành công cụ nghiên cứu không thể thiếu.");

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                column: "description",
                value: "\r\n        Được viết ngay sau Thế chiến I, “The Economic Consequences of the Peace” là\r\n        phân tích sắc bén của Keynes về hậu quả kinh tế từ Hiệp ước Versailles. Ông\r\n        cảnh báo các điều khoản bồi thường quá khắc nghiệt sẽ phá vỡ nền kinh tế\r\n        châu Âu, làm gia tăng bất ổn chính trị và khủng hoảng lạm phát. Tác phẩm là\r\n        lời kêu gọi thận trọng trong thiết kế hòa ước và chính sách tái cấu trúc\r\n        kinh tế hậu chiến, có tầm ảnh hưởng lâu dài đến chính sách quốc tế.");

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"),
                column: "description",
                value: "\r\n        “An Nam Chí Lược” là biên niên sử do Lê Tắc biên soạn, ghi chép chi tiết\r\n        lịch sử Đại Việt từ thời Tiền Lê đến Nhà Trần, đồng thời phản ánh quan hệ chính\r\n        trị, văn hóa với Trung Quốc. Tác giả trình bày các sự kiện qua lăng kính\r\n        quan lại lưu vong, kết hợp tư liệu chữ Hán và phong tục địa phương, tạo nên\r\n        tư liệu quý giá cho nghiên cứu lịch sử và văn hoá Việt Nam thời trung đại.");

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                column: "description",
                value: "\r\n        “Việt Nam Sử Lược” của Trần Trọng Kim là tổng hợp toàn diện lịch sử Việt\r\n        Nam từ thuở sơ khai đến thời Nguyễn. Tác giả trình bày sự kiện rõ ràng,\r\n        mạch lạc, kết hợp sử liệu chính thống và truyền thuyết dân gian, tiếp cận\r\n        gần gũi với độc giả thời đại. Tác phẩm đặt nền móng cho nghiên cứu sử học\r\n        Việt Nam hiện đại, được tái bản nhiều lần và trở thành giáo trình cơ bản\r\n        cho sinh viên và người yêu sử.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"),
                column: "description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"),
                column: "description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"),
                column: "description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"),
                column: "description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"),
                column: "description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"),
                column: "description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"),
                column: "description",
                value: "");

            migrationBuilder.UpdateData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                column: "description",
                value: "");
        }
    }
}
