// <copyright file="WorkExperienceUM.cs" company="ORB">
// Copyright (c) ORB. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;
using ORB.Shared.DataAnnotations;

namespace ORB.Shared.Models.WorkExperience;

/// <summary>
/// Represents update model for work experience for a resume.
/// </summary>
public class WorkExperienceUM
{
    /// <summary>
    /// Gets or sets the name of the company.
    /// </summary>
    [Required]
    public string CompanyName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the position held at the company.
    /// </summary>
    [Required]
    public string Position { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a description of the work experience.
    /// </summary>
    [Required]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the start date of the work experience.
    /// </summary>
    [Required]
    [DateOnly]
    public string StartDate { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the end date of the work experience (if any).
    /// </summary>
    [DateOnly]
    [GreaterThan(nameof(StartDate))]
    public string? EndDate { get; set; }
}
