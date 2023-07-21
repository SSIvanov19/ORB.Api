// <copyright file="EducationService.cs" company="ORB">
// Copyright (c) ORB. All rights reserved.
// </copyright>

using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ORB.Data.Data;
using ORB.Data.Models.Resumes;
using ORB.Services.Contracts;
using ORB.Shared.Models.Education;

namespace ORB.Services.Implementations;

/// <summary>
/// Service implementing the <see cref="IEducationService"/> interface.
/// </summary>
internal class EducationService : IEducationService
{
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="EducationService"/> class.
    /// </summary>
    /// <param name="context">Context.</param>
    /// <param name="mapper">Mapper.</param>
    public EducationService(ApplicationDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<EducationVM> CreateEducationInfoAsync(EducationIM educationModel)
    {
        var education = this.mapper.Map<Education>(educationModel);

        if (education.EndDate == DateOnly.MinValue)
        {
            education.EndDate = null;
        }

        var resume = await this.context.Resumes.FindAsync(education.ResumeId);

        education.Resume = resume;

        await this.context.Educations.AddAsync(education);

        await this.context.SaveChangesAsync();

        education = await this.context.Educations
                                        .Include(e => e.Resume)
                                        .Where(e => e.Id == education.Id)
                                        .FirstOrDefaultAsync();

        education!.Resume.LastModified = DateTime.UtcNow;

        await this.context.SaveChangesAsync();

        return this.mapper.Map<EducationVM>(education);
    }

    /// <inheritdoc/>
    public async Task DeleteEducationInfoByIdAsync(string id)
    {
        var education = await this.context.Educations.FindAsync(id);

        this.context.Educations.Remove(education!);

        await this.context.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task<List<EducationVM>> GetAllEducationsForResumeWithIdAsync(string id)
    {
        return await this.context.Educations.Where(e => e.ResumeId == id)
                                            .ProjectTo<EducationVM>(this.mapper.ConfigurationProvider)
                                            .ToListAsync();
    }

    /// <inheritdoc/>
    public async Task<EducationVM?> GetEducationByIdAsync(string id, string userId)
    {
        var education = await this.context.Educations
                                    .Include(e => e.Resume)
                                    .Where(e => e.Id == id)
                                    .FirstOrDefaultAsync();

        if (education is null)
        {
            return null;
        }

        if (education.Resume.UserId != userId)
        {
            return null;
        }

        if (education.Resume.IsDeleted)
        {
            return null;
        }

        return this.mapper.Map<EducationVM>(education);
    }

    /// <inheritdoc/>
    public async Task<EducationVM> UpdateEducationInfoAsync(string id, EducationUM educationModel)
    {
        var education = await this.context.Educations.FindAsync(id);

        education!.SchoolName = educationModel.SchoolName;
        education.Degree = educationModel.Degree;
        education.FieldOfStudy = educationModel.FieldOfStudy;
        education.Description = educationModel.Description;
        education.StartDate = DateOnly.ParseExact(educationModel.StartDate, "yyyy-MM-dd");

        if (string.IsNullOrEmpty(educationModel.EndDate))
        {
            education.EndDate = null;
        }
        else
        {
            education.EndDate = DateOnly.ParseExact(educationModel.EndDate, "yyyy-MM-dd");
        }

        await this.context.SaveChangesAsync();

        return this.mapper.Map<EducationVM>(education);
    }
}
