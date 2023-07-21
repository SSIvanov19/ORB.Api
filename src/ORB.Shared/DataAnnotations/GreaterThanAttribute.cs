// <copyright file="GreaterThanAttribute.cs" company="ORB">
// Copyright (c) ORB. All rights reserved.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace ORB.Shared.DataAnnotations;

/// <summary>
/// Custom validation attribute for checking if a date is greater than another date property in the same class.
/// </summary>
internal class GreaterThanAttribute : ValidationAttribute
{
    private readonly string nameOfPropertyToCompareTo;

    /// <summary>
    /// Initializes a new instance of the <see cref="GreaterThanAttribute"/> class.
    /// </summary>
    /// <param name="nameOfPropertyToCompareTo">Name of the property you want to compare to.</param>
    public GreaterThanAttribute(string nameOfPropertyToCompareTo)
    {
        this.nameOfPropertyToCompareTo = nameOfPropertyToCompareTo;
    }

    /// <summary>
    /// Determines whether the specified value is valid.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="validationContext">Validation context.</param>
    /// <returns>
    ///   <c>true</c> if the specified value is valid; otherwise, <c>false</c>.
    /// </returns>
    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
        {
            return ValidationResult.Success!;
        }

        if (value is not string stringValue)
        {
            return new ValidationResult($"{validationContext.MemberName} is not string.");
        }

        if (!DateOnly.TryParseExact(stringValue, "yyyy-MM-dd", out var dateOnlyResult))
        {
            return new ValidationResult($"{validationContext.MemberName} is not in the correct format. The correct format is (yyyy-MM-dd).");
        }

        var propertyToCompareTo = validationContext.ObjectType.GetProperty(nameOfPropertyToCompareTo);

        if (propertyToCompareTo is null)
        {
            return new ValidationResult($"{this.nameOfPropertyToCompareTo} is null.");
        }

        var valueOfPropertyToCompareTo = propertyToCompareTo.GetValue(validationContext.ObjectInstance);

        if (valueOfPropertyToCompareTo is null)
        {
            return new ValidationResult($"{this.nameOfPropertyToCompareTo} is null.");
        }

        if (valueOfPropertyToCompareTo is not string stringPropertyToCompareTo)
        {
            return new ValidationResult($"{this.nameOfPropertyToCompareTo} is not string.");
        }

        if (!DateOnly.TryParseExact(stringPropertyToCompareTo, "yyyy-MM-dd", out var dateOnlyPropertyToCompareTo))
        {
            return new ValidationResult($"{this.nameOfPropertyToCompareTo} is not in the correct format. The correct format is (yyyy-MM-dd).");
        }

        if (dateOnlyResult > dateOnlyPropertyToCompareTo)
        {
            return ValidationResult.Success!;
        }

        return new ValidationResult($"{validationContext.MemberName} should be greater than {this.nameOfPropertyToCompareTo}.");
    }
}