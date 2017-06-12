// <copyright file="GuardExpressionTests.cs" company="Guardian contributors">
//  Copyright (c) Guardian contributors. All rights reserved.
// </copyright>

namespace Guardian.Tests
{
    using System;
    using FluentAssertions;
    using Guardian.Tests.Extensions;
    using Guardian.Tests.Objects;
    using Xunit;

    public class GuardExpressionTests
    {
        [Fact]
        public void SupportedExpressionParsing()
        {
            var @class = default(Class);

            // single
            Guard.Expression.Parse(() => @class).Should().Be("class");

            // first-level members
            Guard.Expression.Parse(() => @class.ClassField).Should().Be("class.ClassField");
            Guard.Expression.Parse(() => @class.StructField.Class).Should().Be("class.StructField.Class");
            Guard.Expression.Parse(() => @class.NullableStructField).Should().Be("class.NullableStructField");
            Guard.Expression.Parse(() => @class.NullableStructField.Value.Class).Should().Be("class.NullableStructField.Value.Class");
            Guard.Expression.Parse(() => @class.ClassProperty).Should().Be("class.ClassProperty");
            Guard.Expression.Parse(() => @class.StructProperty.Class).Should().Be("class.StructProperty.Class");
            Guard.Expression.Parse(() => @class.NullableStructProperty).Should().Be("class.NullableStructProperty");
            Guard.Expression.Parse(() => @class.NullableStructProperty.Value.Class).Should().Be("class.NullableStructProperty.Value.Class");

            // second-level members (classField)
            Guard.Expression.Parse(() => @class.ClassField.ClassField).Should().Be("class.ClassField.ClassField");
            Guard.Expression.Parse(() => @class.ClassField.StructField.Class).Should().Be("class.ClassField.StructField.Class");
            Guard.Expression.Parse(() => @class.ClassField.NullableStructField).Should().Be("class.ClassField.NullableStructField");
            Guard.Expression.Parse(() => @class.ClassField.NullableStructField.Value.Class).Should().Be("class.ClassField.NullableStructField.Value.Class");
            Guard.Expression.Parse(() => @class.ClassField.ClassProperty).Should().Be("class.ClassField.ClassProperty");
            Guard.Expression.Parse(() => @class.ClassField.StructProperty.Class).Should().Be("class.ClassField.StructProperty.Class");
            Guard.Expression.Parse(() => @class.ClassField.NullableStructProperty).Should().Be("class.ClassField.NullableStructProperty");
            Guard.Expression.Parse(() => @class.ClassField.NullableStructProperty.Value.Class).Should().Be("class.ClassField.NullableStructProperty.Value.Class");

            // second-level members (structField.Class)
            Guard.Expression.Parse(() => @class.StructField.Class.ClassField).Should().Be("class.StructField.Class.ClassField");
            Guard.Expression.Parse(() => @class.StructField.Class.StructField.Class).Should().Be("class.StructField.Class.StructField.Class");
            Guard.Expression.Parse(() => @class.StructField.Class.NullableStructField).Should().Be("class.StructField.Class.NullableStructField");
            Guard.Expression.Parse(() => @class.StructField.Class.NullableStructField.Value.Class).Should().Be("class.StructField.Class.NullableStructField.Value.Class");
            Guard.Expression.Parse(() => @class.StructField.Class.ClassProperty).Should().Be("class.StructField.Class.ClassProperty");
            Guard.Expression.Parse(() => @class.StructField.Class.StructProperty.Class).Should().Be("class.StructField.Class.StructProperty.Class");
            Guard.Expression.Parse(() => @class.StructField.Class.NullableStructProperty).Should().Be("class.StructField.Class.NullableStructProperty");
            Guard.Expression.Parse(() => @class.StructField.Class.NullableStructProperty.Value.Class).Should().Be("class.StructField.Class.NullableStructProperty.Value.Class");

            // second-level members (nullableStructField.Value.Class)
            Guard.Expression.Parse(() => @class.NullableStructField.Value.Class.ClassField).Should().Be("class.NullableStructField.Value.Class.ClassField");
            Guard.Expression.Parse(() => @class.NullableStructField.Value.Class.StructField.Class).Should().Be("class.NullableStructField.Value.Class.StructField.Class");
            Guard.Expression.Parse(() => @class.NullableStructField.Value.Class.NullableStructField).Should().Be("class.NullableStructField.Value.Class.NullableStructField");
            Guard.Expression.Parse(() => @class.NullableStructField.Value.Class.NullableStructField.Value.Class).Should().Be("class.NullableStructField.Value.Class.NullableStructField.Value.Class");
            Guard.Expression.Parse(() => @class.NullableStructField.Value.Class.ClassProperty).Should().Be("class.NullableStructField.Value.Class.ClassProperty");
            Guard.Expression.Parse(() => @class.NullableStructField.Value.Class.StructProperty.Class).Should().Be("class.NullableStructField.Value.Class.StructProperty.Class");
            Guard.Expression.Parse(() => @class.NullableStructField.Value.Class.NullableStructProperty).Should().Be("class.NullableStructField.Value.Class.NullableStructProperty");
            Guard.Expression.Parse(() => @class.NullableStructField.Value.Class.NullableStructProperty.Value.Class).Should().Be("class.NullableStructField.Value.Class.NullableStructProperty.Value.Class");

            // second-level members (ClassProperty)
            Guard.Expression.Parse(() => @class.ClassProperty.ClassField).Should().Be("class.ClassProperty.ClassField");
            Guard.Expression.Parse(() => @class.ClassProperty.StructField.Class).Should().Be("class.ClassProperty.StructField.Class");
            Guard.Expression.Parse(() => @class.ClassProperty.NullableStructField).Should().Be("class.ClassProperty.NullableStructField");
            Guard.Expression.Parse(() => @class.ClassProperty.NullableStructField.Value.Class).Should().Be("class.ClassProperty.NullableStructField.Value.Class");
            Guard.Expression.Parse(() => @class.ClassProperty.ClassProperty).Should().Be("class.ClassProperty.ClassProperty");
            Guard.Expression.Parse(() => @class.ClassProperty.StructProperty.Class).Should().Be("class.ClassProperty.StructProperty.Class");
            Guard.Expression.Parse(() => @class.ClassProperty.NullableStructProperty).Should().Be("class.ClassProperty.NullableStructProperty");
            Guard.Expression.Parse(() => @class.ClassProperty.NullableStructProperty.Value.Class).Should().Be("class.ClassProperty.NullableStructProperty.Value.Class");

            // second-level members (StructProperty.Class)
            Guard.Expression.Parse(() => @class.StructProperty.Class.ClassField).Should().Be("class.StructProperty.Class.ClassField");
            Guard.Expression.Parse(() => @class.StructProperty.Class.StructField.Class).Should().Be("class.StructProperty.Class.StructField.Class");
            Guard.Expression.Parse(() => @class.StructProperty.Class.NullableStructField).Should().Be("class.StructProperty.Class.NullableStructField");
            Guard.Expression.Parse(() => @class.StructProperty.Class.NullableStructField.Value.Class).Should().Be("class.StructProperty.Class.NullableStructField.Value.Class");
            Guard.Expression.Parse(() => @class.StructProperty.Class.ClassProperty).Should().Be("class.StructProperty.Class.ClassProperty");
            Guard.Expression.Parse(() => @class.StructProperty.Class.StructProperty.Class).Should().Be("class.StructProperty.Class.StructProperty.Class");
            Guard.Expression.Parse(() => @class.StructProperty.Class.NullableStructProperty).Should().Be("class.StructProperty.Class.NullableStructProperty");
            Guard.Expression.Parse(() => @class.StructProperty.Class.NullableStructProperty.Value.Class).Should().Be("class.StructProperty.Class.NullableStructProperty.Value.Class");

            // second-level members (NullableStructProperty.Value.Class)
            Guard.Expression.Parse(() => @class.NullableStructProperty.Value.Class.ClassField).Should().Be("class.NullableStructProperty.Value.Class.ClassField");
            Guard.Expression.Parse(() => @class.NullableStructProperty.Value.Class.StructField.Class).Should().Be("class.NullableStructProperty.Value.Class.StructField.Class");
            Guard.Expression.Parse(() => @class.NullableStructProperty.Value.Class.NullableStructField).Should().Be("class.NullableStructProperty.Value.Class.NullableStructField");
            Guard.Expression.Parse(() => @class.NullableStructProperty.Value.Class.NullableStructField.Value.Class).Should().Be("class.NullableStructProperty.Value.Class.NullableStructField.Value.Class");
            Guard.Expression.Parse(() => @class.NullableStructProperty.Value.Class.ClassProperty).Should().Be("class.NullableStructProperty.Value.Class.ClassProperty");
            Guard.Expression.Parse(() => @class.NullableStructProperty.Value.Class.StructProperty.Class).Should().Be("class.NullableStructProperty.Value.Class.StructProperty.Class");
            Guard.Expression.Parse(() => @class.NullableStructProperty.Value.Class.NullableStructProperty).Should().Be("class.NullableStructProperty.Value.Class.NullableStructProperty");
            Guard.Expression.Parse(() => @class.NullableStructProperty.Value.Class.NullableStructProperty.Value.Class).Should().Be("class.NullableStructProperty.Value.Class.NullableStructProperty.Value.Class");

            // execute tests that have to be run from inside a generic type
            new GenericGuardExpressionTests<Class>().TestSupported<Class>(null, null);
        }

