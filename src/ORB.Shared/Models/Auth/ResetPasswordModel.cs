// <copyright file="ResetPasswordModel.cs" company="ORB">
// Copyright (c) ORB. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace ORB.Shared.Models.Auth;

/// <summary>
/// Model for resetting a password.
/// </summary>
public class ResetPasswordModel
{
    /// <summary>
    /// Gets or sets the password of the user.
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email address of the user.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the token for resetting a password.
    /// </summary>
    public string Token { get; set; } = string.Empty;
}
