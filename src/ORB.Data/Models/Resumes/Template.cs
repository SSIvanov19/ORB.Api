// <copyright file="Templates.cs" company="ORB">
// Copyright (c) ORB. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace ORB.Data.Models.Resumes;

/// <summary>
/// Represents a resume template.
/// </summary>
public class Template
{
    /// <summary>
    /// Gets or sets the ID of the template.
    /// </summary>
    [Required]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Gets or sets the name of the template.
    /// </summary>
    [Required]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the content of the template.
    /// </summary>
    [Required]
    public string Content { get; set; } = string.Empty;
}