        [Fact]
        public void UnsupportedExpressionParsing()
        {
            // null check!
            Record.Exception(() => Guard.Expression.Parse((Func<object>)null))
                .ShouldBeValid<ArgumentNullException>()
                .WithParameter("expression");

            var closedGeneric = new GenericType<int>();

            // A generic method call that does not have any generic context.
            Guard.Expression.Parse(() => closedGeneric.Method<object>(42, new object())).Should().BeNull();

            // execute tests that have to be run from inside a generic type
            new GenericGuardExpressionTests<Class>().TestUnsupported<Class>(null, null);
        }

        // LINK (Cameron): http://msdn.microsoft.com/en-us/library/ms145421.aspx
        private class GenericGuardExpressionTests<T1> where T1 : Class
        {
            public void TestSupported<T2>(T1 parameter1, T2 parameter2) where T2 : Class
            {
                Guard.Expression.Parse(() => parameter1.ClassField).Should().Be("parameter1.ClassField");
                Guard.Expression.Parse(() => parameter1.StructField.Class).Should().Be("parameter1.StructField.Class");
                Guard.Expression.Parse(() => parameter1.NullableStructField).Should().Be("parameter1.NullableStructField");
                Guard.Expression.Parse(() => parameter1.NullableStructField.Value.Class).Should().Be("parameter1.NullableStructField.Value.Class");
                Guard.Expression.Parse(() => parameter1.ClassProperty).Should().Be("parameter1.ClassProperty");
                Guard.Expression.Parse(() => parameter1.StructProperty.Class).Should().Be("parameter1.StructProperty.Class");
                Guard.Expression.Parse(() => parameter1.NullableStructProperty).Should().Be("parameter1.NullableStructProperty");
                Guard.Expression.Parse(() => parameter1.NullableStructProperty.Value.Class).Should().Be("parameter1.NullableStructProperty.Value.Class");

                Guard.Expression.Parse(() => parameter2.ClassField).Should().Be("parameter2.ClassField");
                Guard.Expression.Parse(() => parameter2.StructField.Class).Should().Be("parameter2.StructField.Class");
                Guard.Expression.Parse(() => parameter2.NullableStructField).Should().Be("parameter2.NullableStructField");
                Guard.Expression.Parse(() => parameter2.NullableStructField.Value.Class).Should().Be("parameter2.NullableStructField.Value.Class");
                Guard.Expression.Parse(() => parameter2.ClassProperty).Should().Be("parameter2.ClassProperty");
                Guard.Expression.Parse(() => parameter2.StructProperty.Class).Should().Be("parameter2.StructProperty.Class");
                Guard.Expression.Parse(() => parameter2.NullableStructProperty).Should().Be("parameter2.NullableStructProperty");
                Guard.Expression.Parse(() => parameter2.NullableStructProperty.Value.Class).Should().Be("parameter2.NullableStructProperty.Value.Class");

                var nonGeneric = new NonGenericType();
                Guard.Expression.Parse(() => nonGeneric.Class.ClassField).Should().Be("nonGeneric.Class.ClassField");
                Guard.Expression.Parse(() => nonGeneric.Class.StructField.Class).Should().Be("nonGeneric.Class.StructField.Class");
                Guard.Expression.Parse(() => nonGeneric.Class.NullableStructField).Should().Be("nonGeneric.Class.NullableStructField");
                Guard.Expression.Parse(() => nonGeneric.Class.NullableStructField.Value.Class).Should().Be("nonGeneric.Class.NullableStructField.Value.Class");
                Guard.Expression.Parse(() => nonGeneric.Class.ClassProperty).Should().Be("nonGeneric.Class.ClassProperty");
                Guard.Expression.Parse(() => nonGeneric.Class.StructProperty.Class).Should().Be("nonGeneric.Class.StructProperty.Class");
                Guard.Expression.Parse(() => nonGeneric.Class.NullableStructProperty).Should().Be("nonGeneric.Class.NullableStructProperty");
                Guard.Expression.Parse(() => nonGeneric.Class.NullableStructProperty.Value.Class).Should().Be("nonGeneric.Class.NullableStructProperty.Value.Class");
                
                var closedGeneric = new GenericType<Class>();
                Guard.Expression.Parse(() => closedGeneric.Parameter.ClassField).Should().Be("closedGeneric.Parameter.ClassField");
                Guard.Expression.Parse(() => closedGeneric.Parameter.StructField.Class).Should().Be("closedGeneric.Parameter.StructField.Class");
                Guard.Expression.Parse(() => closedGeneric.Parameter.NullableStructField).Should().Be("closedGeneric.Parameter.NullableStructField");
                Guard.Expression.Parse(() => closedGeneric.Parameter.NullableStructField.Value.Class).Should().Be("closedGeneric.Parameter.NullableStructField.Value.Class");
                Guard.Expression.Parse(() => closedGeneric.Parameter.ClassProperty).Should().Be("closedGeneric.Parameter.ClassProperty");
                Guard.Expression.Parse(() => closedGeneric.Parameter.StructProperty.Class).Should().Be("closedGeneric.Parameter.StructProperty.Class");
                Guard.Expression.Parse(() => closedGeneric.Parameter.NullableStructProperty).Should().Be("closedGeneric.Parameter.NullableStructProperty");
                Guard.Expression.Parse(() => closedGeneric.Parameter.NullableStructProperty.Value.Class).Should().Be("closedGeneric.Parameter.NullableStructProperty.Value.Class");

                var openGeneric = new GenericType<T1>();
                Guard.Expression.Parse(() => openGeneric.Parameter.ClassField).Should().Be("openGeneric.Parameter.ClassField");
                Guard.Expression.Parse(() => openGeneric.Parameter.StructField.Class).Should().Be("openGeneric.Parameter.StructField.Class");
                Guard.Expression.Parse(() => openGeneric.Parameter.NullableStructField).Should().Be("openGeneric.Parameter.NullableStructField");
                Guard.Expression.Parse(() => openGeneric.Parameter.NullableStructField.Value.Class).Should().Be("openGeneric.Parameter.NullableStructField.Value.Class");
                Guard.Expression.Parse(() => openGeneric.Parameter.ClassProperty).Should().Be("openGeneric.Parameter.ClassProperty");
                Guard.Expression.Parse(() => openGeneric.Parameter.StructProperty.Class).Should().Be("openGeneric.Parameter.StructProperty.Class");
                Guard.Expression.Parse(() => openGeneric.Parameter.NullableStructProperty).Should().Be("openGeneric.Parameter.NullableStructProperty");
                Guard.Expression.Parse(() => openGeneric.Parameter.NullableStructProperty.Value.Class).Should().Be("openGeneric.Parameter.NullableStructProperty.Value.Class");
            }

