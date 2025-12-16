using System.Reflection;
using Application;
using Domain.Models.Identity;
using Infrastructure;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Presentation.Seeding.Foods;
using Presentation.Seeding.Identity;
using Presentation.Seeding.PromoCode;

var corsPolicyName = "AllowAll";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication().AddInfrastructure(builder.Configuration);

builder.Services.AddCors(opt =>
{
    opt.AddPolicy(
        name: corsPolicyName,
        policyBuilder =>
        {
            policyBuilder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        }
    );
});

builder
    .Services.AddControllers()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.ReferenceHandler = System
            .Text
            .Json
            .Serialization
            .ReferenceHandler
            .IgnoreCycles;
    });

builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    c.IncludeXmlComments(xmlPath);
    c.AddSecurityDefinition(
        "Bearer",
        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme.",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
        }
    );

    c.AddSecurityRequirement(
        new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer",
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header,
                },
                new string[] {}
            },
        }
    );
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseCors(corsPolicyName);

//if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (args.Length > 0 && (args[0] == "seedRoles" || args[0] == "seed"))
{
    var scope = app.Services.CreateScope();
    using var context = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await SeedRoles.SeedAsync(context);
}


if (args.Length > 0 && (args[0] == "seedAdmin" || args[0] == "seed"))
{
    var scope = app.Services.CreateScope();
    using var context = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    await SeedAdmin.SeedAsync(context);
}


if (args.Length > 0 && (args[0] == "seedAdmin" || args[0] == "seed"))
    using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await SeedPromoCode.SeedAsync(dbContext);
}
if (args.Length > 0 && (args[0] == "seedAdmin" || args[0] == "seed"))
    using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await SeedCategories.SeedAsync(dbContext);
    await SeedSubCategories.SeedAsync(dbContext);
    await SeedIngredient.SeedAsync(dbContext);
    await SeedMeals.SeedAsync(dbContext);
}

if (args.Length > 0 && (args[0] == "seedPlans" || args[0] == "seed"))
{
    var scope = app.Services.CreateScope();
    using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await SeedPlan.SeedAsync(context);
}

if (args.Length > 0 && (args[0] == "seedAll" || args[0] == "seed"))
{
    Environment.Exit(0);
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapControllers();

app.MapGet("/Health", () => "I'm alive");

app.Run();
