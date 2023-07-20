// <copyright file="ResumesController.cs" company="ORB">
// Copyright (c) ORB. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ORB.Services.Contracts;
using ORB.Shared.Models;
using ORB.Shared.Models.Education;
using ORB.Shared.Models.Resume;
using ORB.Shared.Models.WorkExperience;
using static ORB.Services.Contracts.IEmailService;

namespace ORB.WebHost.Controllers;

/// <summary>
/// Controller for resumes API end-points.
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class ResumesController : ControllerBase
{
    private readonly IWorkExperienceService workExperienceService;
    private readonly IPersonalInfoService personalInfoService;
    private readonly IEducationService educationService;
    private readonly ILogger<ResumesController> logger;
    private readonly ITemplateService templateService;
    private readonly IResumeService resumeService;
    private readonly IEmailService emailService;
    private readonly ICurrentUser currentUser;

    /// <summary>
    /// Initializes a new instance of the <see cref="ResumesController"/> class.
    /// </summary>
    /// <param name="workExperienceService">Work experience service.</param>
    /// <param name="personalInfoService">Personal info service.</param>
    /// <param name="educationService">Education service.</param>
    /// <param name="templateService">Template service.</param>
    /// <param name="resumeService">Resume service.</param>
    /// <param name="emailService">Email service.</param>
    /// <param name="currentUser">Current user.</param>
    /// <param name="logger">Logger.</param>
    public ResumesController(
        IWorkExperienceService workExperienceService,
        IPersonalInfoService personalInfoService,
        IEducationService educationService,
        ILogger<ResumesController> logger,
        ITemplateService templateService,
        IResumeService resumeService,
        IEmailService emailService,
        ICurrentUser currentUser)
    {
        this.workExperienceService = workExperienceService;
        this.personalInfoService = personalInfoService;
        this.educationService = educationService;
        this.templateService = templateService;
        this.resumeService = resumeService;
        this.emailService = emailService;
        this.currentUser = currentUser;
        this.logger = logger;
    }

    /// <summary>
    /// Endpoint for creating a new resume.
    /// </summary>
    /// <param name="resumeIM">Input model for the new resume.</param>
    /// <returns>The created resume.</returns>
    [HttpPost]
    public async Task<ActionResult<ResumeVM>> CreateResumeAsync([FromBody] ResumeIM resumeIM)
    {
        this.logger.LogInformation($"CreateResumeAsync method in ResumesController class : User with id: {this.currentUser.UserId} is trying to create a resume with name {resumeIM.Title}.");

        if (await this.templateService.FindTemplateByIdAsync(resumeIM.TemplateId) is null)
        {
            this.logger.LogInformation($"CreateResumeAsync method in ResumesController class : User with id: {this.currentUser.UserId} received a bad request due to an invalid templateId.");
            return this.BadRequest();
        }

        var personalInfo = await this.personalInfoService
                                        .CreateDefaultPersonalInfoForUserWithIdAsync(this.currentUser.UserId);

        var resume = await this.resumeService.CreateResumeAsync(resumeIM, personalInfo.Id, this.currentUser.UserId);

        this.logger.LogInformation($"CreateResumeAsync method in ResumesController class : User with id:{this.currentUser.UserId} successfully created a new resume with ID:{resume.Id}.");
        return this.Ok(resume);
    }

    /// <summary>
    /// Endpoint for getting all resumes of the current user.
    /// </summary>
    /// <returns>List of resumes of the current user.</returns>
    [HttpGet]
    public async Task<ActionResult<List<ResumeVM>>> GetAllResumesForCurrentUserAsync()
    {
        this.logger.LogInformation($"GetAllResumesForCurrentUserAsync method in ResumesController : Getting all resumes for user with ID {this.currentUser.UserId}");

        var resumes = await this.resumeService.GetAllResumesForUserWithIdAsync(this.currentUser.UserId);

        this.logger.LogInformation($"GetAllResumesForCurrentUserAsync method in ResumesController : Successfully retrieved {resumes.Count} resumes for user with ID {this.currentUser.UserId}");
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
        this.logger.LogInformation("GetResumeByIdAsync method called with resume id: {id}", id);
        var resume = await this.resumeService.GetResumeByIdAsync(id);

        if (resume is null)
        {
            this.logger.LogWarning("Resume with id: {id} was not found", id);
            return this.NotFound();
        }

        if (resume.UserId != this.currentUser.UserId)
        {
            this.logger.LogError("User with id: {uid} is unauthorized to view resume with id: {id}", this.currentUser.UserId, id);
            return this.Forbid();
        }

        if (resume.IsDeleted)
        {
            this.logger.LogWarning("Resume with id: {id} is deleted.", id);
            return this.BadRequest();
        }

        this.logger.LogInformation("Resume found with id: {id}", id);
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
        this.logger.LogInformation("UpdateResumeWithIdAsync method called with resume id: {id}", id);
        var resume = await this.resumeService.GetResumeByIdAsync(id);

        if (resume is null)
        {
            this.logger.LogWarning("Resume not found.");
            return this.NotFound();
        }

        if (resume.UserId != this.currentUser.UserId)
        {
            this.logger.LogWarning("Unauthorized user tried to update resume.");
            return this.Forbid();
        }

        if (resume.IsDeleted)
        {
            this.logger.LogWarning("Resume is marked as deleted.");
            return this.BadRequest();
        }

        resume = await this.resumeService.UpdateResumeInfoWithIdAsync(id, newResumeInfo);

        this.logger.LogInformation("Resume updated successfully.");

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
            this.logger.LogInformation("Resume not found with ID {id}", id);
            return this.NotFound();
        }

        if (resume.UserId != this.currentUser.UserId)
        {
            this.logger.LogInformation("The user {userId} is not authorized to delete the resume with ID {id}", this.currentUser.UserId, id);
            return this.Forbid();
        }

        if (resume.IsDeleted)
        {
            this.logger.LogInformation("Resume with ID {id} is already deleted", id);
            return this.BadRequest();
        }

        await this.resumeService.DeleteResumeAsync(id);
        this.logger.LogInformation("Resume with ID {id} has been deleted successfully", id);

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
        this.logger.LogInformation($"Attempting to recover resume with ID {id}");

        if (resume is null)
        {
            this.logger.LogWarning($"Resume with ID {id} not found");
            return this.NotFound();
        }

        if (resume.UserId != this.currentUser.UserId)
        {
            this.logger.LogWarning($"User with ID {this.currentUser.UserId} forbidden to recover resume with ID {id}");
            return this.Forbid();
        }

        if (!resume.IsDeleted)
        {
            this.logger.LogWarning($"Resume with ID {id} not marked as deleted");
            return this.BadRequest();
        }

        await this.resumeService.RecoverResumeAsync(id);
        this.logger.LogInformation($"Resume with ID {id} successfully recovered");

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

        logger.LogInformation($"Retrieving all deleted resumes for user with id: {this.currentUser.UserId}");
        return this.Ok(resumes);
    }

    /// <summary>
    /// Endpoint for downloading a resume as PDF.
    /// </summary>
    /// <param name="id">Id of the resume to be download.</param>
    /// <returns>The resume as PDF.</returns>
    [HttpGet("{id}/download")]
    public async Task<string?> DownloadResumeAsPDFAsync(string id)
    {
        var resume = await this.resumeService.GetResumeByIdAsync(id);

        if (resume is null)
        {
            logger.LogInformation($"Resume with ID:{id} was not found.");
            return null;
        }

        if (resume.UserId != this.currentUser.UserId)
        {
            logger.LogWarning($"Found user did not own resume with ID:{id}.");
            return null;
        }

        if (resume.IsDeleted)
        {
            logger.LogWarning($"Tried to download resume with ID:{id} that was deleted.");
            return null;
        }

        var fileMemoryStream = await this.resumeService.CreatePDFForResumeAsync(resume);

        var bytes = fileMemoryStream.ToArray();
        logger.LogInformation($"Resume with ID:{id} was successfully downloaded.");
        return Convert.ToBase64String(bytes);
    }

    /// <summary>
    /// Endpoints for getting all educations for a resume with a specific id.
    /// </summary>
    /// <param name="id">Id of the resume.</param>
    /// <returns>A list of educations for the resume.</returns>
    [HttpGet("{id}/educations")]
    public async Task<ActionResult<List<EducationVM>>> GetAllEducationsForResumeWithId(string id)
    {
        logger.LogInformation("Getting all education experiences for resume with id {0}", id);

        var resume = await this.resumeService.GetResumeByIdAsync(id);

        if (resume is null)
        {
            logger.LogWarning("Resume with id {0} not found", id);
            return this.NotFound();
        }

        if (resume.UserId != this.currentUser.UserId)
        {
            logger.LogWarning("Attempt to access resume with id {0} by unauthorized user", id);
            return this.Forbid();
        }

        if (resume.IsDeleted)
        {
            logger.LogWarning("Attempt to access deleted resume with id {0}", id);
            return this.BadRequest();
        }

        var educations = await this.educationService.GetAllEducationsForResumeWithIdAsync(id);
        logger.LogInformation("Found {0} education experiences for resume with id {1}", educations.Count(), id);

        return this.Ok(educations);
    }

    /// <summary>
    /// Endpoints for getting all work experiences for a resume with a specific id.
    /// </summary>
    /// <param name="id">Id of the resume.</param>
    /// <returns>A list of work experiences for the resume.</returns>
    [HttpGet("{id}/work")]
    public async Task<ActionResult<List<WorkExperienceVM>>> GetAllWorkExperienceForResumeWithId(string id)
    {
        this.logger.LogInformation($"Getting all work experiences for resume with id {id}.");
        var resume = await this.resumeService.GetResumeByIdAsync(id);

        if (resume is null)
        {
            this.logger.LogInformation($"Resume with id {id} not found.");
            return this.NotFound();
        }

        if (resume.UserId != this.currentUser.UserId)
        {
            this.logger.LogInformation($"Resume with id {id} belongs to user {resume.UserId} who is unauthorized.");
            return this.Forbid();
        }

        if (resume.IsDeleted)
        {
            this.logger.LogInformation($"Resume with id {id} is deleted.");
            return this.BadRequest();
        }

        var workExperiences = await this.workExperienceService.GetAllWorkExperienceForResumeWithIdAsync(id);

        this.logger.LogInformation($"Retrieved all work experiences for resume with id {id}.");

        return this.Ok(workExperiences);
    }

    /// <summary>
    /// Share the resume with specified ID by email with the specified share information.
    /// </summary>
    /// <param name="id">The ID of the resume to share.</param>
    /// <param name="share">The share information.</param>
    /// <returns>A <see cref="IActionResult"/> with the status response code.</returns>
    [HttpPost("{id}")]
    public async Task<IActionResult> ShareResumeByEmail(string id, [FromBody] ShareIM share)
    {
        var resume = await this.resumeService.GetResumeByIdAsync(id);

        if (resume is null)
        {
            this.logger.LogInformation($"No Resume found with id {id}");
            return this.NotFound();
        }

        if (resume.UserId != this.currentUser.UserId)
        {
            this.logger.LogWarning($"User with id {this.currentUser.UserId} is not authorized for sharing Resume with id {id}");
            return this.Forbid();
        }

        if (resume.IsDeleted)
        {
            this.logger.LogWarning($"Resume with id {id} is deleted");
            return this.BadRequest();
        }

        var fileMemoryStream = await this.resumeService.CreatePDFForResumeAsync(resume);

        fileMemoryStream.Seek(0, SeekOrigin.Begin);

        var emailRequest = new SendEmailRequest(share.Email, "Check out my resume", "Hi,\r\nCheck out my resume!", "resume.pdf", Convert.ToBase64String(fileMemoryStream.ToArray()));

        await this.emailService.SendEmailAsync(emailRequest);

        this.logger.LogInformation($"Resume with id {id} is shared with email {share.Email}");
        return this.Ok();
    }
}
