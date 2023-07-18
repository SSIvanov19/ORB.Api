// <copyright file="IFileService.cs" company="ORB">
// Copyright (c) ORB. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Http;

namespace ORB.Services.Contracts;

/// <summary>
/// An interface for file service.
/// </summary>
public interface IFileService
{
    /// <summary>
    /// Saves image to the server.
    /// </summary>
    /// <param name="file">Image.</param>
    /// <param name="containerName">Container for the image.</param>
    /// <returns>URL of the image.</returns>
    Task<string> SaveImageAsync(IFormFile file, string containerName);
}
