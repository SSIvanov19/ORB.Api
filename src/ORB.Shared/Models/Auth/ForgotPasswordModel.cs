// <copyright file="ForgotPasswordModel.cs" company="ORB">
// Copyright (c) ORB. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace ORB.Shared.Models.Auth;

/// <summary>
/// Model for forgot password.
/// </summary>
public class ForgotPasswordModel
{
    /// <summary>
    ///   Gets or sets the email address of the user.
    /// </summary>
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
}
