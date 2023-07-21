// <copyright file="PersonalInfoVM.cs" company="ORB">
// Copyright (c) ORB. All rights reserved.
// </copyright>

namespace ORB.Shared.Models.PersonalInfo;

/// <summary>
/// Represents a Personal Information View Model.
/// </summary>
public class PersonalInfoVM
{
    /// <summary>
    /// Gets or sets the unique identifier of person information.
    /// </summary>
    public string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// Gets or sets the full name of person.
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the address of person.
    /// </summary>
    public string Address { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the phone number of person.
    /// </summary>
    public string PhoneNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email of person.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the summary of person's skills and experience.
    /// </summary>
    public string Summary { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the URL of person's image.
    /// </summary>
    public string? PersonImageURL { get; set; } = null;
}
