// <copyright file="AuthService.cs" company="ORB">
// Copyright (c) ORB. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Identity;
using ORB.Data.Models.Auth;
using ORB.Services.Contracts;
using ORB.Shared.Models.Auth.User;

namespace ORB.Services.Implementations;

/// <summary>
/// Class that implements <see cref="IAuthService"/>.
/// </summary>
internal class AuthService : IAuthService
{
    private readonly UserManager<User> userManager;
    private readonly SignInManager<User> signInManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthService"/> class.
    /// </summary>
    /// <param name="userManager">User manager.</param>
    /// <param name="signInManager">SignIn Manager.</param>
    /// <param name="roleManager">Role manager.</param>
    public AuthService(
        UserManager<User> userManager,
        SignInManager<User> signInManager)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
    }

    /// <inheritdoc/>
    public async Task<bool> CheckIfUserExistsAsync(string email)
    {
        return await this.userManager.FindByEmailAsync(email) != null;
    }

    /// <inheritdoc/>
    public async Task<bool> CheckIsPasswordCorrectAsync(string email, string password)
    {
        var user = await this.userManager.FindByEmailAsync(email);

        return !(user is null || !await this.userManager.CheckPasswordAsync(user, password));
    }

    /// <inheritdoc/>
    public async Task<Tuple<bool, string?>> CreateUserAsync(UserIM userIM)
    {
        User user = new ()
        {
            Email = userIM.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = userIM.Email,
            FirstName = userIM.FirstName,
            LastName = userIM.LastName,
        };

        var result = await this.userManager.CreateAsync(user, userIM.Password);

        if (!result.Succeeded)
        {
            return new (false, result.Errors.FirstOrDefault()?.Description);
        }

        return new (true, null);
    }

    /// <inheritdoc/>
    public async Task<IdentityResult> ConfirmEmailAsyncAsync(string email, string token)
    {
        var user = await this.userManager.FindByEmailAsync(email);

        return await this.userManager.ConfirmEmailAsync(user, token);
    }

    /// <inheritdoc/>
    public async Task<IdentityResult> ResetPasswordAsync(string email, string token, string password)
    {
        var user = await this.userManager.FindByEmailAsync(email);

        return await this.userManager.ResetPasswordAsync(user, token, password);
    }

    /// <inheritdoc/>
    public async Task<bool> CheckIfUserHasVerifiedEmailAsync(string email)
    {
        var user = await this.userManager.FindByEmailAsync(email);

        return !(user is null || !await this.signInManager.CanSignInAsync(user));
    }
}
