// <copyright file="IPersonalInfoService.cs" company="ORB">
// Copyright (c) ORB. All rights reserved.
// </copyright>

using ORB.Shared.Models.PersonalInfo;
using ORB.Shared.Models.Resume;

namespace ORB.Services.Contracts;

/// <summary>
/// Interface for managing personal information.
/// </summary>
public interface IPersonalInfoService
{
    /// <summary>
    /// Creates a default personal information object for the specified user.
    /// </summary>
    /// <param name="id">The user's id.</param>
    /// <returns>A PersonalInfoVM object.</returns>
    Task<PersonalInfoVM> CreateDefaultPersonalInfoForUserWithIdAsync(string id);

    /// <summary>
    /// Retrieves the personal information of the specified user by id and user id.
    /// </summary>
    /// <param name="id">The id of the personal information object.</param>
    /// <param name="userId">The id of the user whose personal information is being retrieved.</param>
    /// <returns>The PersonalInfoVM object for the specified user, or null if not found.</returns>
    Task<PersonalInfoVM?> GetPersonalInfoByIdAsync(string id, string userId);

    /// <summary>
    /// Updates the personal information of the specified user by id and user id.
    /// </summary>
    /// <param name="id">The id of the personal information object.</param>
    /// <param name="newPersonalInfo">The new personal information of the user.</param>
    /// <returns>The updated PersonalInfoVM object for the specified user.</returns>
    Task<PersonalInfoVM> UpdatePersonalInfoWithIdAsync(string id, PersonalInfoIM newPersonalInfo);
}
