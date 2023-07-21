// <copyright file="Education.cs" company="ORB">
// Copyright (c) ORB. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ORB.Data.Models.Resumes;

/// <summary>
/// Represents education of a person in a resume.
/// </summary>
public class Education
{
    /// <summary>
    /// Gets or sets identifier of education.
    /// </summary>
    [Required]
    public string Id { get; set; } = Guid.NewGuid().ToString();

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
    [Required]
    public DateOnly StartDate { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

    /// <summary>
    /// Gets or sets the date education ended.
    /// </summary>
    public DateOnly? EndDate { get; set; }

    /// <summary>
    /// Gets or sets resume object this education is linked to.
    /// </summary>
    [ForeignKey(nameof(ResumeId))]
    public Resume Resume { get; set; }
}