            public void TestUnsupported<T2>(T1 parameter1, T2 parameter2) where T2 : Class
            {
                var nonGeneric = new NonGenericType();
                var closedGeneric = new GenericType<Class>();
                var openGeneric = new GenericType<T1>();

                // A generic method call that depends on its generic context, because it uses the type parameters 
                // of the enclosing generic type and the enclosing generic method.
                Guard.Expression.Parse(() => openGeneric.Method<T2>(parameter1, parameter2)).Should().BeNull();

                // A non-generic method call that depends on its generic context, because it uses the type parameter 
                // of the enclosing generic type.
                Guard.Expression.Parse(() => openGeneric.Method(parameter1)).Should().BeNull();

                // A generic method call that does not depend on its generic context, because it does not use type 
                // parameters of the enclosing generic type or method
                Guard.Expression.Parse(() => closedGeneric.Method<object>(null, new object())).Should().BeNull();

                // A non-generic method call that does not depend on its generic context, because it does not use 
                // the type parameters of the enclosing generic type or method
                Guard.Expression.Parse(() => nonGeneric.Method()).Should().BeNull();
            }
        }

        private class GenericType<T1>
        {
            public T1 Parameter { get; set; }

            public string Method<T2>(T1 param1, T2 param2)
            {
                return null;
            }

            public string Method(T1 param)
            {
                return null;
            }
        }

        private class NonGenericType
        {
            public Class Class { get; set; }

            public string Method()
            {
                return null;
            }
        }
    }
}
