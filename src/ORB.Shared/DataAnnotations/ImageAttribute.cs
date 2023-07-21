// <copyright file="ImageAttribute.cs" company="ORB">
// Copyright (c) ORB. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;
using ImageMagick;
using Microsoft.AspNetCore.Http;

namespace ORB.Shared.DataAnnotations;

/// <summary>
/// Image attribute.
/// </summary>
public class ImageAttribute : ValidationAttribute
{
    /// <summary>
    /// Check if the file uploaded is a valid image.
    /// </summary>
    /// <param name="value">The file.</param>
    /// <returns>Is the file an image.</returns>
    public override bool IsValid(object? value)
    {
        if (value == null)
        {
            return true;
        }

        if (value is not IFormFile image)
        {
            return false;
        }

        try
        {
            using MagickImage magickImage = new (image.OpenReadStream());
            return true;
        }
        catch
        {
            return false;
        }
    }
}
