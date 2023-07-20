// <copyright file="TemplateIM.cs" company="ORB">
// Copyright (c) ORB. All rights reserved.
// </copyright>

namespace ORB.Shared.Models.Templates;

/// <summary>
/// A view model for a template.
/// </summary>
public class TemplateIM
{
    /// <summary>
    /// Gets or sets the name of the template.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the content of the template.
    /// </summary>
    public string Content { get; set; } = string.Empty;
}