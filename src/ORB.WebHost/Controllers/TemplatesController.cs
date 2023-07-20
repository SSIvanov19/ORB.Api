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

    /// <summary>
    /// Initializes a new instance of the <see cref="TemplatesController"/> class.
    /// </summary>
    /// <param name="templateService">Template service.</param>
    public TemplatesController(ITemplateService templateService)
    {
        this.templateService = templateService;
    }

    /// <summary>
    /// Get all available templates.
    /// </summary>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TemplateVM>>> GetTemplates()
    {
        var templates = await this.templateService.GetAllTemplatesAsync();

        if (templates is null)
        {
            return this.NotFound();
        }

        return this.Ok(templates);
    }

    /// <summary>
    /// Get a template by id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<TemplateVM>> GetTemplateById(string id)
    {
        var template = await this.templateService.FindTemplateByIdAsync(id);

        if (template is null)
        {
            return this.NotFound();
        }

        return this.Ok(template);
    }
}
