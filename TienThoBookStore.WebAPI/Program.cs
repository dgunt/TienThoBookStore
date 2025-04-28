using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TienThoBookStore.Domain.Entities;
using TienThoBookStore.Infrastructure.Contexts;
using TienThoBookStore.Infrastructure.UnitOfWork;
using TienThoBookStore.Infrastructure.Repositories;
using TienThoBookStore.Application.Services;
using TienThoBookStore.Application.Mappings;
using TienThoBookStore.Application.Services.Implementations;
using TienThoBookStore.Application.Services.Interfaces;
using TienThoBookStore.WebAPI.Services;


namespace TienThoBookStore.WebAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowWebApp", policy =>
                {
                    policy.WithOrigins("https://localhost:7231")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });


            // Lấy chuỗi kết nối từ appsettings.json
            var connectionString = builder.Configuration.GetConnectionString("TienThoBookStoreConnection");

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();
            builder.Services.AddTransient<IEmailSender, SmtpEmailSender>();
            builder.Services.AddSingleton<PdfService>();





            // Đăng ký DbContext với SQL Server
            builder.Services.AddDbContext<TienThoBookStoreDbContext>(options =>
                options.UseSqlServer(connectionString));


            //Đăng ký AutoMapper
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);


            // Cấu hình ASP.NET Core Identity
            builder.Services.AddIdentity<AppUser, IdentityRole<Guid>>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<TienThoBookStoreDbContext>()
            .AddDefaultTokenProviders();
            
            builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
            opt.TokenLifespan = TimeSpan.FromMinutes(5)  // token sống 5 phút
);


            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var roleMgr = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
                var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                // 1) Tạo role "Admin" nếu chưa có
                if (!await roleMgr.RoleExistsAsync("Admin"))
                    await roleMgr.CreateAsync(new IdentityRole<Guid>("Admin"));

                // 2) Tạo user admin nếu chưa có
                var adminEmail = "vuducminhvn2003@gmail.com";
                var admin = await userMgr.FindByEmailAsync(adminEmail);
                if (admin == null)
                {
                    admin = new AppUser
                    {
                        UserName = adminEmail,
                        Email = adminEmail,
                        EmailConfirmed = true,
                        Name = "Administrator",    // admin luôn xác thực
                        Verified = true,// nếu bạn dùng trường Verified
                        CreatedAt = DateTime.UtcNow 
                    };
                    var result = await userMgr.CreateAsync(admin, "YourStrongP@ssw0rd!");
                    if (result.Succeeded)
                        await userMgr.AddToRoleAsync(admin, "Admin");
                }
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            
            

            app.UseHttpsRedirection();
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    ctx.Context.Response.Headers.Add("Access-Control-Allow-Origin", "https://localhost:7231");
                    ctx.Context.Response.Headers.Add("Access-Control-Allow-Methods", "GET,HEAD");
                }
            });
            
            app.UseRouting();
            app.UseCors("AllowWebApp");
            app.UseAuthorization();


            app.MapControllers();

            await app.RunAsync();
        }
    }
}
