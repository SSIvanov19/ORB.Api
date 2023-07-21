using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ORB.Services.Contracts;
using ORB.Shared.Models.Templates;

namespace ORB.WebHost.Controllers;

/// <summary>
/// Templates controller used to manage templates.
/// </summary>
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TemplatesController : ControllerBase
{
    private readonly ITemplateService templateService;
    private readonly ILogger<TemplatesController> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="TemplatesController"/> class.
    /// </summary>
    /// <param name="templateService">Template service.</param>
    /// <param name="logger">Logger.</param>
    public TemplatesController(ITemplateService templateService, ILogger<TemplatesController> logger)
    {
        this.templateService = templateService;
        this.logger = logger;
    }

    /// <summary>
    /// Get all available templates.
    /// </summary>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TemplateVM>>> GetTemplates()
    {
        this.logger.LogInformation("Trying to get all templates");

        var templates = await this.templateService.GetAllTemplatesAsync();

        if (templates is null)
        {
            this.logger.LogWarning("There aren't any templates");
            return this.NotFound();
        }

        this.logger.LogInformation("Successfully got all templates");
        return this.Ok(templates);
    }

    /// <summary>
    /// Get a template by id.
    /// </summary>
    /// <param name="id">Id of the template.</param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<TemplateVM>> GetTemplateById(string id)
    {
        this.logger.LogInformation($"Trying to get template with id: {id}");
        var template = await this.templateService.FindTemplateByIdAsync(id);

        if (template is null)
        {
            this.logger.LogWarning($"There isn't a template with this id: {id}");
            return this.NotFound();
        }

        this.logger.LogInformation($"Successfully got template with id: {id}");
        return this.Ok(template);
    }
}
