// <copyright file="SwaggerConfiguration.cs" company="ORB">
// Copyright (c) ORB. All rights reserved.
// </copyright>

using System.Reflection;
using Microsoft.OpenApi.Models;

namespace ORB.WebHost.SwaggerConfiguration;

/// <summary>
/// Swagger configuration.
/// </summary>
public static class SwaggerConfiguration
{
    /// <summary>
    /// Adds swagger to service.
    /// </summary>
    /// <param name="services">Service.</param>
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"JWT Authorization header using the Bearer scheme. 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      Example: 'Bearer 12345abcdef'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
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
                new List<string>()
                },
            });

            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Online Resume Builder API",
                Description = "An API for Online Resume Builder",
            });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
    }
}
