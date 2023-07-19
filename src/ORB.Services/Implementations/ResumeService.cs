// <copyright file="ResumeService.cs" company="ORB">
// Copyright (c) ORB. All rights reserved.
// </copyright>

using AutoMapper;
using AutoMapper.QueryableExtensions;
using HandlebarsDotNet;
using Microsoft.EntityFrameworkCore;
using ORB.Data.Data;
using ORB.Data.Models.Resumes;
using ORB.Services.Contracts;
using ORB.Shared.Models.Resume;
using PuppeteerSharp.Media;
using PuppeteerSharp;

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
    public async Task DeleteResumeAsync(string id)
    {
        var resume = await this.context.Resumes.FindAsync(id);

        resume!.IsDeleted = true;

        await this.context.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task<List<ResumeVM>> GetAllResumesForUserWithIdAsync(string userId)
    {
        return await this.context.Resumes
                            .Where(r => r.UserId == userId && !r.IsDeleted)
                            .ProjectTo<ResumeVM>(this.mapper.ConfigurationProvider)
                            .ToListAsync();
    }

    /// <inheritdoc/>
    public async Task<List<ResumeVM>> GetAllDeletedResumesForUserWithIdAsync(string userId)
    {
        return await this.context.Resumes
                            .Where(r => r.UserId == userId && r.IsDeleted)
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

    /// <inheritdoc/>
    public async Task RecoverResumeAsync(string id)
    {
        var resume = await this.context.Resumes.FindAsync(id);

        resume!.IsDeleted = false;

        await this.context.SaveChangesAsync();
    }

    public async Task<MemoryStream> CreatePDFForResumeAsync(ResumeVM resume)
    {
        var template = await this.context.Templates.FindAsync(resume.TemplateId);

        var source = template!.Content;

        var handlebars = Handlebars.Compile(source);

        var personalInfo = await this.context.PersonalInfo.FindAsync(resume.PersonalInfoId);

        var educations = await this.context.Educations.Where(e => e.ResumeId == resume.Id).ToListAsync();

        var workExperience = await this.context.WorkExperiences.Where(e => e.ResumeId == resume.Id).ToListAsync();

        var data = new
        {
            FullName = resume.UserFullNames,
            ImageUrl = personalInfo.PersonImageURL,
            personalInfo.Summary,
            Contacts = new
            {
                personalInfo.Address,
                personalInfo.PhoneNumber,
                personalInfo.Email,
            },
            Education = educations.ToArray(),
            Experience = workExperience.ToArray(),
        };

        var html = handlebars(data);

        await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultChromiumRevision);
        await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
        {
            Headless = true,
        });
        await using var page = await browser.NewPageAsync();
        await page.EmulateMediaTypeAsync(MediaType.Screen);
        await page.SetContentAsync(html);
        using var pdfContent = await page.PdfStreamAsync(new PdfOptions
        {
            PrintBackground = true,
        });

        var stream = new MemoryStream();
        pdfContent.CopyTo(stream);
        return stream;
    }
}
