// <copyright file="GuardExtensions.cs" company="Guardian contributors">
//  Copyright (c) Guardian contributors. All rights reserved.
// </copyright>
// <summary>Guardian. Mostly of null values.</summary>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1636:FileHeaderCopyrightTextMustMatch", Scope = "Module", Justification = "Content is valid.")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1641:FileHeaderCompanyNameTextMustMatch", Scope = "Module", Justification = "Content is valid.")]

// ReSharper disable CheckNamespace
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable PossibleMultipleEnumeration
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedParameter.Global

/// <summary>
/// Provides extension methods for the <see cref="Guard"/> clause.
/// </summary>
internal static class GuardExtensions
{
    /// <summary>
    /// Guard against null argument values.
    /// </summary>
    /// <typeparam name="T">The type of value to guard against.</typeparam>
    /// <param name="guard">The guard clause.</param>
    /// <param name="value">The value to guard against.</param>
    /// <param name="parameterName">Name of the parameter.</param>
    [DebuggerStepThrough]
    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "May not be called.")]
    [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "guard", Justification = "By design.")]
    public static void Null<T>(this Guard guard, T value, string parameterName)
        where T : class
    {
        Guard.Against.Null(() => parameterName);

        if (value == null)
        {
            throw new ArgumentNullException(parameterName, "Value cannot be empty.");
        }
    }

    /// <summary>
    /// Guard against null argument values.
    /// </summary>
    /// <typeparam name="T">The type of value to guard against.</typeparam>
    /// <param name="guard">The guard clause.</param>
    /// <param name="value">The value to guard against.</param>
    /// <param name="parameterName">Name of the parameter.</param>
    /// <param name="propertyName">Name of the property.</param>
    [DebuggerStepThrough]
    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "May not be called.")]
    [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "guard", Justification = "By design.")]
    public static void Null<T>(this Guard guard, T value, string parameterName, string propertyName)
        where T : class
    {
        Guard.Against.Null(() => parameterName);
        Guard.Against.Null(() => propertyName);

        if (value == null)
        {
            throw new ArgumentException("Value cannot be empty.", string.Concat(parameterName, ".", propertyName));
        }
    }

    /// <summary>
    /// Guard against null argument values.
    /// </summary>
    /// <typeparam name="T">The type of value to guard against.</typeparam>
    /// <param name="guard">The guard clause.</param>
    /// <param name="value">The value to guard against.</param>
    /// <param name="parameterName">Name of the parameter.</param>
    [DebuggerStepThrough]
    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "May not be called.")]
    [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "guard", Justification = "By design.")]
    public static void Null<T>(this Guard guard, T? value, string parameterName)
        where T : struct
    {
        Guard.Against.Null(() => parameterName);

        if (!value.HasValue)
        {
            throw new ArgumentNullException(parameterName, "Value cannot be empty.");
        }
    }

    /// <summary>
    /// Guard against null argument values.
    /// </summary>
    /// <typeparam name="T">The type of value to guard against.</typeparam>
    /// <param name="guard">The guard clause.</param>
    /// <param name="value">The value to guard against.</param>
    /// <param name="parameterName">Name of the parameter.</param>
    /// <param name="propertyName">Name of the property.</param>
    [DebuggerStepThrough]
    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "May not be called.")]
    [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "guard", Justification = "By design.")]
    public static void Null<T>(this Guard guard, T? value, string parameterName, string propertyName)
        where T : struct
    {
        Guard.Against.Null(() => parameterName);
        Guard.Against.Null(() => propertyName);

        if (!value.HasValue)
        {
            throw new ArgumentException("Value cannot be empty.", string.Concat(parameterName, ".", propertyName));
        }
    }

    /// <summary>
    /// Guard against null or empty argument values.
    /// </summary>
    /// <typeparam name="T">The type of value to guard against.</typeparam>
    /// <param name="guard">The guard clause.</param>
    /// <param name="expression">An expression returning the value to guard against.</param>
    [DebuggerStepThrough]
    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "May not be called.")]
    [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "guard", Justification = "By design.")]
    public static void NullOrEmpty<T>(this Guard guard, Func<IEnumerable<T>> expression)
    {
        Guard.Against.Null(expression);

        if (!expression().Any())
        {
            throw new ArgumentException("Value cannot be empty.", Guard.Expression.Parse(expression));
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
    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "May not be called.")]
    [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "guard", Justification = "By design.")]
    public static void NullOrEmptyOrNullElements<T>(this Guard guard, Func<IEnumerable<T>> expression)
        where T : class
    {
        Guard.Against.NullOrEmpty(expression);

        if (expression().Any(element => element == null))
        {
            throw new ArgumentException("Value cannot contain null elements.", Guard.Expression.Parse(expression));
        }
    }

    /// <summary>
    /// Guard against null or empty or null elements argument values.
    /// </summary>
    /// <typeparam name="T">The type of value to guard against.</typeparam>
    /// <param name="guard">The guard clause.</param>
    /// <param name="expression">An expression returning the value to guard against.</param>
    [DebuggerStepThrough]
    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "May not be called.")]
    [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "guard", Justification = "By design.")]
    public static void NullOrEmptyOrNullElements<T>(this Guard guard, Func<IEnumerable<T?>> expression)
        where T : struct
    {
        Guard.Against.NullOrEmpty(expression);

        if (expression().Any(element => !element.HasValue))
        {
            throw new ArgumentException("Value cannot contain null elements.", Guard.Expression.Parse(expression));
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

    /// <summary>
    /// Guard against positive argument values.
    /// </summary>
    /// <param name="guard">The guard clause.</param>
    /// <param name="expression">An expression returning the value to guard against.</param>
    [DebuggerStepThrough]
    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "May not be called.")]
    [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "guard", Justification = "By design.")]
    public static void Positive(this Guard guard, Func<int> expression)
    {
        Guard.Against.OutOfRange(expression, value => value <= 0, "Value has to be positive.");
    }

    /// <summary>
    /// Guard against positive or zero argument values.
    /// </summary>
    /// <param name="guard">The guard clause.</param>
    /// <param name="expression">An expression returning the value to guard against.</param>
    [DebuggerStepThrough]
    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "May not be called.")]
    [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "guard", Justification = "By design.")]
    public static void PositiveOrZero(this Guard guard, Func<int> expression)
    {
        Guard.Against.OutOfRange(expression, value => value < 0, "Value has to be positive or zero.");
    }

    /// <summary>
    /// Guard against positive argument values.
    /// </summary>
    /// <param name="guard">The guard clause.</param>
    /// <param name="expression">An expression returning the value to guard against.</param>
    [DebuggerStepThrough]
    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "May not be called.")]
    [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "guard", Justification = "By design.")]
    public static void Positive(this Guard guard, Func<long> expression)
    {
        Guard.Against.OutOfRange(expression, value => value <= 0, "Value has to be positive.");
    }

    /// <summary>
    /// Guard against positive or zero argument values.
    /// </summary>
    /// <param name="guard">The guard clause.</param>
    /// <param name="expression">An expression returning the value to guard against.</param>
    [DebuggerStepThrough]
    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "May not be called.")]
    [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "guard", Justification = "By design.")]
    public static void PositiveOrZero(this Guard guard, Func<long> expression)
    {
        Guard.Against.OutOfRange(expression, value => value < 0, "Value has to be positive or zero.");
    }

    /// <summary>
    /// Guard against negative argument values.
    /// </summary>
    /// <param name="guard">The guard clause.</param>
    /// <param name="expression">An expression returning the value to guard against.</param>
    [DebuggerStepThrough]
    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "May not be called.")]
    [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "guard", Justification = "By design.")]
    public static void Negative(this Guard guard, Func<int> expression)
    {
        Guard.Against.OutOfRange(expression, value => value >= 0, "Value has to be negative.");
    }

    /// <summary>
    /// Guard against negative or zero argument values.
    /// </summary>
    /// <param name="guard">The guard clause.</param>
    /// <param name="expression">An expression returning the value to guard against.</param>
    [DebuggerStepThrough]
    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "May not be called.")]
    [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "guard", Justification = "By design.")]
    public static void NegativeOrZero(this Guard guard, Func<int> expression)
    {
        Guard.Against.OutOfRange(expression, value => value > 0, "Value has to be negative or zero.");
    }

    /// <summary>
    /// Guard against negative argument values.
    /// </summary>
    /// <param name="guard">The guard clause.</param>
    /// <param name="expression">An expression returning the value to guard against.</param>
    [DebuggerStepThrough]
    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "May not be called.")]
    [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "guard", Justification = "By design.")]
    public static void Negative(this Guard guard, Func<long> expression)
    {
        Guard.Against.OutOfRange(expression, value => value >= 0, "Value has to be negative.");
    }

    /// <summary>
    /// Guard against negative or zero argument values.
    /// </summary>
    /// <param name="guard">The guard clause.</param>
    /// <param name="expression">An expression returning the value to guard against.</param>
    [DebuggerStepThrough]
    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "May not be called.")]
    [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "guard", Justification = "By design.")]
    public static void NegativeOrZero(this Guard guard, Func<long> expression)
    {
        Guard.Against.OutOfRange(expression, value => value > 0, "Value has to be negative or zero.");
    }

    [DebuggerStepThrough]
    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "May not be called.")]
    [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "guard", Justification = "By design.")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Private method.")]
    private static void OutOfRange<T>(this Guard guard, Func<T> expression, Func<T, bool> rangePredicate, string errorMessage)
    {
        var value = expression();

        if (rangePredicate(value))
        {
            throw new ArgumentOutOfRangeException(Guard.Expression.Parse(expression), value, errorMessage);
        }
    }
}