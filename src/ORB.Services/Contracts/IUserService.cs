// <copyright file="IUserService.cs" company="ORB">
// Copyright (c) ORB. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Identity;
using ORB.Shared.Models.Auth.User;

namespace ORB.Services.Contracts;

/// <summary>
/// Interface of the user service.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Gets a user by id.
    /// </summary>
    /// <param name="id">Id of the user.</param>
    /// <returns>User as UserVM.</returns>
    Task<UserVM?> GetUserByIdAsync(string id);

    /// <summary>
    /// Gets a user's email by id.
    /// </summary>
    /// <param name="id">Id of the user.</param>
    /// <returns>Email of the user.</returns>
    Task<string> GetUserEmailByIdAsync(string id);

    /// <summary>
    /// Validates the user one time token.
    /// </summary>
    /// <param name="userId">Id of the user.</param>
    /// <param name="token">Token.</param>
    /// <param name="type">Type.</param>
    /// <param name="purpose">Purpose of the token.</param>
    /// <returns>Is the token valid.</returns>
    Task<bool> ValidateOneTimeTokenForUserAsync(string userId, string token, string type, string purpose);

    /// <summary>
    /// Change a user password.
    /// </summary>
    /// <param name="userId">Id of the user.</param>
    /// <param name="newPassword">New password.</param>
    /// <returns>IdentityResult.</returns>
    Task<IdentityResult> ChangePasswordAsync(string userId, string newPassword);

    /// <summary>
    /// Updates the user's info.
    /// </summary>
    /// <param name="userEmail">Email of the user.</param>
    /// <param name="newUserInfo">New info of the user.</param>
    /// <returns>Was it successful.</returns>
    Task<bool> UpdateUserAsync(string userEmail, UserUM newUserInfo);

    /// <summary>
    /// Generates one time token for user.
    /// </summary>
    /// <param name="email">Email of the user.</param>
    /// <param name="type">Type of the token.</param>
    /// <param name="purpose">Purpose of the token.</param>
    /// <returns>The token.</returns>
    Task<string> GenerateOneTimeTokenForUserAsync(string email, string type, string purpose);

    /// <summary>
    /// Gets user by email.
    /// </summary>
    /// <param name="email">Email.</param>
    /// <returns>The user.</returns>
    Task<UserVM> GetUserByEmailAsync(string email);
}