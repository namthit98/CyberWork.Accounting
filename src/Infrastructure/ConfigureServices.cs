using System.Text;
using CyberWork.Accounting.Application.Common.Interfaces;
using CyberWork.Accounting.Domain.Entities;
using CyberWork.Accounting.Infrastructure.Identity;
using CyberWork.Accounting.Infrastructure.Persistence;
using CyberWork.Accounting.Infrastructure.Persistence.Interceptors;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                builder => builder
                .MigrationsAssembly(
                    typeof(ApplicationDbContext).Assembly.FullName
                )
            )
        );

        services.AddIdentityCore<AppUser>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
                opt.User.RequireUniqueEmail = true;
            })
            .AddRoles<AppRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["TokenKey"]));
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = key,
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                //    opt.Events = new JwtBearerEvents
                //    {
                //        OnMessageReceived = context =>
                //        {
                //            var accessToken = context.Request.Query["access_token"];
                //            var path = context.HttpContext.Request.Path;
                //            if (!string.IsNullOrEmpty(accessToken) && (path.StartsWithSegments("/chat")))
                //            {
                //                context.Token = accessToken;
                //            }
                //            return Task.CompletedTask;
                //        }
                //    };
                });

        services.AddTransient<ITokenServices, TokenServices>();
        services.AddTransient<IRoleServices, RoleServices>();
        services.AddTransient<IUserServices, UserServices>();
        services.AddScoped<IApplicationDbContext>(
            provider => provider.GetRequiredService<ApplicationDbContext>()
        );
        services.AddScoped<ApplicationDbContextInitialiser>();

        return services;
    }
}