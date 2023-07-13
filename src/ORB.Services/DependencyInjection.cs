// <copyright file="DependencyInjection.cs" company="ORB">
// Copyright (c) ORB. All rights reserved.
// </copyright>

using Microsoft.Extensions.DependencyInjection;
using ORB.Services.Contracts;
using ORB.Services.Implementations;

public static class DependencyInjection
{
    /// <summary>
    /// Add Services.
    /// </summary>
    /// <param name="services">Services.</param>
    public static void AddServices(this IServiceCollection services)
    {
        services
            .AddScoped<ICurrentUser, CurrentUser>()
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<ITokenService, TokenService>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<IEmailService, EmailService>()
            .AddScoped<ITemplateService, TemplateService>();
    }
}
