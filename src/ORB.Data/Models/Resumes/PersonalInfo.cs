// <copyright file="PersonalInfo.cs" company="ORB">
// Copyright (c) ORB. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ORB.Data.Models.Resumes;

/// <summary>
/// Represents personal information for a resume.
/// </summary>
public class PersonalInfo
{
    /// <summary>
    /// Gets or sets the unique identifier of person information.
    /// </summary>
    [Required]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Gets or sets the full name of person.
    /// </summary>
    [Required]
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the address of person.
    /// </summary>
    [Required]
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the phone number of person.
    /// </summary>
    [Required]
    [Phone]
    public string PhoneNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email of person.
    /// </summary>
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the summary of person's skills and experience.
    /// </summary>
    [Required]
    public string Summary { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the URL of person's image.
    /// </summary>
    [AllowNull]
    public string? PersonImageURL { get; set; } = null;
}
