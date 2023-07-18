// <copyright file="WorkExperienceController.cs" company="ORB">
// Copyright (c) ORB. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ORB.Services.Contracts;
using ORB.Shared.Models.WorkExperience;

namespace ORB.WebHost.Controllers;

/// <summary>
/// Controller for work experience API end-points.
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class WorkExperienceController : ControllerBase
{
    private readonly ICurrentUser currentUser;
    private readonly IResumeService resumeService;
    private readonly IWorkExperienceService workExperienceService;

    /// <summary>
    /// Initializes a new instance of the <see cref="WorkExperienceController"/> class.
    /// </summary>
    /// <param name="currentUser">Current user.</param>
    /// <param name="resumeService">Resume service.</param>
    /// <param name="workExperienceService">Work experience service.</param>
    public WorkExperienceController(
        ICurrentUser currentUser,
        IResumeService resumeService,
        IWorkExperienceService workExperienceService)
    {
        this.currentUser = currentUser;
        this.resumeService = resumeService;
        this.workExperienceService = workExperienceService;
    }

    /// <summary>
    /// Creates a new work experience.
    /// </summary>
    /// <param name="workExperienceModel">The model containing the information for the work experience.</param>
    /// <returns>The newly created work experience.</returns>
    [HttpPost]
    public async Task<ActionResult<WorkExperienceVM>> CreateWorkExperienceAsync(WorkExperienceIM workExperienceModel)
    {
        var resume = await this.resumeService.GetResumeByIdAsync(workExperienceModel.ResumeId);

        if (resume is null)
        {
            return this.NotFound("There isn't a resume with this id!");
        }

        if (resume.UserId != this.currentUser.UserId)
        {
            return this.Forbid("User doesn't have access to this resume!");
        }

        if (resume.IsDeleted)
        {
            return this.BadRequest("This resume is deleted and you can't add work experience to it!");
        }

        var workExperience = await this.workExperienceService.CreateWorkExperienceAsync(workExperienceModel);

        return this.Ok(workExperience);
    }

    /// <summary>
    /// Gets work experience information by id.
    /// </summary>
    /// <param name="id">Id of the work experience.</param>
    /// <returns>Work experience as view model.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<WorkExperienceVM>> GetWorkExperienceByIdAsync(string id)
    {
        var workExperience = await this.workExperienceService.GetWorkExperienceByIdAsync(id, this.currentUser.UserId);

        if (workExperience is null)
        {
            return this.NotFound();
        }

        return this.Ok(workExperience);
    }

    /// <summary>
    /// Updates work experience information by id.
    /// </summary>
    /// <param name="id">The id of the work experience to update.</param>
    /// <param name="workExperienceModel">The updated work experience model with the new information.</param>
    /// <returns>The updated work experience.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<WorkExperienceVM>> UpdateWorkExperienceInfoByIdAsync(string id, WorkExperienceUM workExperienceModel)
    {
        var workExperience = await this.workExperienceService.GetWorkExperienceByIdAsync(id, this.currentUser.UserId);

        if (workExperience is null)
        {
            return this.NotFound();
        }

        workExperience = await this.workExperienceService.UpdateWorkExperienceInfoAsync(id, workExperienceModel);

        return this.Ok(workExperience);
    }

    /// <summary>
    /// Deletes work experience information by id.
    /// </summary>
    /// <param name="id">The id of the work experience to delete.</param>
    /// <returns>The status code of the operation.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWorkExperienceInfoByIdAsync(string id)
    {
        var workExperience = await this.workExperienceService.GetWorkExperienceByIdAsync(id, this.currentUser.UserId);

        if (workExperience is null)
        {
            return this.NotFound();
        }

        await this.workExperienceService.DeleteWorkExperienceInfoByIdAsync(id);

        return this.Ok();
    }
}