// <copyright file="ResumesController.cs" company="ORB">
// Copyright (c) ORB. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ORB.Services.Contracts;
using ORB.Shared.Models.Resume;

namespace ORB.WebHost.Controllers;

/// <summary>
/// Controller for resumes API end-points.
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ResumesController : ControllerBase
{
    private readonly IPersonalInfoService personalInfoService;
    private readonly ITemplateService templateService;
    private readonly IResumeService resumeService;
    private readonly ICurrentUser currentUser;

    /// <summary>
    /// Initializes a new instance of the <see cref="ResumesController"/> class.
    /// </summary>
    /// <param name="personalInfoService">Personal info service.</param>
    /// <param name="templateService">Template service.</param>
    /// <param name="currentUser">Current user.</param>
    /// <param name="resumeService">Resume service.</param>
    public ResumesController(
        IPersonalInfoService personalInfoService,
        ICurrentUser currentUser,
        ITemplateService templateService,
        IResumeService resumeService)
    {
        this.personalInfoService = personalInfoService;
        this.currentUser = currentUser;
        this.templateService = templateService;
        this.resumeService = resumeService;
    }

    /// <summary>
    /// Endpoint for creating a new resume.
    /// </summary>
    /// <param name="resumeIM">Input model for the new resume.</param>
    /// <returns>The created resume.</returns>
    [HttpPost]
    public async Task<ActionResult<ResumeVM>> CreateResumeAsync([FromBody] ResumeIM resumeIM)
    {
        if (await this.templateService.FindTemplateByIdAsync(resumeIM.TemplateId) is null)
        {
            return this.BadRequest();
        }

        var personalInfo = await this.personalInfoService
                                        .CreateDefaultPersonalInfoForUserWithIdAsync(this.currentUser.UserId);

        var resume = await this.resumeService.CreateResumeAsync(resumeIM, personalInfo.Id, this.currentUser.UserId);

        return this.Ok(resume);
    }

    /// <summary>
    /// Endpoint for getting all resumes of the current user.
    /// </summary>
    /// <returns>List of resumes of the current user.</returns>
    [HttpGet]
    public async Task<ActionResult<List<ResumeVM>>> GetAllResumesForCurrentUserAsync()
    {
        var resumes = await this.resumeService.GetAllResumesForUserWithIdAsync(this.currentUser.UserId);

        return this.Ok(resumes);
    }

    /// <summary>
    /// Endpoint for getting a specific resume by its id.
    /// </summary>
    /// <param name="id">Id of the resume to retrieve.</param>
    /// <returns>The requested resume.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<ResumeVM>> GetResumeByIdAsync(string id)
    {
        var resume = await this.resumeService.GetResumeByIdAsync(id);

        if (resume is null)
        {
            return this.NotFound();
        }

        if (resume.UserId != this.currentUser.UserId)
        {
            return this.Forbid();
        }

        if (resume.IsDeleted)
        {
            return this.BadRequest();
        }

        return this.Ok(resume);
    }

    /// <summary>
    /// Update a resume by its id.
    /// </summary>
    /// <param name="id">Id of the resume to update.</param>
    /// <param name="newResumeInfo">Updated information of the resume.</param>
    /// <returns>The updated resume.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<ResumeVM>> UpdateResumeWithIdAsync(string id, [FromBody] ResumeIM newResumeInfo)
    {
        var resume = await this.resumeService.GetResumeByIdAsync(id);

        if (resume is null)
        {
            return this.NotFound();
        }

        if (resume.UserId != this.currentUser.UserId)
        {
            return this.Unauthorized();
        }

        if (resume.IsDeleted)
        {
            return this.BadRequest();
        }

        resume = await this.resumeService.UpdateResumeInfoWithIdAsync(id, newResumeInfo);

        return this.Ok(resume);
    }

    /// <summary>
    /// Soft delete a resume by its id.
    /// </summary>
    /// <param name="id">Id of the resume to be deleted.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous Soft Delete operation.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteResumeWithIdAsync(string id)
    {
        var resume = await this.resumeService.GetResumeByIdAsync(id);

        if (resume is null)
        {
            return this.NotFound();
        }

        if (resume.UserId != this.currentUser.UserId)
        {
            return this.Unauthorized();
        }

        if (resume.IsDeleted)
        {
            return this.BadRequest();
        }

        await this.resumeService.DeleteResumeAsync(id);

        return this.Ok();
    }

    /// <summary>
    /// Recover a deleted resume by its id.
    /// </summary>
    /// <param name="id">Id of the deleted resume to recover.</param>
    /// <returns>A <see cref="Task"/> that represents the asynchronous Recover operation.</returns>
    [HttpPost("recover/{id}")]
    public async Task<IActionResult> RecoverResumeWithIdAsync(string id)
    {
        var resume = await this.resumeService.GetResumeByIdAsync(id);

        if (resume is null)
        {
            return this.NotFound();
        }

        if (resume.UserId != this.currentUser.UserId)
        {
            return this.Unauthorized();
        }

        if (!resume.IsDeleted)
        {
            return this.BadRequest();
        }

        await this.resumeService.RecoverResumeAsync(id);

        return this.Ok();
    }

    /// <summary>
    /// Endpoint for getting all deleted resumes of the current user.
    /// </summary>
    /// <returns>List of deleted resumes of the current user.</returns>
    [HttpGet("deleted")]
    public async Task<ActionResult<List<ResumeVM>>> GetAllDeletedResumesForCurrentUserAsync()
    {
        var resumes = await this.resumeService.GetAllDeletedResumesForUserWithIdAsync(this.currentUser.UserId);

        return this.Ok(resumes);
    }
}
