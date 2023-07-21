// <copyright file="DateOnlyAttribute.cs" company="ORB">
// Copyright (c) ORB. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace ORB.Shared.DataAnnotations;

/// <summary>
///   Validates date strings with format "yyyy-MM-dd".
/// </summary>
internal class DateOnlyAttribute : ValidationAttribute
{
    /// <summary>
    ///   Returns true if the value is null or if it can be parsed into a valid date "yyyy-MM-dd" format.
    ///   Otherwise, returns false.
    /// </summary>
    /// <param name="value">The object to validate.</param>
    /// <returns>True if the object is null or a valid date string. Otherwise, false.</returns>

    public override bool IsValid(object? value)
    {
        if (value is null)
        {
            return true;
        }

        if (value is not string stringValue)
        {
            return false;
        }

        return DateOnly.TryParseExact(stringValue, "yyyy-MM-dd", out _);
    }
}
