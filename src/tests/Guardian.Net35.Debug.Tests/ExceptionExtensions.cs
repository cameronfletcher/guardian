// <copyright file="ExceptionExtensions.cs" company="Guardian contributors">
//  Copyright (c) Guardian contributors. All rights reserved.
// </copyright>

namespace Guardian.Tests
{
    using System;
    using FluentAssertions;

    internal enum ExceptionType
    {
        Null = 0,
        Empty = 1
    }

    internal static class ExceptionExtensions
    {
        // TODO (Cameron): Add reasons to assertions.
        public static T ShouldBeValid<T>(this Exception exception, ExceptionType type = ExceptionType.Null) where T : Exception
        {
            exception.Should().NotBeNull("because a null exception is invalid");
            exception.Should().BeOfType<T>();

            if (typeof(ArgumentException).IsAssignableFrom(typeof(T)))
            {
                exception.Message.Should().StartWith(type == ExceptionType.Null ? "Value cannot be null." : "Value cannot be empty.");
            }

            if (typeof(NotSupportedException).IsAssignableFrom(typeof(T)))
            {
                exception.Message.Should().StartWith("The expression used in the Guard clause is not supported.");
            }
            
            return exception as T;
        }

        public static void ShouldBeStrictArgumentNullException(this Exception exception)
        {
#if GUARD_STRICT
            exception.ShouldBeValid<NotSupportedException>();
#else
            exception.ShouldBeValid<ArgumentNullException>().WithUnknownParameter();
#endif
        }

        public static void ShouldBeStrictNull(this Exception exception)
        {
#if GUARD_STRICT
            exception.ShouldBeValid<NotSupportedException>();
#else
            exception.Should().BeNull();
#endif
        }

        public static void WithParameter(this ArgumentException exception, string parameterName)
        {
            exception.ParamName.Should().Be(parameterName);
        }

        public static void WithUnknownParameter(this ArgumentException exception)
        {
            exception.ParamName.Should().Be(Guard.Expression.Unknown);
        }
    }
}