// <copyright file="PersonalInfoController.cs" company="ORB">
// Copyright (c) ORB. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ORB.Services.Contracts;
using ORB.Shared.Models.PersonalInfo;
using ORB.Shared.Models.Resume;

namespace ORB.WebHost.Controllers;

/// <summary>
/// Controller for resumes API end-points.
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PersonalInfoController : ControllerBase
{
    private readonly IPersonalInfoService personalInfoService;
    private readonly ICurrentUser currentUser;

    /// <summary>
    /// Initializes a new instance of the <see cref="PersonalInfoController"/> class.
    /// </summary>
    /// <param name="personalInfoService">Personal info service.</param>
    /// <param name="currentUser">Current user.</param>
    public PersonalInfoController(
        IPersonalInfoService personalInfoService,
        ICurrentUser currentUser)
    {
        this.personalInfoService = personalInfoService;
        this.currentUser = currentUser;
    }

    /// <summary>
    /// Endpoint for getting a specific personal info by its id.
    /// </summary>
    /// <param name="id">Id of the personal info to retrieve.</param>
    /// <returns>The requested personal info.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<PersonalInfoVM>> GetPersonalInfoByIdAsync(string id)
    {
        var personalInfo = await this.personalInfoService.GetPersonalInfoByIdAsync(id, this.currentUser.UserId);

        if (personalInfo is null)
        {
            return this.NotFound();
        }

        return this.Ok(personalInfo);
    }

    /// <summary>
    /// Update a personal info by its id.
    /// </summary>
    /// <param name="id">Id of the personal info to update.</param>
    /// <param name="newPersonalInfo">Updated information of the personal info.</param>
    /// <returns>The updated resume.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<ResumeVM>> UpdateResumeWithIdAsync(string id, [FromForm] PersonalInfoIM newPersonalInfo)
    {
        var personalInfo = await this.personalInfoService.GetPersonalInfoByIdAsync(id, this.currentUser.UserId);

        if (personalInfo is null)
        {
            return this.NotFound();
        }

        personalInfo = await this.personalInfoService.UpdatePersonalInfoWithIdAsync(id, newPersonalInfo);

        return this.Ok(personalInfo);
    }
}
