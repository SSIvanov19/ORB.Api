// <copyright file="IResumeService.cs" company="ORB">
// Copyright (c) ORB. All rights reserved.
// </copyright>

using ORB.Shared.Models.Resume;

namespace ORB.Services.Contracts;

/// <summary>
/// Interface for managing resumes.
/// </summary>
public interface IResumeService
{
    /// <summary>
    /// Creates a new resume.
    /// </summary>
    /// <param name="resume">The resume's data.</param>
    /// <param name="personaInfoId">The ID of the persona's information.</param>
    /// <param name="userId">The ID of the user creating the resume.</param>
    /// <returns>The newly created resume.</returns>
    Task<ResumeVM> CreateResumeAsync(ResumeIM resume, string personaInfoId, string userId);

    /// <summary>
    /// Soft deletes a resume.
    /// </summary>
    /// <param name="id">Id of the resume to be deleted.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous Soft Delete operation.</returns>
    Task DeleteResumeAsync(string id);

    /// <summary>
    /// Recovers a soft deleted resume.
    /// </summary>
    /// <param name="id">Id of the resume to be recovered.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous Soft Delete operation.</returns>
    Task RecoverResumeAsync(string id);

    /// <summary>
    /// Retrieves all resumes belonging to the user with the given ID.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <returns>A list of all resumes belonging to the user with the given ID.</returns>
    Task<List<ResumeVM>> GetAllResumesForUserWithIdAsync(string userId);

    /// <summary>
    /// Retrieves all soft deleted resumes belonging to the user with the given ID.
    /// </summary>
    /// <param name="userId">Id of the user.</param>
    /// <returns>A list of all deleted resume belonging to the user with the given ID.</returns>
    Task<List<ResumeVM>> GetAllDeletedResumesForUserWithIdAsync(string userId);

    /// <summary>
    /// Gets a resume by its ID.
    /// </summary>
    /// <param name="id">The resume's ID.</param>
    /// <returns>The resume with the given ID, or null if it doesn't exist.</returns>
    Task<ResumeVM?> GetResumeByIdAsync(string id);

    /// <summary>
    /// Updates the resume with the given ID with the new resume information.
    /// </summary>
    /// <param name="id">The ID of the resume to update.</param>
    /// <param name="newResumeInfo">The updated resume information.</param>
    /// <returns>The updated resume.</returns>
    Task<ResumeVM> UpdateResumeInfoWithIdAsync(string id, ResumeIM newResumeInfo);

    /// <summary>
    /// Creates a PDF file for the given resume.
    /// </summary>
    /// <param name="resume">The resume as view model.</param>
    /// <returns>A memory stream containing the PDF file.</returns>
    Task<MemoryStream> CreatePDFForResumeAsync(ResumeVM resume);
}
