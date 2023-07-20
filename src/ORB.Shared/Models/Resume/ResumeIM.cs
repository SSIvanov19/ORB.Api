// <copyright file="ResumeIM.cs" company="ORB">
// Copyright (c) ORB. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace ORB.Shared.Models.Resume;

/// <summary>
/// Represents a Resume Input Model.
/// </summary>
public class ResumeIM
{
    /// <summary>
    /// Gets or sets the title of the Resume.
    /// </summary>
    [Required]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the TemplateId of the Template associated with the Resume.
    /// </summary>
    [Required]
    public string TemplateId { get; set; } = string.Empty;
}