using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TienThoBookStore.Domain.Entities;
using TienThoBookStore.Infrastructure.Configurations;
using System.Reflection.Emit;

namespace TienThoBookStore.Infrastructure.Contexts
{
    public class TienThoBookStoreDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        public TienThoBookStoreDbContext(DbContextOptions<TienThoBookStoreDbContext> options)
            : base(options)
        {
        }

        // DbSet cho Domain Entities
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<Highlight> Highlights { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<ReadingProgress> ReadingProgresses { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionDetail> TransactionDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Gọi base để Identity cấu hình các bảng mặc định
            base.OnModelCreating(builder);

            // Áp dụng tự động các Configuration đã tạo trong Assembly Configurations
            builder.ApplyConfigurationsFromAssembly(typeof(CategoryConfiguration).Assembly);

            // --- SEED CATEGORY ---
            builder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Tiểu thuyết" },
                new Category { CategoryId = 2, Name = "Khoa học" },
                new Category { CategoryId = 3, Name = "Kinh tế" },
                new Category { CategoryId = 4, Name = "Lịch sử" }
            );
            // --- SEED BOOK ---
            builder.Entity<Book>().HasData(
                new Book
                {
                    BookId = new Guid("11111111-1111-1111-1111-111111111111"),
                    Title = "Dế Mèn Phiêu Lưu Ký",
                    Author = "Tô Hoài",
                    Description = @"
        “Dế Mèn Phiêu Lưu Ký” là hành trình kỳ thú của chú Dế Mèn dũng cảm, ham khám phá
        và không ngừng vươn lên giữa bao thử thách. Từ những ngày an toàn trong hang ổ,
        Dế Mèn lạc vào thế giới rộng lớn đầy phức tạp: gặp gỡ bạn bè như Dế Trũi,
        trải qua giông bão thiên nhiên, đối mặt kẻ thù hung hãn và học được bài học
        “lá lành đùm lá rách”. Tác phẩm kết hợp sự hóm hỉnh, ngôn từ giản dị nhưng
        giàu tính nhân văn, giúp độc giả mọi lứa tuổi cảm nhận tình bạn, lòng quả
        cảm và thái độ nhân ái giữa cuộc sống muôn màu.",
                    CoverImage = "cover1.jpg",
                    Price = 120000m,
                    ContentSample = "11111111-1111-1111-1111-111111111111.pdf",
                    ContentFull = "11111111-1111-1111-1111-111111111111.pdf",
                    PublishedDate = new DateTime(2023, 1, 1),
                    CategoryId = 1
                },
                new Book
                {
                    BookId = new Guid("22222222-2222-2222-2222-222222222222"),
                    Title = "Chí Phèo",
                    Author = "Nam Cao",
                    Description = @"
        “Chí Phèo” là tác phẩm kinh điển khắc họa bi kịch xã hội đương thời qua hình
        ảnh người nông dân bị xã hội cuốn vào vòng vây mất nhân dạng. Chí, từ một kẻ
        chất phác, lương thiện, dần bị đẩy vào con đường tội lỗi sau bao bất công,
        từng nếm trải sự đay nghiến, lênh đênh giữa say sưa và tuyệt vọng. Khi gặp
        Thị Nở, Chí khát khao một giây phút bình yên, trở về kiếp người lương thiện.
        Tác phẩm đặt ra câu hỏi về quyền sống, lòng thương xót và trách nhiệm của xã
        hội đối với những mảnh đời bất hạnh.",
                    CoverImage = "cover2.jpg",
                    Price = 150000m,
                    ContentSample = "22222222-2222-2222-2222-222222222222.pdf",
                    ContentFull =   "22222222-2222-2222-2222-222222222222.pdf",
                    PublishedDate = new DateTime(2023, 1, 1),
                    CategoryId = 1
                },
                new Book
                {
                    BookId = new Guid("33333333-3333-3333-3333-333333333333"),
                    Title = "Lão Hạc",
                    Author = "Nam Cao",
                    Description = @"
        Câu chuyện “Lão Hạc” kể về người nông dân nghèo khổ, tảo tần nuôi con giữa
        những cánh đồng gió lộng. Lão Hạc yêu thương cậu Vàng như con đẻ, nhưng chịu
        áp lực cơm áo, ông buộc lòng bán đi vật nuôi duy nhất. Nỗi đau mất mát chất
        chồng, cuộc sống càng khốn khó hơn khi xã hội không còn chỗ cho lòng hi sinh
        trọn tình. Tác phẩm là bức tranh hiện thực đau đớn, lay động lòng người, phơi
        bày bi kịch gia đình và thân phận con người trước thử thách khắc nghiệt.",
                    CoverImage = "cover3.jpg",
                    Price = 130000m,
                    ContentSample = "33333333-3333-3333-3333-333333333333.pdf",
                    ContentFull = "33333333-3333-3333-3333-333333333333.pdf",
                    PublishedDate = new DateTime(2023, 1, 1),
                    CategoryId = 1
                },
                new Book
                {
                    BookId = new Guid("44444444-4444-4444-4444-444444444444"),
                    Title = "Tắt Đèn",
                    Author = "Ngô Tất Tố",
                    Description = @"
        “Tắt Đèn” khai thác đề tài nông dân Việt Nam dưới ánh sáng luật pháp thực
        dân – phong kiến. Nhân vật chị Dậu cùng chồng con vật lộn với cảnh đói rét,
        hà khắc của chủ điền, luật thuế chồng chất. Từ hình ảnh người vợ gồng mình
        bảo vệ con khỏi cảnh bị phạt chó đến khoảnh khắc thắt cổ tự vẫn, tác giả
        vẽ nên bức tranh xã hội tàn nhẫn và khát khao bứt phá khỏi kiếp nô lệ.
        Tác phẩm thôi thúc suy ngẫm về công lý, lương tri và giá trị của sinh mạng con người.",
                    CoverImage = "cover4.jpg",
                    Price = 110000m,
                    ContentSample = "44444444-4444-4444-4444-444444444444.pdf",
                    ContentFull = "44444444-4444-4444-4444-444444444444.pdf",
                    PublishedDate = new DateTime(2023, 1, 1),
                    CategoryId = 1
                },
                new Book
                {
                    BookId = new Guid("55555555-5555-5555-5555-555555555555"),
                    Title = "On the Origin of Species",
                    Author = "Charles Darwin",
                    Description = @"
        “On the Origin of Species” (Nguồn gốc các loài) là công trình trọng đại mở ra
        lý thuyết tiến hóa qua chọn lọc tự nhiên, giải thích cơ chế sinh học đưa muôn
        loài đến sự đa dạng hiện nay. Darwin phân tích di truyền, chuyển đổi hình thái,
        môi trường sống và sự cạnh tranh sinh tồn. Tác phẩm không chỉ thay đổi cách ta
        nhìn về nguồn gốc con người mà còn đặt nền móng cho sinh học hiện đại, kích
        thích tranh luận khoa học và triết học suốt nhiều thập kỷ.",
                    CoverImage = "cover5.jpg",
                    Price = 200000m,
                    ContentSample = "55555555-5555-5555-5555-555555555555.pdf",
                    ContentFull = "55555555-5555-5555-5555-555555555555.pdf",
                    PublishedDate = new DateTime(2023, 1, 1),
                    CategoryId = 2
                },
                new Book
                {
                    BookId = new Guid("66666666-6666-6666-6666-666666666666"),
                    Title = "The Science of Mechanics",
                    Author = "Ernst Mach",
                    Description = @"
        Trong “The Science of Mechanics”, Ernst Mach trình bày quan điểm xây dựng
        cơ học dựa trên quan sát thực nghiệm và cảm giác chủ quan. Ông chỉ ra rằng
        khái niệm lực, chuyển động nên được hiểu như biểu hiện của trải nghiệm
        người quan sát. Tác phẩm kết hợp vật lý lý thuyết và triết học khoa học,
        thách thức cách tiếp cận Newton truyền thống, đồng thời mở đường cho sự
        phát triển của tâm lý học nhận thức và thuyết tương đối sau này.",
                    CoverImage = "cover6.jpg",
                    Price = 180000m,
                    ContentSample = "66666666-6666-6666-6666-666666666666.pdf",
                    ContentFull = "66666666-6666-6666-6666-666666666666.pdf",
                    PublishedDate = new DateTime(2023, 1, 1),
                    CategoryId = 2
                },
                new Book
                {
                    BookId = new Guid("77777777-7777-7777-7777-777777777777"),
                    Title = "The Wealth of Nations",
                    Author = "Adam Smith",
                    Description = @"
        “The Wealth of Nations” là nền tảng của kinh tế học cổ điển,
        nơi Adam Smith phân tích cách thị trường tự do, phân công lao động và lợi ích
        cá nhân dẫn đến thịnh vượng chung. Ông đề xuất “bàn tay vô hình” điều tiết
        giá cả và sản xuất, kêu gọi hạn chế can thiệp chính phủ. Tác phẩm ảnh hưởng
        sâu rộng đến chính sách kinh tế, tư tưởng tự do và học thuyết kinh tế hiện
        đại, trở thành công cụ nghiên cứu không thể thiếu.",

                    CoverImage = "cover7.jpg",
                    Price = 220000m,
                    ContentSample = "77777777-7777-7777-7777-777777777777.pdf",
                    ContentFull = "77777777-7777-7777-7777-777777777777.pdf",
                    PublishedDate = new DateTime(2023, 1, 1),
                    CategoryId = 3
                },
                new Book
                {
                    BookId = new Guid("88888888-8888-8888-8888-888888888888"),
                    Title = "The Economic Consequences of the Peace",
                    Author = "John Maynard Keynes",
                    Description = @"
        Được viết ngay sau Thế chiến I, “The Economic Consequences of the Peace” là
        phân tích sắc bén của Keynes về hậu quả kinh tế từ Hiệp ước Versailles. Ông
        cảnh báo các điều khoản bồi thường quá khắc nghiệt sẽ phá vỡ nền kinh tế
        châu Âu, làm gia tăng bất ổn chính trị và khủng hoảng lạm phát. Tác phẩm là
        lời kêu gọi thận trọng trong thiết kế hòa ước và chính sách tái cấu trúc
        kinh tế hậu chiến, có tầm ảnh hưởng lâu dài đến chính sách quốc tế.",
                    CoverImage = "cover8.jpg",
                    Price = 210000m,
                    ContentSample = "88888888-8888-8888-8888-888888888888.pdf",
                    ContentFull = "88888888-8888-8888-8888-888888888888.pdf",
                    PublishedDate = new DateTime(2023, 1, 1),
                    CategoryId = 3
                },
                new Book
                {
                    BookId = new Guid("99999999-9999-9999-9999-999999999999"),
                    Title = "An Nam Chí Lược",
                    Author = "Lê Tắc",
                    Description = @"
        “An Nam Chí Lược” là biên niên sử do Lê Tắc biên soạn, ghi chép chi tiết
        lịch sử Đại Việt từ thời Tiền Lê đến Nhà Trần, đồng thời phản ánh quan hệ chính
        trị, văn hóa với Trung Quốc. Tác giả trình bày các sự kiện qua lăng kính
        quan lại lưu vong, kết hợp tư liệu chữ Hán và phong tục địa phương, tạo nên
        tư liệu quý giá cho nghiên cứu lịch sử và văn hoá Việt Nam thời trung đại.",
                    CoverImage = "cover9.jpg",
                    Price = 90000m,
                    ContentSample = "99999999-9999-9999-9999-999999999999.pdf",
                    ContentFull = "99999999-9999-9999-9999-999999999999.pdf",
                    PublishedDate = new DateTime(2023, 1, 1),
                    CategoryId = 4
                },
                new Book
                {
                    BookId = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                    Title = "Việt Nam Sử Lược",
                    Author = "Trần Trọng Kim",
                    Description = @"
        “Việt Nam Sử Lược” của Trần Trọng Kim là tổng hợp toàn diện lịch sử Việt
        Nam từ thuở sơ khai đến thời Nguyễn. Tác giả trình bày sự kiện rõ ràng,
        mạch lạc, kết hợp sử liệu chính thống và truyền thuyết dân gian, tiếp cận
        gần gũi với độc giả thời đại. Tác phẩm đặt nền móng cho nghiên cứu sử học
        Việt Nam hiện đại, được tái bản nhiều lần và trở thành giáo trình cơ bản
        cho sinh viên và người yêu sử.",
                    CoverImage = "cover10.jpg",
                    Price = 95000m,
                    ContentSample = "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa.pdf",
                    ContentFull = "aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa.pdf",
                    PublishedDate = new DateTime(2023, 1, 1),
                    CategoryId = 4
                }
            );



            // Cấu hình lại bảng AppUser nếu cần
            builder.Entity<AppUser>(entity =>
            {
                entity.ToTable("User");
                entity.Property(u => u.Id)
                      .HasColumnName("userId")
                      .IsRequired();
                entity.Property(u => u.Name)
                      .HasColumnName("name")
                      .HasMaxLength(256)
                      .IsRequired();
                entity.Property(u => u.Email)
                      .HasColumnName("email")
                      .HasMaxLength(256)
                      .IsRequired();
                entity.Property(u => u.PasswordHash)
                      .HasColumnName("passwordHash");
                entity.Property(u => u.PhoneNumber)
                      .HasColumnName("phone");
                entity.Property(u => u.Role)
                      .HasColumnName("role")
                      .IsRequired(false); 
                entity.Property(u => u.Verified)
                      .HasColumnName("verified")
                      .HasDefaultValue(false);
                entity.Property(u => u.CreatedAt)
                      .HasColumnName("createdAt")
                      .HasDefaultValueSql("GETDATE()");
                entity.Property(u=>u.EmailConfirmationSentAt)
                .HasColumnName("emailConfirmationSentAt")
                .HasDefaultValueSql("GETDATE()");
            });

            // Thay đổi tên các bảng Identity mặc định nếu cần
            builder.Entity<IdentityRole<Guid>>().ToTable("Role");
            builder.Entity<IdentityUserRole<Guid>>().ToTable("UserRoles");
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("UserClaims");
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("UserLogins");
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("RoleClaims");
            builder.Entity<IdentityUserToken<Guid>>().ToTable("UserTokens");
        }
    }
}
