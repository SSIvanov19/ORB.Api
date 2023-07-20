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
    private readonly ILogger<WorkExperienceController> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="WorkExperienceController"/> class.
    /// </summary>
    /// <param name="logger">Logger.</param>
    /// <param name="currentUser">Current user.</param>
    /// <param name="resumeService">Resume service.</param>
    /// <param name="workExperienceService">Work experience service.</param>
    public WorkExperienceController(
        ICurrentUser currentUser,
        IResumeService resumeService,
        IWorkExperienceService workExperienceService,
        ILogger<WorkExperienceController> logger)
    {
        this.currentUser = currentUser;
        this.resumeService = resumeService;
        this.workExperienceService = workExperienceService;
        this.logger = logger;
    }

    /// <summary>
    /// Creates a new work experience.
    /// </summary>
    /// <param name="workExperienceModel">The model containing the information for the work experience.</param>
    /// <returns>The newly created work experience.</returns>
    [HttpPost]
    public async Task<ActionResult<WorkExperienceVM>> CreateWorkExperienceAsync(WorkExperienceIM workExperienceModel)
    {
        this.logger.LogInformation($"CreateWorkExperienceAsync method in the WorkExperienceController class : User with id: {this.currentUser.UserId} is trying to create work experience with resume id: {workExperienceModel.ResumeId}");
        var resume = await this.resumeService.GetResumeByIdAsync(workExperienceModel.ResumeId);

        if (resume is null)
        {
            this.logger.LogWarning($"CreateWorkExperienceAsync method in the WorkExperienceController class : There isn't a resume with this id: {workExperienceModel.ResumeId}");
            return this.NotFound("There isn't a resume with this id!");
        }

        if (resume.UserId != this.currentUser.UserId)
        {
            this.logger.LogWarning($"CreateWorkExperienceAsync method in the WorkExperienceController class : User with id: {this.currentUser.UserId} doesn't have access to this resume with id: {workExperienceModel.ResumeId}");
            return this.Forbid("User doesn't have access to this resume!");
        }

        if (resume.IsDeleted)
        {
            this.logger.LogWarning($"CreateWorkExperienceAsync method in the WorkExperienceController class : User with id: {this.currentUser.UserId} is trying to add work experience to a deleted resume with id: {workExperienceModel.ResumeId}");
            return this.BadRequest("This resume is deleted and you can't add work experience to it!");
        }

        var workExperience = await this.workExperienceService.CreateWorkExperienceAsync(workExperienceModel);

        this.logger.LogInformation($"CreateWorkExperienceAsync method in the WorkExperienceController class : User with id: {this.currentUser.UserId} successfully created work experience with id: {workExperience.Id} for resume with id: {workExperienceModel.ResumeId}");
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
        this.logger.LogInformation($"GetWorkExperienceByIdAsync method in the WorkExperienceController class : User with id: {this.currentUser.UserId} is trying to get work experience with id: {id}");
        var workExperience = await this.workExperienceService.GetWorkExperienceByIdAsync(id, this.currentUser.UserId);

        if (workExperience is null)
        {
            this.logger.LogWarning($"GetWorkExperienceByIdAsync method in the WorkExperienceController class : There isn't work experience with id: {id}");
            return this.NotFound();
        }

        this.logger.LogInformation($"GetWorkExperienceByIdAsync method in the WorkExperienceController class : User with id: {this.currentUser.UserId} successfully got work experience with id: {id}");
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
        this.logger.LogInformation($"UpdateWorkExperienceInfoByIdAsync method in the WorkExperienceController class : User with id: {this.currentUser.UserId} is trying to update work experience with id: {id}");
        var workExperience = await this.workExperienceService.GetWorkExperienceByIdAsync(id, this.currentUser.UserId);

        if (workExperience is null)
        {
            this.logger.LogWarning($"UpdateWorkExperienceInfoByIdAsync method in the WorkExperienceController class : There isn't work experience with id: {id}");
            return this.NotFound();
        }

        workExperience = await this.workExperienceService.UpdateWorkExperienceInfoAsync(id, workExperienceModel);

        this.logger.LogInformation($"UpdateWorkExperienceInfoByIdAsync method in the WorkExperienceController class : User with id: {this.currentUser.UserId} successfully updated work experience with id: {id}");
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
        this.logger.LogInformation($"Deleting work experience with id: {id}");

        var workExperience = await this.workExperienceService.GetWorkExperienceByIdAsync(id, this.currentUser.UserId);

        if (workExperience is null)
        {
            this.logger.LogWarning($"Work experience with id {id} doesn't exist!");

            return this.NotFound();
        }

        await this.workExperienceService.DeleteWorkExperienceInfoByIdAsync(id);

        this.logger.LogInformation($"Work experience with id: {id} has been deleted successfully!");

        return this.Ok();
    }
}