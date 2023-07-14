// <copyright file="MappingProfile.cs" company="ORB">
// Copyright (c) ORB. All rights reserved.
// </copyright>

using AutoMapper;
using ORB.Data.Models.Auth;
using ORB.Data.Models.Resumes;
using ORB.Shared.Models.Auth.User;
using ORB.Shared.Models.Templates;

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
        //this.CreateMap<Template, TemplateVM>();
        //this.CreateMap<TemplateVM, Template>();
    }
}
