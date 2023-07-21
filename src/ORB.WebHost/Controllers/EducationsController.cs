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
    private readonly ILogger<EducationsController> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="EducationsController"/> class.
    /// </summary>
    /// <param name="currentUser">Current user.</param>
    /// <param name="resumeService">Resume service.</param>
    /// <param name="educationService">Education service.</param>
    /// <param name="logger">Logger.</param>
    public EducationsController(
        ICurrentUser currentUser,
        IResumeService resumeService,
        IEducationService educationService,
        ILogger<EducationsController> logger)
    {
        this.currentUser = currentUser;
        this.resumeService = resumeService;
        this.educationService = educationService;
        this.logger = logger;
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
        this.logger.LogInformation($"CreateEducationAsync method in the EducationsController class : User with id: {this.currentUser.UserId} is trying to create education for resume with id: {educationModel.ResumeId}");

        if (resume is null)
        {
            this.logger.LogWarning($"CreateEducationAsync method in the EducationsController class : There isn't a resume with this id: {educationModel.ResumeId}");
            return this.NotFound("There isn't a resume with this id!");
        }

        if (resume.UserId != this.currentUser.UserId)
        {
            this.logger.LogWarning($"CreateEducationAsync method in the EducationsController class : User doesn't have access to this resume - {this.currentUser.UserId}");
            return this.Forbid("User doesn't have access to this resume!");
        }

        if (resume.IsDeleted)
        {
            this.logger.LogWarning($"CreateEducationAsync method in the EducationsController class : Resume with id: {resume.Id} is soft deleted");
            return this.BadRequest("Resume is deleted!");
        }

        var education = await this.educationService.CreateEducationInfoAsync(educationModel);

        this.logger.LogInformation($"CreateEducationAsync method in the EducationsController class : User with Id: {this.currentUser.UserId} created a new education item for resume with id: {resume.Id}");
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
        this.logger.LogInformation($"GetEducationByIdAsync method : User with id: {this.currentUser.UserId} is trying to retrieve education item with id: {id} from resume.");
        var education = await this.educationService.GetEducationByIdAsync(id, this.currentUser.UserId);

        if (education is null)
        {
            this.logger.LogWarning($"GetEducationByIdAsync method : Couldn't find education item with id: {id} in resume for user with id: {this.currentUser.UserId}");
            return this.NotFound();
        }

        this.logger.LogInformation($"GetEducationByIdAsync method : User with id: {this.currentUser.UserId} successfully retrieved education item with id: {id} from resume.");
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
        this.logger.LogInformation($"UpdateEducationInfoByIdAsync method : User with id: {this.currentUser.UserId} is trying to update education item with id: {id} from resume.");
        var education = await this.educationService.GetEducationByIdAsync(id, this.currentUser.UserId);

        if (education is null)
        {
            this.logger.LogWarning($"UpdateEducationInfoByIdAsync method : Couldn't find education item with id: {id} in resume for user with id: {this.currentUser.UserId}");
            return this.NotFound();
        }

        education = await this.educationService.UpdateEducationInfoAsync(id, educationModel);

        this.logger.LogInformation($"UpdateEducationInfoByIdAsync method : User with id: {this.currentUser.UserId} successfully updated education item with id: {id} from resume.");

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
        this.logger.LogInformation($"DeleteEducationInfoByIdAsync method : User with id: {this.currentUser.UserId} is trying to delete education item with id: {id} from resume.");
        var education = await this.educationService.GetEducationByIdAsync(id, this.currentUser.UserId);

        if (education is null)
        {
            this.logger.LogWarning($"DeleteEducationInfoByIdAsync method : Couldn't find education item with id: {id} in resume for user with id: {this.currentUser.UserId}");
            return this.NotFound();
        }

        await this.educationService.DeleteEducationInfoByIdAsync(id);

        this.logger.LogInformation($"DeleteEducationInfoByIdAsync method : User with id: {this.currentUser.UserId} successfully deleted education item with id: {id} from resume.");

        return this.Ok();
    }
}
