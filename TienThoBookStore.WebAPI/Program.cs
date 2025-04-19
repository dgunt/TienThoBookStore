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


namespace TienThoBookStore.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Lấy chuỗi kết nối từ appsettings.json
            var connectionString = builder.Configuration.GetConnectionString("TienThoBookStoreConnection");

            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            builder.Services.AddScoped<IBookService, BookService>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();



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

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
