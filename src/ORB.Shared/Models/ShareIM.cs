// <copyright file="ShareIM.cs" company="ORB">
// Copyright (c) ORB. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace ORB.Shared.Models;

/// <summary>
/// A class for sharing resumes.
/// </summary>
public class ShareIM
{
    /// <summary>
    /// Gets or sets the email of the person to share with.
    /// </summary>
    [EmailAddress]
    [Required]
    public string Email { get; set; } = string.Empty;
}
