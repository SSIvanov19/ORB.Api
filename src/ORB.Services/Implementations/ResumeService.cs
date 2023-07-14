// <copyright file="ResumeService.cs" company="ORB">
// Copyright (c) ORB. All rights reserved.
// </copyright>

using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ORB.Data.Data;
using ORB.Data.Models.Resumes;
using ORB.Services.Contracts;
using ORB.Shared.Models.Resume;

namespace ORB.Services.Implementations;

/// <summary>
/// Class implementing the <see cref="IResumeService"/> interface.
/// </summary>
internal class ResumeService : IResumeService
{
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="ResumeService"/> class.
    /// </summary>
    /// <param name="context">DB Context.</param>
    /// <param name="mapper">Mapper.</param>
    public ResumeService(ApplicationDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    /// <inheritdoc/>
    public async Task<ResumeVM> CreateResumeAsync(ResumeIM resumeIM, string personaInfoId, string userId)
    {
        var resume = this.mapper.Map<Resume>(resumeIM);

        resume.User = null;
        resume.PersonalInfoId = personaInfoId;
        resume.UserId = userId;

        await this.context.Resumes.AddAsync(resume);

        await this.context.SaveChangesAsync();

        return this.mapper.Map<ResumeVM>(resume);
    }

    /// <inheritdoc/>
    public async Task<List<ResumeVM>> GetAllResumesForUserWithIdAsync(string userId)
    {
        return await this.context.Resumes
                            .Where(r => r.UserId == userId)
                            .ProjectTo<ResumeVM>(this.mapper.ConfigurationProvider)
                            .ToListAsync();
    }

    /// <inheritdoc/>
    public async Task<ResumeVM?> GetResumeByIdAsync(string id)
    {
        return await this.context.Resumes
                            .Where(r => r.Id == id)
                            .ProjectTo<ResumeVM>(this.mapper.ConfigurationProvider)
                            .FirstOrDefaultAsync();
    }

    /// <inheritdoc/>
    public async Task<ResumeVM> UpdateResumeInfoWithIdAsync(string id, ResumeIM newResumeInfo)
    {
        var resume = await this.context.Resumes.Where(r => r.Id == id).FirstOrDefaultAsync();

        resume!.Title = newResumeInfo.Title;
        resume!.Template = null;
        resume!.TemplateId = newResumeInfo.TemplateId;
        resume!.LastModified = DateTime.UtcNow;

        await this.context.SaveChangesAsync();

        return this.mapper.Map<ResumeVM>(resume);
    }
}
