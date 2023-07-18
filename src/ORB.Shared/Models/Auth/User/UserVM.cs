// <copyright file="UserVM.cs" company="ORB">
// Copyright (c) ORB. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace ORB.Shared.Models.Auth.User;

/// <summary>
/// Represents the view model for a User.
/// </summary>
public class UserVM
{
    /// <summary>
    /// Gets or sets the id of the User.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the first name of the User.
    /// </summary>
    [Display(Name = "First name")]
    [Required(ErrorMessage = "First name is required")]
    [RegularExpression("^(?=.*[A-ZА-Яа-яa-z])([A-ZА-Я])([a-zа-я]{2,29})+(?<![_.])$", ErrorMessage = "First name is not valid")]
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the last name of the User.
    /// </summary>
    [Display(Name = "Last name")]
    [Required(ErrorMessage = "Last name is required")]
    [RegularExpression("^(?=.*[A-ZА-Яа-яa-z])([A-ZА-Я])([a-zа-я]{2,29})+(?<![_.])$", ErrorMessage = "Last name is not valid")]
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email of the User.
    /// </summary>
    [Required(ErrorMessage = "Email name is required")]
    [EmailAddress(ErrorMessage = "Email name is not in the correct format")]
    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;
}
