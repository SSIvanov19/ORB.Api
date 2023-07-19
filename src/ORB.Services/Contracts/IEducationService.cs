// <copyright file="IEducationService.cs" company="ORB">
// Copyright (c) ORB. All rights reserved.
// </copyright>

using ORB.Shared.Models.Education;

namespace ORB.Services.Contracts;

/// <summary>
/// Interface for managing education information.
/// </summary>
public interface IEducationService
{
    /// <summary>
    /// Create a new education item to resume.
    /// </summary>
    /// <param name="educationModel">Model with all information needed to create a new education item.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation. The task result contains the created <see cref="EducationVM"/>.</returns>
    Task<EducationVM> CreateEducationInfoAsync(EducationIM educationModel);

    /// <summary>
    /// Delete an education item by its id.
    /// </summary>
    /// <param name="id">The id of the education item to be deleted.</param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    Task DeleteEducationInfoByIdAsync(string id);

    /// <summary>
    /// Gets all education items linked to a resume by ID.
    /// </summary>
    /// <param name="id">The ID of the resume.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation. The task result contains a list of <see cref="EducationVM"/>.</returns>
    Task<List<EducationVM>> GetAllEducationsForResumeWithIdAsync(string id);

    /// <summary>
    /// Get an education item by its id and user id.
    /// </summary>
    /// <param name="id">The id of the education item to be retrieved.</param>
    /// <param name="userId">The id of the user requesting the education item.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation. The task result contains the retrieved <see cref="EducationVM"/> or null if not found.</returns>
    Task<EducationVM?> GetEducationByIdAsync(string id, string userId);

    /// <summary>
    /// Update an existing education item.
    /// </summary>
    /// <param name="id">The id of the education item to be updated.</param>
    /// <param name="educationModel">Model with all information needed to update an education item.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation. The task result contains the updated <see cref="EducationVM"/>.</returns>
    Task<EducationVM> UpdateEducationInfoAsync(string id, EducationUM educationModel);
}
