// <copyright file="UpdatePasswordModel.cs" company="ORB">
// Copyright (c) ORB. All rights reserved.
// </copyright>

namespace ORB.Shared.Models.Auth;

/// <summary>
/// Model class for updating password.
/// </summary>
public class UpdatePasswordModel
{
    /// <summary>
    /// Gets or sets the old password.
    /// </summary>
    public string OldPassword { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the new password.
    /// </summary>
    public string NewPassword { get; set; } = string.Empty;
}
