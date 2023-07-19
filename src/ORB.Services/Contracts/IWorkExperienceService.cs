using ORB.Shared.Models.Education;
using ORB.Shared.Models.PersonalInfo;
using ORB.Shared.Models.WorkExperience;

namespace ORB.Services.Contracts;

/// <summary>
/// Interface for work experience service.
/// </summary>
public interface IWorkExperienceService
{
    /// <summary>
    /// Creates and saves a new work experience item.
    /// </summary>
    /// <param name="workModel">The work experience input object.</param>
    /// <returns>The newly created WorkExperience object.</returns>
    Task<WorkExperienceVM> CreateWorkExperienceAsync(WorkExperienceIM workModel);

    /// <summary>
    /// Deletes a work experience item by ID.
    /// </summary>
    /// <param name="id">The ID of the work experience item to be deleted.</param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    Task DeleteWorkExperienceInfoByIdAsync(string id);

    /// <summary>
    /// Retrieves all work experience items for a given resume ID.
    /// </summary>
    /// <param name="id">The ID of the resume for which to retrieve the work experience items.</param>
    /// <returns>A list of WorkExperience objects.</returns>
    Task<List<WorkExperienceVM>> GetAllWorkExperienceForResumeWithIdAsync(string id);

    /// <summary>
    /// Retrieves a work experience item by ID and user ID.
    /// </summary>
    /// <param name="id">The ID of the work experience item to be retrieved.</param>
    /// <param name="userId">The user ID associated with the item.</param>
    /// <returns>The retrieved WorkExperience object.</returns>
    Task<WorkExperienceVM?> GetWorkExperienceByIdAsync(string id, string userId);

    /// <summary>
    /// Updates a work experience item by ID.
    /// </summary>
    /// <param name="id">The ID of the work experience item to be updated.</param>
    /// <param name="workModel">The updated work experience input object.</param>
    /// <returns>The updated WorkExperience object.</returns>
    Task<WorkExperienceVM> UpdateWorkExperienceInfoAsync(string id, WorkExperienceUM workModel);
}
