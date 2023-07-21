// <copyright file="WorkExperience.cs" company="ORB">
// Copyright (c) ORB. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORB.Data.Models.Resumes;

/// <summary>
/// Represents work experience for a resume.
/// </summary>
public class WorkExperience
{
    /// <summary>
    /// Gets or sets the unique identifier for the work experience.
    /// </summary>
    [Required]
    public string Id { get; set; } = Guid.NewGuid().ToString();

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
    public DateOnly StartDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

    /// <summary>
    /// Gets or sets the end date of the work experience (if any).
    /// </summary>
    public DateOnly? EndDate { get; set; }

    /// <summary>
    /// Gets or sets the related resume to this work experience.
    /// </summary>
    [ForeignKey(nameof(ResumeId))]
    public Resume Resume { get; set; }
}
