// <copyright file="ICurrentUser.cs" company="ORB">
// Copyright (c) ORB. All rights reserved.
// </copyright>

namespace ORB.Services.Contracts;

/// <summary>
/// Interface for current user.
/// </summary>
public interface ICurrentUser
{
    /// <summary>
    /// Gets the id of the user.
    /// </summary>
    string UserId { get; }
}
