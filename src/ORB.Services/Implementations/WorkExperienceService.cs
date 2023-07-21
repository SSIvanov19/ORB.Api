// <copyright file="WorkExperienceService.cs" company="ORB">
// Copyright (c) ORB. All rights reserved.
// </copyright>

using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ORB.Data.Data;
using ORB.Data.Models.Resumes;
using ORB.Services.Contracts;
using ORB.Shared.Models.Education;
using ORB.Shared.Models.WorkExperience;

namespace ORB.Services.Implementations;

/// <summary>
/// Service implementing the <see cref="IWorkExperienceService"/> interface.
/// </summary>
internal class WorkExperienceService : IWorkExperienceService
{
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="WorkExperienceService"/> class.
    /// </summary>
    /// <param name="context">Context.</param>
    /// <param name="mapper">Mapper.</param>
    public WorkExperienceService(ApplicationDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<WorkExperienceVM> CreateWorkExperienceAsync(WorkExperienceIM workModel)
    {
        var workExperience = this.mapper.Map<WorkExperience>(workModel);

        if (workExperience.EndDate == DateOnly.MinValue)
        {
            workExperience.EndDate = null;
        }

        var resume = await this.context.Resumes.FindAsync(workExperience.ResumeId);

        workExperience.Resume = resume;

        await this.context.WorkExperiences.AddAsync(workExperience);

        await this.context.SaveChangesAsync();

        workExperience = await this.context.WorkExperiences
                                        .Include(e => e.Resume)
                                        .Where(e => e.Id == workExperience.Id)
                                        .FirstOrDefaultAsync();

        workExperience!.Resume.LastModified = DateTime.UtcNow;

        await this.context.SaveChangesAsync();

        return this.mapper.Map<WorkExperienceVM>(workExperience);
    }

    /// <inheritdoc/>
    public async Task DeleteWorkExperienceInfoByIdAsync(string id)
    {
        var workExperience = await this.context.WorkExperiences.FindAsync(id);

        this.context.WorkExperiences.Remove(workExperience!);

        await this.context.SaveChangesAsync();
    }

    public async Task<List<WorkExperienceVM>> GetAllWorkExperienceForResumeWithIdAsync(string id)
    {
        return await this.context.WorkExperiences.Where(e => e.ResumeId == id)
                                            .ProjectTo<WorkExperienceVM>(this.mapper.ConfigurationProvider)
                                            .ToListAsync();
    }

    /// <inheritdoc/>
    public async Task<WorkExperienceVM?> GetWorkExperienceByIdAsync(string id, string userId)
    {
        var workExperience = await this.context.WorkExperiences
                                    .Include(e => e.Resume)
                                    .Where(e => e.Id == id)
                                    .FirstOrDefaultAsync();

        if (workExperience is null)
        {
            return null;
        }

        if (workExperience.Resume.UserId != userId)
        {
            return null;
        }

        if (workExperience.Resume.IsDeleted)
        {
            return null;
        }

        return this.mapper.Map<WorkExperienceVM>(workExperience);
    }

    /// <inheritdoc/>
    public async Task<WorkExperienceVM> UpdateWorkExperienceInfoAsync(string id, WorkExperienceUM workModel)
    {
        var workExperience = await this.context.WorkExperiences.FindAsync(id);

        workExperience!.CompanyName = workModel.CompanyName;
        workExperience.Position = workModel.Position;
        workExperience.Description = workModel.Description;
        workExperience.StartDate = DateOnly.ParseExact(workModel.StartDate, "yyyy-MM-dd");

        if (string.IsNullOrEmpty(workModel.EndDate))
        {
            workExperience.EndDate = null;
        }
        else
        {
            workExperience.EndDate = DateOnly.ParseExact(workModel.EndDate, "yyyy-MM-dd");
        }

        await this.context.SaveChangesAsync();

        return this.mapper.Map<WorkExperienceVM>(workExperience);
    }
}
