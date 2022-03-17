using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Students.API.Services;
using Students.Domain.Interfaces;
using Students.Domain.Schools;
using Students.Domain.StudentPayments;
using Students.Domain.Students;
using Students.Infrastructure;
using Students.Infrastructure.Repositories;

namespace Students.API.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        // Configure DbContext with Scoped lifetime
        services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("StudentConnection"));
                //options.UseLazyLoadingProxies();
            }
        );
        services.AddScoped<Func<AppDbContext>>((provider) => provider.GetService<AppDbContext>);
        services.AddScoped<DbFactory>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
        .AddScoped(typeof(IRepository<>), typeof(Repository<>))
        .AddScoped<ISchoolRepository, SchoolRepository>()
        .AddScoped<IStudentRepository, StudentRepository>()
        .AddScoped<IStudentPaymentRepository, StudentPaymentRepository>();
    }
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services
            .AddScoped<SchoolService>()
            .AddScoped<StudentService>()
            .AddScoped<StudentPaymentService>();
    }
}