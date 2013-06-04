﻿// <copyright file="Guard.cs" company="Guardian contributors">
//  Copyright (c) Guardian contributors. All rights reserved.
// </copyright>
// <summary>Guardian. Mostly of null values.</summary>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1636:FileHeaderCopyrightTextMustMatch", Scope = "Module", Justification = "Content is valid.")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1641:FileHeaderCompanyNameTextMustMatch", Scope = "Module", Justification = "Content is valid.")]

// ReSharper disable CheckNamespace
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedParameter.Global

/// <summary>
/// The <see cref="Guard"/> clause.
/// </summary>
internal class Guard
{
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Private member.")]
    private static readonly Guard Instance = new Guard();

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Private member, also.")]
    private static readonly Dictionary<Type, Func<string, string, ArgumentException>> ExceptionFactories =
        new Dictionary<Type, Func<string, string, ArgumentException>>
        {
            { typeof(ArgumentException), (message, parameterName) => new ArgumentException(message, parameterName) },
            { typeof(ArgumentNullException), (message, parameterName) => new ArgumentNullException(parameterName, message) },
        };

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Private constructor.")]
    private Guard()
    {
    }

    /// <summary>
    /// Provides instance and extension methods for the <see cref="Guard"/> clause.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "Not here.")]
    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "May not be called.")]
    public static Guard Against
    {
        get { return Instance; }
    }

    /// <summary>
    /// Guard against null argument values.
    /// </summary>
    /// <typeparam name="T">The type of value to guard against.</typeparam>
    /// <param name="expression">An expression returning the value to guard against.</param>
    [DebuggerStepThrough]
    [Obsolete("This method is not fully supported in portable class libraries. Consider using Guard.Against.Null(value, parameterName) instead.")]
    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "May not be called.")]
    [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "By design.")]
    public void Null<T>(Func<T> expression)
        where T : class
    {
        Guard.Against.NullUsing<ArgumentNullException>(expression, "expression");
        Guard.Against.NullUsing<ArgumentNullException>(expression(), null);
    }

    /// <summary>
    /// Guard against null argument values.
    /// </summary>
    /// <typeparam name="T">The type of value to guard against.</typeparam>
    /// <param name="expression">An expression returning the value to guard against.</param>
    [DebuggerStepThrough]
    [Obsolete("This method is not fully supported in portable class libraries. Consider using Guard.Against.Null(value, parameterName) instead.")]
    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "May not be called.")]
    [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "By design.")]
    public void Null<T>(Func<T?> expression)
        where T : struct
    {
        Guard.Against.NullUsing<ArgumentNullException>(expression, "expression");
        Guard.Against.NullUsing<ArgumentNullException>(expression(), null);
    }

    /// <summary>
    /// Guard against null argument values.
    /// </summary>
    /// <typeparam name="T">The type of value to guard against.</typeparam>
    /// <param name="value">The value.</param>
    /// <param name="parameterName">Name of the parameter.</param>
    [DebuggerStepThrough]
    [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "By design.")]
    public void Null<T>(T value, string parameterName)
        where T : class
    {
        Guard.Against.NullUsing<ArgumentNullException>(parameterName, "parameterName");
        Guard.Against.NullUsing<ArgumentNullException>(value, parameterName);
    }

    /// <summary>
    /// Guard against null argument values.
    /// </summary>
    /// <typeparam name="T">The type of value to guard against.</typeparam>
    /// <param name="value">The value.</param>
    /// <param name="parameterName">Name of the parameter.</param>
    /// <param name="propertyName">Name of the property.</param>
    [DebuggerStepThrough]
    [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "By design.")]
    public void Null<T>(T value, string parameterName, string propertyName)
        where T : class
    {
        Guard.Against.NullUsing<ArgumentNullException>(parameterName, "parameterName");
        Guard.Against.NullUsing<ArgumentNullException>(propertyName, "propertyName");
        Guard.Against.NullUsing<ArgumentException>(value, string.Concat(parameterName, ".", propertyName));
    }

    /// <summary>
    /// Guard against null argument values.
    /// </summary>
    /// <typeparam name="T">The type of value to guard against.</typeparam>
    /// <param name="value">The value.</param>
    /// <param name="parameterName">Name of the parameter.</param>
    [DebuggerStepThrough]
    [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "By design.")]
    public void Null<T>(T? value, string parameterName)
        where T : struct
    {
        Guard.Against.NullUsing<ArgumentNullException>(parameterName, "parameterName");
        Guard.Against.NullUsing<ArgumentNullException>(value, parameterName);
    }

    /// <summary>
    /// Guard against null argument values.
    /// </summary>
    /// <typeparam name="T">The type of value to guard against.</typeparam>
    /// <param name="value">The value.</param>
    /// <param name="parameterName">Name of the parameter.</param>
    /// <param name="propertyName">Name of the property.</param>
    [DebuggerStepThrough]
    [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "By design.")]
    public void Null<T>(T? value, string parameterName, string propertyName)
        where T : struct
    {
        Guard.Against.NullUsing<ArgumentNullException>(parameterName, "parameterName");
        Guard.Against.NullUsing<ArgumentNullException>(propertyName, "propertyName");
        Guard.Against.NullUsing<ArgumentException>(value, string.Concat(parameterName, ".", propertyName));
    }

    [DebuggerStepThrough]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Private method.")]
    [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "By design.")]
    private void NullUsing<T>(object value, string parameterName)
        where T : Exception
    {
        if (value == null)
        {
            throw ExceptionFactories[typeof(T)].Invoke("Value cannot be null.", parameterName);
        }
    }

    /// <summary>
    /// Provides expression helper methods for the <see cref="Guard"/> clause.
    /// </summary>
    [Obsolete("This is not supported in portable class libraries.")]
    public static class Expression
    {
        /// <summary>
        /// Converts the specified expression to its string representation.
        /// </summary>
        /// <typeparam name="T">The expression type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns>The string representation of the specified expression.</returns>
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "May not be called.")]
        [SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "expression", Justification = "Maintaining interface.")]
        public static string Parse<T>(Func<T> expression)
        {
            return null; // unable to parse in portable class library
        }
    }
}