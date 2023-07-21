// <copyright file="WorkExperienceIM.cs" company="ORB">
// Copyright (c) ORB. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;
using ORB.Shared.DataAnnotations;

namespace ORB.Shared.Models.WorkExperience;

/// <summary>
/// Represents an input model for work experience for a resume.
/// </summary>
public class WorkExperienceIM
{
    /// <summary>
    /// Gets or sets the ID reference to the resume this work experience belongs to.
    /// </summary>
    [Required]
    public string ResumeId { get; set; } = string.Empty;

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
