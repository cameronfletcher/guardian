// <copyright file="Guard.cs" company="Guardian contributors">
//  Copyright (c) Guardian contributors. All rights reserved.
// </copyright>
// <summary>Guardian. Mostly of null values.</summary>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.Emit;

[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1636:FileHeaderCopyrightTextMustMatch", Scope = "Module", Justification = "Content is valid.")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1641:FileHeaderCompanyNameTextMustMatch", Scope = "Module", Justification = "Content is valid.")]

// ReSharper disable CheckNamespace
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable PossibleNullReferenceException
// ReSharper disable RedundantNameQualifier

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
    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "May not be called.")]
    [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "By design.")]
    public void Null<T>(Func<T> expression)
        where T : class
    {
        Guard.Against.Invalid(expression);

        if (expression == null || expression() == null)
        {
            throw GetException(expression);
        }
    }

    /// <summary>
    /// Guard against null argument values.
    /// </summary>
    /// <typeparam name="T">The type of value to guard against.</typeparam>
    /// <param name="expression">An expression returning the value to guard against.</param>
    [DebuggerStepThrough]
    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "May not be called.")]
    [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "By design.")]
    public void Null<T>(Func<T?> expression)
        where T : struct
    {
        Guard.Against.Invalid(expression);

        if (expression == null || expression() == null)
        {
            throw GetException(expression);
        }
    }

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Private method.")]
    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "May not be called.")]
    [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly", Justification = "By design.")]
    private static Exception GetException<T>(Func<T> expression)
    {
        var parameterName = expression == null ? "expression" : Expression.Parse(expression);
        var exceptionType = parameterName == null || !parameterName.Contains(".")
            ? typeof(ArgumentNullException)
            : typeof(ArgumentException);

        return ExceptionFactories[exceptionType].Invoke("Value cannot be null.", parameterName);
    }

    [Conditional("GUARD_STRICT")]
    [DebuggerStepThrough]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Private method.")]
    [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Justification = "By design.")]
    private void Invalid<T>(Func<T> expression)
    {
        if (expression != null && Expression.Parse(expression) == null)
        {
            throw new NotSupportedException("The expression used in the Guard clause is not supported.");
        }
    }

    /// <summary>
    /// Provides expression helper methods for the <see cref="Guard"/> clause.
    /// </summary>
    public static class Expression
    {
        /// <summary>
        /// Converts the specified expression to its string representation.
        /// </summary>
        /// <typeparam name="T">The expression type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns>The string representation of the specified expression.</returns>
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "May not be called.")]
        public static string Parse<T>(Func<T> expression)
        {
            var il = expression.Method.GetMethodBody().GetILAsByteArray();

            if (il[0] != (byte)OpCodes.Ldarg_0.Value || il[1] != (byte)OpCodes.Ldfld.Value)
            {
                return null;
            }

            var memberNames = new Stack<string>();

            for (var @byte = 1; @byte < il.Length; @byte = @byte + 5)
            {
                if (il[@byte] == (byte)OpCodes.Stloc_0.Value || il[@byte] == (byte)OpCodes.Ret.Value)
                {
                    break;
                }

                if (il[@byte] == (byte)OpCodes.Ldfld.Value)
                {
                    var handle = BitConverter.ToInt32(il, @byte + 1);
                    var member = expression.Target.GetType().Module.ResolveMember(handle);
                    memberNames.Push(member.Name);
                    continue;
                }

                if (il[@byte] == (byte)OpCodes.Callvirt.Value || il[@byte] == (byte)OpCodes.Call.Value)
                {
                    var handle = BitConverter.ToInt32(il, @byte + 1);
                    var method = expression.Target.GetType().Module.ResolveMethod(handle);
                    if (!method.Name.StartsWith("get_", StringComparison.OrdinalIgnoreCase))
                    {
                        return null;
                    }

                    memberNames.Push(method.Name.Substring(4));
                    continue;
                }

                return null; // unrecognised OpCode
            }

            return string.Join(".", memberNames.Reverse().ToArray());
        }
    }
}