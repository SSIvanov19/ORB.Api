// <copyright file="IAuthService.cs" company="ORB">
// Copyright (c) ORB. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Identity;
using ORB.Shared.Models.Auth.User;

namespace ORB.Services.Contracts;

/// <summary>
/// Interface for authentication service.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Checks if user exists in the Database.
    /// </summary>
    /// <param name="email">Email of the user.</param>
    /// <returns>Does user exists.</returns>
    Task<bool> CheckIfUserExistsAsync(string email);

    /// <summary>
    /// Checks if had verified his email.
    /// </summary>
    /// <param name="email">Email of the user.</param>
    /// <returns>Is email verified.</returns>
    Task<bool> CheckIfUserHasVerifiedEmailAsync(string email);

    /// <summary>
    /// Checks if user's provided password is correct.
    /// </summary>
    /// <param name="email">Email of the user.</param>
    /// <param name="password">Password of the user.</param>
    /// <returns>Is password correct.</returns>
    Task<bool> CheckIsPasswordCorrectAsync(string email, string password);

    /// <summary>
    /// Saves user to the database.
    /// </summary>
    /// <param name="userIM">User info.</param>
    /// <returns>Is creating successful.</returns>
    Task<Tuple<bool, string?>> CreateUserAsync(UserIM userIM);

    /// <summary>
    /// Confirms the email of the user.
    /// </summary>
    /// <param name="email">Email of the user.</param>
    /// <param name="token">Token.</param>
    /// <returns>IdentityResult.</returns>
    Task<IdentityResult> ConfirmEmailAsyncAsync(string email, string token);

    /// <summary>
    /// Resets the password of the user.
    /// </summary>
    /// <param name="email">Email of the user.</param>
    /// <param name="token">Token for the password resetting.</param>
    /// <param name="password">New password for the user.</param>
    /// <returns>IdentityResult.</returns>
    Task<IdentityResult> ResetPasswordAsync(string email, string token, string password);
}