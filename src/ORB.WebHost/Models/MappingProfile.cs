// <copyright file="MappingProfile.cs" company="ORB">
// Copyright (c) ORB. All rights reserved.
// </copyright>

using AutoMapper;
using ORB.Data.Models.Auth;
using ORB.Data.Models.Resumes;
using ORB.Shared.Models.Auth.User;
using ORB.Shared.Models.Education;
using ORB.Shared.Models.PersonalInfo;
using ORB.Shared.Models.Resume;
using ORB.Shared.Models.Templates;
using ORB.Shared.Models.WorkExperience;

namespace ORB.WebHost.Models;

/// <summary>
/// Mapping profile.
/// </summary>
public class MappingProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="MappingProfile"/> class.
    /// </summary>
    public MappingProfile()
    {
        this.CreateMap<User, UserVM>();
        this.CreateMap<UserUM, UserIM>();
        this.CreateMap<ResumeIM, Resume>();
        this.CreateMap<Resume, ResumeVM>()
            .ForMember(d => d.UserFullNames, cfg => cfg.MapFrom(s => $"{s.User.FirstName} {s.User.LastName}"))
            .ForMember(d => d.EducationsIds, cfg => cfg.MapFrom(s => s.Educations.Select(e => e.Id)))
            .ForMember(d => d.WorkExperienceIds, cfg => cfg.MapFrom(s => s.WorkExperience.Select(e => e.Id)));
        this.CreateMap<PersonalInfo, PersonalInfoVM>();
        this.CreateMap<PersonalInfoIM, PersonalInfo>();
        this.CreateMap<EducationIM, Education>()
            .ForMember(d => d.StartDate, cfg => cfg.MapFrom(s => DateOnly.ParseExact(s.StartDate, "yyyy-MM-dd")))
            .ForMember(d => d.EndDate, cfg => cfg.MapFrom(s => DateOnly.ParseExact(s.EndDate!, "yyyy-MM-dd")));
        this.CreateMap<Education, EducationVM>();
        this.CreateMap<Template, TemplateVM>();
        this.CreateMap<TemplateIM, Template>();
        this.CreateMap<WorkExperienceIM, WorkExperience>()
            .ForMember(d => d.StartDate, cfg => cfg.MapFrom(s => DateOnly.ParseExact(s.StartDate, "yyyy-MM-dd")))
            .ForMember(d => d.EndDate, cfg => cfg.MapFrom(s => DateOnly.ParseExact(s.EndDate!, "yyyy-MM-dd")));
        this.CreateMap<WorkExperience, WorkExperienceVM>();
    }
}
