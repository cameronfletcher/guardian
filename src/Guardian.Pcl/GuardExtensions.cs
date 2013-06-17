// <copyright file="GuardExtensions.cs" company="Guardian contributors">
//  Copyright (c) Guardian contributors. All rights reserved.
// </copyright>
// <summary>Guardian. Mostly of null values.</summary>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

// ReSharper disable CheckNamespace
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedParameter.Global

/// <summary>
/// Provides extension methods for the <see cref="Guard"/> clause.
/// </summary>
internal static class GuardExtensions
{
    /// <summary>
    /// Guard against null or empty argument values.
    /// </summary>
    /// <typeparam name="T">The type of value to guard against.</typeparam>
    /// <param name="guard">The Guard clause.</param>
    /// <param name="expression">An expression returning the value to guard against.</param>
    [DebuggerStepThrough]
    [Obsolete("This method is not fully supported in portable class libraries. Consider using Guard.Against.NullOrEmpty(value, parameterName) instead.")]
    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "May not be called.")]
    [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "guard", Justification = "By design.")]
    public static void NullOrEmpty<T>(this Guard guard, Func<IEnumerable<T>> expression)
    {
        Guard.Against.Null(expression, "expression");

        if (!expression().Any())
        {
            throw new ArgumentException("Value cannot be empty.");
        }
    }

    /// <summary>
    /// Guard against null or empty argument values.
    /// </summary>
    /// <typeparam name="T">The type of value to guard against.</typeparam>
    /// <param name="guard">The Guard clause.</param>
    /// <param name="value">The value to guard against.</param>
    /// <param name="parameterName">Name of the parameter.</param>
    [DebuggerStepThrough]
    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "May not be called.")]
    [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "guard", Justification = "By design.")]
    public static void NullOrEmpty<T>(this Guard guard, IEnumerable<T> value, string parameterName)
    {
        Guard.Against.Null(value, "value");
        Guard.Against.Null(parameterName, "parameterName");

        if (!value.Any())
        {
            throw new ArgumentException("Value cannot be empty.", parameterName);
        }
    }

    /// <summary>
    /// Guard against null or empty or null elements argument values.
    /// </summary>
    /// <typeparam name="T">The type of value to guard against.</typeparam>
    /// <param name="guard">The guard clause.</param>
    /// <param name="expression">An expression returning the value to guard against.</param>
    [DebuggerStepThrough]
    [Obsolete("This method is not fully supported in portable class libraries. Consider using Guard.Against.NullOrEmptyOrNullElements(value, parameterName) instead.")]
    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "May not be called.")]
    [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "guard", Justification = "By design.")]
    public static void NullOrEmptyOrNullElements<T>(this Guard guard, Func<IEnumerable<T>> expression)
        where T : class
    {
        Guard.Against.NullOrEmpty(expression);

        if (expression().Any(element => element == null))
        {
            throw new ArgumentException("Value cannot contain null elements.");
        }
    }

    /// <summary>
    /// Guard against null or empty or null elements argument values.
    /// </summary>
    /// <typeparam name="T">The type of value to guard against.</typeparam>
    /// <param name="guard">The guard clause.</param>
    /// <param name="expression">An expression returning the value to guard against.</param>
    [DebuggerStepThrough]
    [Obsolete("This method is not fully supported in portable class libraries. Consider using Guard.Against.NullOrEmptyOrNullElements(value, parameterName) instead.")]
    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "May not be called.")]
    [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "guard", Justification = "By design.")]
    public static void NullOrEmptyOrNullElements<T>(this Guard guard, Func<IEnumerable<T?>> expression)
        where T : struct
    {
        Guard.Against.NullOrEmpty(expression);

        if (expression().Any(element => !element.HasValue))
        {
            throw new ArgumentException("Value cannot contain null elements.");
        }
    }

    /// <summary>
    /// Guard against null or empty or null elements argument values.
    /// </summary>
    /// <typeparam name="T">The type of value to guard against.</typeparam>
    /// <param name="guard">The Guard clause.</param>
    /// <param name="value">The value to guard against.</param>
    /// <param name="parameterName">Name of the parameter.</param>
    [DebuggerStepThrough]
    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "May not be called.")]
    [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "guard", Justification = "By design.")]
    public static void NullOrEmptyOrNullElements<T>(this Guard guard, IEnumerable<T> value, string parameterName)
        where T : class
    {
        Guard.Against.NullOrEmpty(value, parameterName);

        if (value.Any(element => element == null))
        {
            throw new ArgumentException("Value cannot contain null elements.", parameterName);
        }
    }

    /// <summary>
    /// Guard against null or empty or null elements argument values.
    /// </summary>
    /// <typeparam name="T">The type of value to guard against.</typeparam>
    /// <param name="guard">The Guard clause.</param>
    /// <param name="value">The value to guard against.</param>
    /// <param name="parameterName">Name of the parameter.</param>
    [DebuggerStepThrough]
    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "May not be called.")]
    [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "guard", Justification = "By design.")]
    public static void NullOrEmptyOrNullElements<T>(this Guard guard, IEnumerable<T?> value, string parameterName)
        where T : struct
    {
        Guard.Against.NullOrEmpty(value, parameterName);

        if (value.Any(element => !element.HasValue))
        {
            throw new ArgumentException("Value cannot contain null elements.", parameterName);
        }
    }
}