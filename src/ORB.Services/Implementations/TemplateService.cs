// <copyright file="TemplateService.cs" company="ORB">
// Copyright (c) ORB. All rights reserved.
// </copyright>

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ORB.Data.Data;
using ORB.Data.Models.Resumes;
using ORB.Services.Contracts;
using ORB.Shared.Models.Templates;

namespace ORB.Services.Implementations;

/// <summary>
/// Template Service
/// </summary>
public class TemplateService : ITemplateService
{
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="TemplateService"/> class.
    /// </summary>
    /// <param name="context">Context.</param>
    /// <param name="mapper">Mapper.</param>
    public TemplateService(ApplicationDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<TemplateVM>?> GetAllTemplatesAsync()
    {
        var templates = await this.context.Templates.ToListAsync();
        if (templates is null)
        {
        return null;
        }

        return this.mapper.Map<List<TemplateVM>>(templates);
    }

    /// <inheritdoc/>
    public async Task<TemplateVM?> FindTemplateByIdAsync(string id)
    {
        var template = await this.context.Templates.FindAsync(id);
        if (template is null)
        {
            return null;
        }

        return this.mapper.Map<TemplateVM>(template);
    }

    /// <inheritdoc/>
    public async Task AddTemplateIfDoesNotExistAsync(TemplateIM templateIM)
    {
        var templates = await this.context.Templates.Where(t => t.Content == templateIM.Content).ToListAsync();

        if (templates.Count != 0)
        {
            return;
        }

        var template = this.mapper.Map<Template>(templateIM);

        await this.context.Templates.AddAsync(template);

        await this.context.SaveChangesAsync();
    }
}