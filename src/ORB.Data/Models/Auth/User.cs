// <copyright file="User.cs" company="ORB">
// Copyright (c) ORB. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ORB.Data.Models.Auth;

/// <summary>
/// User model.
/// </summary>
public class User : IdentityUser
{
    /// <summary>
    /// Gets or sets first name of the user.
    /// </summary>
    [Required]
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets last name of the user.
    /// </summary>
    [Required]
    public string LastName { get; set; } = string.Empty;
}