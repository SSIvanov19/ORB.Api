// <copyright file="Program.cs" company="ORB">
// Copyright (c) ORB. All rights reserved.
// </copyright>

using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ORB.Data.Data;
using ORB.Data.Models.Auth;
using ORB.Services;
using ORB.WebHost.Helpers;
using ORB.WebHost.Models;
using ORB.WebHost.SwaggerConfiguration;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Entity Framework
builder.Services.AddDbContext<ApplicationDbContext>(options =>
   options.UseSqlServer(configuration.GetConnectionString("DefaultConnection") !, o =>
   {
       o.MigrationsAssembly(typeof(Program).Assembly.FullName);
       o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
       o.UseDateOnlyTimeOnly();
   }));

// For Identity
builder.Services
    .AddIdentity<User, IdentityRole>(options =>
    {
        /*options.SignIn.RequireConfirmedEmail = true;*/
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services
    .AddLogging(conf =>
    {
        conf.ClearProviders();

        conf.AddSeq(configuration.GetSection("Seq"));
        conf.AddConsole();
    });

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"] !)),
        };
    });

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
        builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddServices();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(configuration["Syncfusion:LicenseKey"]);

await app.InitAppAsync();
app.UseSwagger();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
}

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Logger.LogInformation("Starting the app.");

app.Run();