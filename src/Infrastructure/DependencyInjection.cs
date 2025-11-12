using System.Text;
using Application.Interfaces;
using Application.Interfaces.UnitOfWorkInterfaces;
using Domain.Models.Entities;
using Domain.Models.Identity;
using Infrastructure.Data;
using Infrastructure.JwtAuthentication;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var jwtSettings = new JwtSettings();

        configuration.Bind(nameof(jwtSettings), jwtSettings);

        services.AddSingleton(jwtSettings);

        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserPrefernceRepository, UserPrefernceRepository>();
        services.AddScoped<ISystemConfigurationRepository, SystemConfigurationRepository>();

        services
            .AddIdentityCore<User>(o =>
            {
                o.User.RequireUniqueEmail = false;
                o.SignIn.RequireConfirmedPhoneNumber = false;
                o.SignIn.RequireConfirmedEmail = false;
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = true;
                o.Password.RequireUppercase = true;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 8;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
        var connectionString = configuration.GetConnectionString("DefaultConnection");
       // Console.WriteLine(connectionString);
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

        services
            .AddAuthentication(cfg =>
            {
                cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                cfg.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddJwtBearer(options =>
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.Key)
                    ),
                }
            );

        services.AddAuthorization();
        services.AddDistributedMemoryCache();
        services.AddScoped<ICurrentUserService, CurrentUserService>();;
        services.AddScoped<ISmsRepository, SmsRepository>();
        services.AddHttpClient<ISmsRepository, SmsRepository>();
        services.AddScoped<IOtpRepository, OtpRepository>();
        services.AddHttpContextAccessor();
        return services;
    }
}
