using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TienThoBookStore.Application.Mappings;
using TienThoBookStore.Application.Services.Implementations;
using TienThoBookStore.Application.Services.Interfaces;


namespace TienThoBookStore.Application
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<ICategoryService, CategoryService>();

            // Email / PDF
            services.AddTransient<IEmailSender, SmtpEmailSender>();
            services.AddSingleton<PdfService>();
            return services;
        }
    }
}
