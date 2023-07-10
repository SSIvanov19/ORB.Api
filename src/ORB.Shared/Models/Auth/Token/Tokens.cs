// <copyright file="Tokens.cs" company="ORB">
// Copyright (c) ORB. All rights reserved.
// </copyright>

using System.IdentityModel.Tokens.Jwt;

namespace ORB.Shared.Models.Auth.Token;

/// <summary>
/// Tokens for the users.
/// </summary>
public class Tokens
{
    /// <summary>
    /// Gets or sets access token.
    /// </summary>
    public JwtSecurityToken AccessToken { get; set; } = new ();

    /// <summary>
    /// Gets or sets refresh token.
    /// </summary>
    public JwtSecurityToken RefreshToken { get; set; } = new ();
}