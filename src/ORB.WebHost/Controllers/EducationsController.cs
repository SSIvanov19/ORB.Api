using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ORB.Services.Contracts;
using ORB.Shared.Models.Education;

namespace ORB.WebHost.Controllers;

/// <summary>
/// Controller for Education related actions such as creating, viewing, updating and deleting education information in a resume.
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class EducationsController : ControllerBase
{
    private readonly ICurrentUser currentUser;
    private readonly IResumeService resumeService;
    private readonly IEducationService educationService;

    /// <summary>
    /// Initializes a new instance of the <see cref="EducationsController"/> class.
    /// </summary>
    /// <param name="currentUser">Current user.</param>
    /// <param name="resumeService">Resume service.</param>
    /// <param name="educationService">Education resume.</param>
    public EducationsController(
        ICurrentUser currentUser,
        IResumeService resumeService,
        IEducationService educationService)
    {
        this.currentUser = currentUser;
        this.resumeService = resumeService;
        this.educationService = educationService;
    }

    /// <summary>
    /// Creates a new education item in a resume.
    /// </summary>
    /// <param name="educationModel">Input model for creating a new education item.</param>
    /// <returns>Returns the created education item.</returns>
    [HttpPost]
    public async Task<ActionResult<EducationVM>> CreateEducationAsync(EducationIM educationModel)
    {
        var resume = await this.resumeService.GetResumeByIdAsync(educationModel.ResumeId);

        if (resume is null)
        {
            return this.NotFound("There isn't a resume with this id!");
        }

        if (resume.UserId != this.currentUser.UserId)
        {
            return this.Forbid("User doesn't have access to this resume!");
        }

        var education = await this.educationService.CreateResumeAsync(educationModel);

        return this.Ok(education);
    }

    /// <summary>
    /// Retrieves a specific education item in a resume.
    /// </summary>
    /// <param name="id">Identifier for the education item.</param>
    /// <returns>Returns the requested education item.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<EducationVM>> GetEducationByIdAsync(string id)
    {
        var education = await this.educationService.GetEducationByIdAsync(id, this.currentUser.UserId);

        if (education is null)
        {
            return this.NotFound();
        }

        return this.Ok(education);
    }

    /// <summary>
    /// Updates a specific education item in a resume.
    /// </summary>
    /// <param name="id">Identifier for the education item.</param>
    /// <param name="educationModel">Input model for updating the education item.</param>
    /// <returns>Returns the updated education item.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<EducationVM>> UpdateEducationInfoByIdAsync(string id, EducationUM educationModel)
    {
        var education = await this.educationService.GetEducationByIdAsync(id, this.currentUser.UserId);

        if (education is null)
        {
            return this.NotFound();
        }

        education = await this.educationService.UpdateEducationInfoAsync(id, educationModel);

        return this.Ok(education);
    }

    /// <summary>
    /// Deletes a specific education item from a resume.
    /// </summary>
    /// <param name="id">Identifier for the education item.</param>
    /// <returns>Returns NoContent if the deletion is successful.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEducationInfoByIdAsync(string id)
    {
        var education = await this.educationService.GetEducationByIdAsync(id, this.currentUser.UserId);

        if (education is null)
        {
            return this.NotFound();
        }

        await this.educationService.DeleteEducationInfoByIdAsync(id);

        return this.Ok();
    }
}
