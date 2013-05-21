// <copyright file="GuardExtensions.cs" company="Guardian contributors">
//  Copyright (c) Guardian contributors. All rights reserved.
// </copyright>
// <summary>Guardian. Mostly of null values.</summary>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Extension method class.")]
internal static class GuardExtensions
{
    /// <summary>
    /// Guard against empty argument values.
    /// </summary>
    /// <typeparam name="T">The type of value to guard against.</typeparam>
    /// <param name="guard">The Guard class.</param>
    /// <param name="expression">An expression returning the value to guard against.</param>
    [DebuggerStepThrough]
    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "May not be called.")]
    [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "guard", Justification = "By design.")]
    public static void Empty<T>(this Guard guard, Func<IEnumerable<T>> expression)
    {
        Guard.Against.Null(expression);

        if (!expression().Any())
        {
            throw new ArgumentException("Value cannot be empty.", Guard.Expression.Parse(expression) ?? Guard.Expression.Unknown);
        }
    }
}