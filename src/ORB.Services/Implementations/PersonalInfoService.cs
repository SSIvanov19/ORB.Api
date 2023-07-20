// <copyright file="PersonalInfoService.cs" company="ORB">
// Copyright (c) ORB. All rights reserved.
// </copyright>

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ORB.Data.Data;
using ORB.Data.Models.Resumes;
using ORB.Services.Contracts;
using ORB.Shared.Models.PersonalInfo;

namespace ORB.Services.Implementations;

/// <summary>
/// This class provides operations for creating and retrieving personal information.
/// </summary>
internal class PersonalInfoService : IPersonalInfoService
{
    private readonly ApplicationDbContext context;
    private readonly IUserService userService;
    private readonly IMapper mapper;
    private readonly IFileService fileService;

    /// <summary>
    /// Initializes a new instance of the <see cref="PersonalInfoService"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    /// <param name="userService">An instance of IUserService.</param>
    /// <param name="mapper">An instance of IMapper.</param>
    /// <param name="fileService">An instance of file service.</param>
    public PersonalInfoService(
        ApplicationDbContext context,
        IUserService userService,
        IMapper mapper,
        IFileService fileService)
    {
        this.context = context;
        this.userService = userService;
        this.mapper = mapper;
        this.fileService = fileService;
    }

    /// <inheritdoc/>
    public async Task<PersonalInfoVM> CreateDefaultPersonalInfoForUserWithIdAsync(string id)
    {
        var personalInfo = new PersonalInfo();

        var user = await this.userService.GetUserByIdAsync(id);

        personalInfo.FullName = user!.FirstName + " " + user.LastName;

        await this.context.PersonalInfo.AddAsync(personalInfo);
        await this.context.SaveChangesAsync();

        return this.mapper.Map<PersonalInfoVM>(personalInfo);
    }

    /// <inheritdoc/>
    public async Task<PersonalInfoVM?> GetPersonalInfoByIdAsync(string id, string userId)
    {
        var personalInfo = await this.context.Resumes
                                                .Where(r => r.PersonalInfoId == id && r.UserId == userId && !r.IsDeleted)
                                                .Include(r => r.PersonalInfo)
                                                .Select(r => r.PersonalInfo)
                                                .FirstOrDefaultAsync();

        return this.mapper.Map<PersonalInfoVM>(personalInfo);
    }

    /// <inheritdoc/>
    public async Task<PersonalInfoVM> UpdatePersonalInfoWithIdAsync(string id, PersonalInfoIM newPersonalInfo)
    {
        var personalInfo = await this.context.PersonalInfo
                                                .Where(pi => pi.Id == id)
                                                .FirstOrDefaultAsync();

        personalInfo!.FullName = newPersonalInfo.FullName;
        personalInfo.Address = newPersonalInfo.Address;
        personalInfo.PhoneNumber = newPersonalInfo.PhoneNumber;
        personalInfo.Email = newPersonalInfo.Email;
        personalInfo.Summary = newPersonalInfo.Summary;

        if (newPersonalInfo.PersonImage is not null)
        {
            personalInfo.PersonImageURL = await this.fileService.SaveImageAsync(newPersonalInfo.PersonImage, "resumesimages");
        }

        var resume = await this.context.Resumes
                                            .Where(r => r.PersonalInfoId == id)
                                            .FirstOrDefaultAsync();

        resume!.LastModified = DateTime.UtcNow;

        await this.context.SaveChangesAsync();

        return this.mapper.Map<PersonalInfoVM>(personalInfo);
    }
}
