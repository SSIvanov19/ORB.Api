// <copyright file="ITemplateService.cs" company="ORB">
// Copyright (c) ORB. All rights reserved.
// </copyright>

using ORB.Shared.Models.Templates;

namespace ORB.Services.Contracts;

/// <summary>
/// Interface for the Template Service
/// </summary>
public interface ITemplateService
{
    /// <summary>
    /// Returns all templates.
    /// </summary>
    /// <returns>Collection of TemplateVM</returns>
    Task<IEnumerable<TemplateVM>?> GetAllTemplatesAsync();

    /// <summary>
    /// Finds a template based on the provided ID.
    /// </summary>
    /// <param name="id">ID of template to find.</param>
    /// <returns>Template with provided ID.</returns>
    Task<TemplateVM?> FindTemplateByIdAsync(string id);

    /// <summary>
    /// Adds a new template if it does not already exist.
    /// </summary>
    /// <param name="template">The template to add.</param>
    /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
    public Task AddTemplateIfDoesNotExistAsync(TemplateIM template);
}
