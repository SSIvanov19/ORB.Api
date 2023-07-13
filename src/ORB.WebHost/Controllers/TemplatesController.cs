using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ORB.Data.Data;
using ORB.Data.Models.Resumes;
using ORB.Services.Contracts;
using ORB.Services.Implementations;

namespace ORB.WebHost.Controllers;

[Route("/[controller]")]
[ApiController]
public class TemplatesController : ControllerBase
{
    private readonly ITemplateService templateService;

    /// <summary>
    /// Initializes a new instance of the <see cref="TemplatesController"/> class.
    /// </summary>
    /// <param name="templateService"></param>
    public TemplatesController(ITemplateService templateService)
    {
        this.templateService = templateService;
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Template>>> GetTemplates()
    {
        var templates = await this.templateService.GetAllTemplatesAsync();
        if (templates is null)
        {
            return this.NotFound();
        }

        return this.Ok(templates);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Template>> GetTemplateById(string id)
    {
        var template = await this.templateService.FindTemplateByIdAsync(id);

        if (template is null)
        {
            return this.NotFound();
        }

        return this.Ok(template);
    }
}
