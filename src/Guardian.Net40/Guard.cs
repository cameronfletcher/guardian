// <copyright file="Guard.cs" company="Guardian contributors">
//  Copyright (c) Guardian contributors. All rights reserved.
// </copyright>
// <summary>Guardian. Mostly of null values.</summary>

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1636:FileHeaderCopyrightTextMustMatch", Scope = "Module", Justification = "Content is valid.")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1641:FileHeaderCompanyNameTextMustMatch", Scope = "Module", Justification = "Content is valid.")]

#pragma warning disable 0436

// ReSharper disable CheckNamespace
// ReSharper disable ExpressionIsAlwaysNull
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable PossibleNullReferenceException
// ReSharper disable RedundantNameQualifier
// ReSharper disable UnusedMember.Global

/// <summary>
/// The <see cref="Guard"/> clause.
/// </summary>
[ExcludeFromCodeCoverage]
internal class Guard
{
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Private member.")]
    private static readonly Guard Instance = new Guard();

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Private member.")]
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
    /// <value>The <see cref="Guard"/> clause extensibility endpoint.</value>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1623:PropertySummaryDocumentationMustMatchAccessors", Justification = "Not here.")]
    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "May not be called.")]
    public static Guard Against
    {
        [DebuggerStepThrough]
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

        if (expression == null || !expression().HasValue)
        {
            throw GetException(expression);
        }
    }

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Private method.")]
    [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "May not be called.")]
    [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly", Justification = "By design.")]
    private static Exception GetException<T>(Func<T> expression)
    {
        var parameterName = expression == null ? Expression.Parse(() => expression) : Expression.Parse(expression);
        var exceptionType = parameterName == null || parameterName.Contains(".") ? typeof(ArgumentException) : typeof(ArgumentNullException);

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
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Private member.")]
        private static readonly Dictionary<short, OpCode> OpCodeLookup = typeof(OpCodes).GetFields(BindingFlags.Static | BindingFlags.Public)
            .Select(field => field.GetValue(null))
            .Cast<OpCode>()
            .ToDictionary(opCode => opCode.Value);

        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Private member.")]
        private static readonly OpCode[] OpCodeWhitelist = new[] { OpCodes.Constrained, OpCodes.Box, OpCodes.Ldstr };

        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Private member.")]
        private static readonly OpCode[] OpCodeBlacklist = typeof(OpCodes).GetFields(BindingFlags.Static | BindingFlags.Public)
            .Select(field => field.GetValue(null))
            .Cast<OpCode>()
            .Where(opCode => opCode.Name.StartsWith(OpCodes.Ldelem.Name, StringComparison.OrdinalIgnoreCase)) // all indexer calls
            .Union(new[] { OpCodes.Newobj })
            .ToArray();

        /// <summary>
        /// Converts the specified expression to its string representation.
        /// </summary>
        /// <typeparam name="T">The expression type.</typeparam>
        /// <param name="expression">The expression.</param>
        /// <returns>The string representation of the specified expression.</returns>
        [SuppressMessage("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode", Justification = "May not be called.")]
        [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Justification = "Not an issue in this instance.")]
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Not Hungarian notation.")]
        public static string Parse<T>(Func<T> expression)
        {
            if (expression == null)
            {
                throw GetException(expression);
            }

            if (expression.Target == null)
            {
                return null;
            }

            using (var memoryStream = new MemoryStream(expression.Method.GetMethodBody().GetILAsByteArray()))
            using (var binaryReader = new BinaryReader(memoryStream))
            {
                var memberNames = new Stack<string>();

                while (memoryStream.Position != memoryStream.Length)
                {
                    var opCode = GetOpCode(binaryReader);
                    var data = binaryReader.ReadBytes(GetOpCodeSize(opCode.OperandType));

                    if (OpCodeBlacklist.Contains(opCode))
                    {
                        return null;
                    }

                    if (OpCodeWhitelist.Contains(opCode) || data.Length <= 1)
                    {
                        continue;
                    }

                    var handle = BitConverter.ToInt32(data, 0);
                    var targetType = expression.Target.GetType();
                    var member = targetType.Module.ResolveMember(handle, targetType.GetGenericArguments(), new Type[0]);
                    if (member.MemberType == MemberTypes.Method &&
                        (((MethodInfo)member).GetParameters().Any() || !member.Name.StartsWith("get_", StringComparison.OrdinalIgnoreCase)))
                    {
                        return null; // not a property
                    }

                    memberNames.Push(member.MemberType == MemberTypes.Method ? member.Name.Substring(4) : member.Name);
                }

                return memberNames.Any() ? string.Join(".", memberNames.Reverse()) : null;
            }
        }

        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Not Hungarian notation.")]
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Private method.")]
        private static OpCode GetOpCode(BinaryReader binaryReader)
        {
            int opCodeValue;
            if (binaryReader.BaseStream.Position == binaryReader.BaseStream.Length - 1)
            {
                opCodeValue = binaryReader.ReadByte();
            }
            else
            {
                opCodeValue = binaryReader.ReadUInt16();
                if (OpCodes.Prefix1.Value != (opCodeValue & OpCodes.Prefix1.Value))
                {
                    opCodeValue &= 0xFF;
                    binaryReader.BaseStream.Position--;
                }
                else
                {
                    opCodeValue = ((0xFF00 & opCodeValue) >> 8) | ((0xFF & opCodeValue) << 8);
                }
            }

            return OpCodeLookup[(short)opCodeValue];
        }

        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Private method.")]
        private static int GetOpCodeSize(OperandType operandType)
        {
            switch (operandType)
            {
                case OperandType.ShortInlineBrTarget:
                case OperandType.ShortInlineI:
                case OperandType.ShortInlineVar:
                    return 1;
                case OperandType.InlineVar:
                    return 2;
                case OperandType.InlineBrTarget:
                case OperandType.InlineField:
                case OperandType.InlineI:
                case OperandType.InlineMethod:
                case OperandType.InlineSig:
                case OperandType.InlineString:
                case OperandType.InlineSwitch:
                case OperandType.InlineTok:
                case OperandType.InlineType:
                case OperandType.ShortInlineR:
                    return 4;
                case OperandType.InlineI8:
                case OperandType.InlineR:
                    return 8;
                default: /* OperandType.InlineNone */
                    return 0;
            }
        }
    }
}