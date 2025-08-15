using System.Reflection;
using Application;
using Domain.Models.Identity;
using Infrastructure;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Presentation.Seeding.Foods;
using Presentation.Seeding.Identity;

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
                []
            },
        }
    );
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseCors(corsPolicyName);

//if (app.Environment.IsDevelopment())
//{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
//}

//<<<<<<< HEAD
if (args.Length > 0 && args[0] == "seedRoles")
//=======
if (args.Length > 0 && (args[0] == "seedRoles" || args[0] == "seed"))
//>>>>>>> cba8637334410772053f5b81c31b23463032c794
{
    var scope = app.Services.CreateScope();
    using var context = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await SeedRoles.SeedAsync(context);
}

//<<<<<<< HEAD
if (args.Length > 0 && args[0] == "seedAdmin")
//=======
if (args.Length > 0 && (args[0] == "seedAdmin" || args[0] == "seed"))
//>>>>>>> cba8637334410772053f5b81c31b23463032c794
{
    var scope = app.Services.CreateScope();
    using var context = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    await SeedAdmin.SeedAsync(context);
}

//<<<<<<< HEAD
if (args.Length > 0 && args[0] == "seedItems-ingredients")
//=======
if (args.Length > 0 && (args[0] == "seedItems-ingredients" || args[0] == "seed"))
//>>>>>>> cba8637334410772053f5b81c31b23463032c794
{
    var scope = app.Services.CreateScope();
    using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    
    await SeedIngredient.SeedAsync(context);
    await SeedItem.SeedAsync(context);
    await SeedItemIngredient.SeedAsync(context);
    
    Console.WriteLine("Seeding completed successfully!");
}

//<<<<<<< HEAD
//if (args.Length > 0 && args[0] == "seedPlans")
//{
//    var scope = app.Services.CreateScope();
//    using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
//    await SeedPlan.SeedAsync(context);
//}
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await SeedPlan.SeedAsync(context);
}
//=======
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
//>>>>>>> cba8637334410772053f5b81c31b23463032c794
app.UseHttpsRedirection();
app.MapControllers();

app.MapGet("/Health", () => "I'm alive");

app.Run();
