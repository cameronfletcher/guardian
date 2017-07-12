// <copyright file="ExceptionExtensions.cs" company="Guardian contributors">
//  Copyright (c) Guardian contributors. All rights reserved.
// </copyright>

namespace Guardian.Tests.Sdk
{
    using System;
    using System.Reflection;
    using FluentAssertions;

    internal enum ExceptionType
    {
        Null = 0,
        Empty,
        Negative,
        NegativeOrZero,
        Positive,
        PositiveOrZero,
        RelativeUri,
        AbsoluteUri
    }

    internal static class ExceptionExtensions
    {
        // TODO (Cameron): Add reasons to assertions.
        public static T ShouldBeValid<T>(this Exception exception, ExceptionType type = ExceptionType.Null) where T : Exception
        {
            exception.Should().NotBeNull("because a null exception is invalid");
            exception.Should().BeOfType<T>();

            if (typeof(ArgumentOutOfRangeException).IsAssignableFrom(typeof(T)))
            {
                var expectedMessage = default(string);

                switch (type)
                {
                    case ExceptionType.Negative:
                        expectedMessage = "Value cannot be negative.";
                        break;

                    case ExceptionType.NegativeOrZero:
                        expectedMessage = "Value cannot be negative or zero.";
                        break;

                    case ExceptionType.Positive:
                        expectedMessage = "Value cannot be positive.";
                        break;

                    case ExceptionType.PositiveOrZero:
                        expectedMessage = "Value cannot be positive or zero.";
                        break;

                    default:
                        expectedMessage = "Should not get here: debug test";
                        break;
                }

                exception.Message.Should().StartWith(expectedMessage);
            }
            else if (typeof(ArgumentException).IsAssignableFrom(typeof(T)))
            {
                var expectedMessage = default(string);

                switch (type)
                {
                    case ExceptionType.Null:
                        expectedMessage = "Value cannot be null.";
                        break;

                    case ExceptionType.Empty:
                        expectedMessage = "Value cannot be empty.";
                        break;

                    case ExceptionType.RelativeUri:
                        expectedMessage = "Value cannot be a relative URI.";
                        break;

                    case ExceptionType.AbsoluteUri:
                        expectedMessage = "Value cannot be an absolute URI.";
                        break;

                    default:
                        expectedMessage = "Should not get here: debug test";
                        break;
                }

                exception.Message.Should().StartWith(expectedMessage);
            }

            if (typeof(NotSupportedException).GetTypeInfo().IsAssignableFrom(typeof(T).GetTypeInfo()))
            {
                exception.Message.Should().StartWith("The expression used in the Guard clause is not supported.");
            }

            return exception as T;
        }

        public static void ShouldBeStrictArgumentException(this Exception exception)
        {
#if GUARD_STRICT
            exception.ShouldBeValid<NotSupportedException>();
#else
            exception.ShouldBeValid<ArgumentException>().WithUnknownParameter();
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

        public static ArgumentOutOfRangeException WithActualValue(this ArgumentOutOfRangeException exception, object actualValue)
        {
            exception.ActualValue.Should().Be(actualValue);

            return exception;
        }

        public static void WithUnknownParameter(this ArgumentException exception)
        {
            exception.ParamName.Should().BeNull();
        }
    }
}