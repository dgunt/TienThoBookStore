using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TienThoBookStore.Domain.Entities;
using TienThoBookStore.Infrastructure.Contexts;
using TienThoBookStore.Infrastructure.Repositories;
using TienThoBookStore.Infrastructure.Repositories.Implementations;
using TienThoBookStore.Infrastructure.Repositories.Interfaces;
using TienThoBookStore.Infrastructure.UnitOfWork;

namespace TienThoBookStore.Infrastructure
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration config)
        {
            var connectionString = config.GetConnectionString("TienThoBookStoreConnection");

            // DbContext
            services.AddDbContext<TienThoBookStoreDbContext>(opt =>
                opt.UseSqlServer(connectionString));

            // Repository – UnitOfWork
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IUnitOfWork, TienThoBookStore.Infrastructure.UnitOfWork.UnitOfWork>();

            // Identity
            services.AddIdentity<AppUser, IdentityRole<Guid>>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequiredLength = 6;
            })
            .AddEntityFrameworkStores<TienThoBookStoreDbContext>()
            .AddDefaultTokenProviders();

            return services;
        }
    }
}
