// <copyright file="GuardTests.cs" company="Guardian contributors">
//  Copyright (c) Guardian contributors. All rights reserved.
// </copyright>

namespace Guardian.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class GuardTests
    {
        [Fact]
        public void NullExpression()
        {
            // act
            var exception = Record.Exception(() => Guard.Against.Null((Func<object>)null));

            // assert
#if PCL
            exception.ShouldBeValid<ArgumentNullException>().WithParameter("expression");
#else
            exception.ShouldBeValid<ArgumentNullException>().WithParameter("expression");
#endif
        }

        [Fact]
        public void ParameterTest()
        {
            // arrange
            var thing = (Thing)null;

            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => thing));

            // assert
#if PCL
            exception.ShouldBeValid<ArgumentException>().WithUnknownParameter();
#else
            exception.ShouldBeValid<ArgumentNullException>().WithParameter("thing");
#endif
        }

        [Fact]
        public void FieldTest()
        {
            // arrange
            var thing = new Thing();

            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => thing.field));

            // assert
#if PCL
            exception.ShouldBeValid<ArgumentException>().WithUnknownParameter();
#else
            exception.ShouldBeValid<ArgumentException>().WithParameter("thing.field");
#endif
        }

        [Fact]
        public void PropertyTest()
        {
            // arrange
            var thing = new Thing();

            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => thing.Property));

            // assert
#if PCL
            exception.ShouldBeValid<ArgumentException>().WithUnknownParameter();
#else
            exception.ShouldBeValid<ArgumentException>().WithParameter("thing.Property");
#endif
        }

        [Fact]
        public void NestedFieldTest()
        {
            // arrange
            var thing = new Thing();

            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => thing.NestedThing.field));

            // assert
#if PCL
            exception.ShouldBeValid<ArgumentException>().WithUnknownParameter();
#else
            exception.ShouldBeValid<ArgumentException>().WithParameter("thing.NestedThing.field");
#endif
        }

        [Fact]
        public void NestedPropertyTest()
        {
            // arrange
            var thing = new Thing();

            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => thing.NestedThing.Property));

            // assert
#if PCL
            exception.ShouldBeValid<ArgumentException>().WithUnknownParameter();
#else
            exception.ShouldBeValid<ArgumentException>().WithParameter("thing.NestedThing.Property");
#endif
        }

        [Fact]
        public void DefaultExpression()
        {
            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => default(object)));

            // assert
            exception.ShouldBeStrictArgumentException();
        }

        [Fact]
        public void BlockExpression()
        {
            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => { return (object)null; }));

            // assert
            exception.ShouldBeStrictArgumentException();
        }

        [Fact]
        public void ConditionalExpression()
        {
            // act
#pragma warning disable 429
            var exception = Record.Exception(() => Guard.Against.Null(() => 10 > 2 ? null : string.Empty));
#pragma warning restore 429

            // assert
            exception.ShouldBeStrictArgumentException();
        }

        [Fact]
        public void ConstantExpression()
        {
            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => (object)null));

            // assert
            exception.ShouldBeStrictArgumentException();
        }

        [Fact]
        public void ObjectArray()
        {
            // arrange
            var array = new object[] { new object(), null };

            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => array[1]));

            // assert
            exception.ShouldBeStrictArgumentException();
        }

        [Fact]
        public void ObjectList()
        {
            // arrange
            var list = new List<object>(new object[] { new object(), null });

            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => list[1]));

            // assert
            exception.ShouldBeStrictArgumentException();
        }

        [Fact]
        public void ObjectDictionary()
        {
            // arrange
            var dictionary = new Dictionary<string, string> { { "key", null } };

            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => dictionary["key"]));

            // assert
            exception.ShouldBeStrictArgumentException();
        }

        [Fact]
        public void ValueArray()
        {
            // arrange
            var array = new int?[] { 1, null };

            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => array[1]));

            // assert
            exception.ShouldBeStrictArgumentException();
        }

        [Fact]
        public void ValueList()
        {
            // arrange
            var list = new List<int?>(new int?[] { 1, null });

            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => list[1]));

            // assert
            exception.ShouldBeStrictArgumentException();
        }

        [Fact]
        public void ValueDictionary()
        {
            // arrange
            var dictionary = new Dictionary<int?, string> { { 2, null } };

            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => dictionary[2]));

            // assert
            exception.ShouldBeStrictArgumentException();
        }

        [Fact]
        public void MethodTest()
        {
            // arrange
            var thing = new Thing();

            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => thing.Method()));

            // assert
            exception.ShouldBeStrictArgumentException();
        }

        [Fact]
        public void NestedMethodTest()
        {
            // arrange
            var thing = new Thing();

            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => thing.NestedThing.Method()));

            // assert
            exception.ShouldBeStrictArgumentException();
        }

        [Fact]
        public void ExtensionMethod()
        {
            // arrange
            var array = new string[] { };

            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => array.FirstOrDefault()));

            // assert
            exception.ShouldBeStrictArgumentException();
        }

        [Fact]
        public void MethodWithArguments()
        {
            // arrange
            var parameter = new Thing();

            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => parameter.Method("argument")));

            // assert
            exception.ShouldBeStrictArgumentException();
        }

        [Fact]
        public void NonNullExpression()
        {
            // arrange
            var dictionary = new Dictionary<int?, string> { { 2, string.Empty } };

            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => dictionary[2]));

            // assert
            exception.ShouldBeStrictNull();
        }

        // LINK (Cameron): http://msdn.microsoft.com/en-us/library/ms145421.aspx
        [Fact]
        public void AllTheGenericExpressions()
        {
            // arrange
            var generic = new GenericType2<int>();

            // act (and assert)
            generic.GenericMethod<string>(1, "hello");
        }

        private class GenericType1<Tg1>
        {
            public string GenericMethod<Tgm1>(Tg1 param1, Tgm1 param2)
            {
                return null;
            }

            public string NonGenericMethod(Tg1 param)
            {
                return null;
            }
        }

        private class GenericType2<Tg2>
        {
            public void GenericMethod<Tgm2>(Tg2 param1, Tgm2 param2)
            {
                var openGeneric = new GenericType1<Tg2>();
                var closedGeneric = new GenericType1<int>();
                var nonGeneric = new NonGenericType();

                Guard.Expression.Parse(() => openGeneric.GenericMethod<Tgm2>(param1, param2));
                Guard.Expression.Parse(() => openGeneric.NonGenericMethod(param1));
                Guard.Expression.Parse(() => closedGeneric.GenericMethod<object>(42, new object()));
                Guard.Expression.Parse(() => nonGeneric.NonGenericMethod());

                nonGeneric.NonGenericMethod();
            }
        }

        private class NonGenericType
        {
            public string NonGenericMethod()
            {
                var closedGeneric = new GenericType1<int>();
                Guard.Expression.Parse(() => closedGeneric.GenericMethod<object>(42, new object()));

                return null;
            }
        }
    }
}