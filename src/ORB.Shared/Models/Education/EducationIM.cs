// <copyright file="EducationIM.cs" company="ORB">
// Copyright (c) ORB. All rights reserved.
// </copyright>

using ORB.Shared.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace ORB.Shared.Models.Education;

public class EducationIM
{
    /// <summary>
    /// Gets or sets identifier of resume.
    /// </summary>
    [Required]
    public string ResumeId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets name of the school/university.
    /// </summary>
    [Required]
    public string SchoolName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets degree obtained.
    /// </summary>
    [Required]
    public string Degree { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets field of study of the degree program.
    /// </summary>
    [Required]
    public string FieldOfStudy { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets description of the education program.
    /// </summary>
    [Required]
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date education started.
    /// </summary>
    [DateOnly]
    [Required]
    public string StartDate { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date education ended.
    /// </summary>
    [DateOnly]
    [GreaterThan(nameof(StartDate))]
    public string? EndDate { get; set; }
}