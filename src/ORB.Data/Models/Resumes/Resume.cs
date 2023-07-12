// <copyright file="Resume.cs" company="ORB">
// Copyright (c) ORB. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ORB.Data.Models.Auth;

namespace ORB.Data.Models.Resumes;

/// <summary>
/// Represents the Resume model.
/// </summary>
public class Resume
{
    /// <summary>
    /// Gets or sets the Id of the Resume.
    /// </summary>
    [Required]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Gets or sets the title of the Resume.
    /// </summary>
    [Required]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the creation time of the Resume.
    /// </summary>
    [Required]
    public DateTime CreationTime { get; set; } = DateTime.Now;

    /// <summary>
    /// Gets or sets the last modified time of the Resume.
    /// </summary>
    [Required]
    public DateTime LastModified { get; set; } = DateTime.Now;

    /// <summary>
    /// Gets or sets the UserId of the User associated with the Resume.
    /// </summary>
    [Required]
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the PersonalInfoId of the PersonalInfo associated with the Resume.
    /// </summary>
    public string? PersonalInfoId { get; set; }

    /// <summary>
    /// Gets or sets the TemplateId of the Template associated with the Resume.
    /// </summary>
    public string? TemplateId { get; set; }

    /// <summary>
    /// Gets or sets the User model associated with the Resume.
    /// </summary>
    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = new ();

    /// <summary>
    /// Gets or sets the PersonalInfo model associated with the Resume.
    /// </summary>
    [ForeignKey(nameof(PersonalInfoId))]
    public PersonalInfo? PersonalInfo { get; set; }

    /// <summary>
    /// Gets or sets the Template model associated with the Resume.
    /// </summary>
    [ForeignKey(nameof(TemplateId))]
    public Template? Template { get; set; }

    /// <summary>
    /// Gets or sets the list of Education models associated with the Resume.
    /// </summary>
    public ICollection<Education>? Educations { get; set; }

    /// <summary>
    /// Gets or sets the list of WorkExperience models associated with the Resume.
    /// </summary>
    public ICollection<WorkExperience>? WorkExperience { get; set; }
}
