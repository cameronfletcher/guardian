// <copyright file="UnsupportedGuardClauses.cs" company="Guardian contributors">
//  Copyright (c) Guardian contributors. All rights reserved.
// </copyright>

namespace Guardian.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class UnsupportedGuardClauses
    {
        [Fact]
        public void DefaultExpression()
        {
            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => default(object)));

            // assert
            AssertValid(exception);
        }

        [Fact]
        public void BlockExpression()
        {
            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => { return (object)null; }));

            // assert
            AssertValid(exception);
        }

        [Fact]
        public void ConditionalExpression()
        {
            // act
#pragma warning disable 429
            var exception = Record.Exception(() => Guard.Against.Null(() => 10 > 2 ? null : string.Empty));
#pragma warning restore 429

            // assert
            AssertValid(exception);
        }

        [Fact]
        public void ConstantExpression()
        {
            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => (object)null));

            // assert
            AssertValid(exception);
        }

        [Fact]
        public void ObjectArray()
        {
            // arrange
            var array = new object[] { new object(), null };

            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => array[1]));

            // assert
            AssertValid(exception);
        }

        [Fact]
        public void ObjectList()
        {
            // arrange
            var list = new List<object>(new object[] { new object(), null });

            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => list[1]));

            // assert
            AssertValid(exception);
        }

        [Fact]
        public void ObjectDictionary()
        {
            // arrange
            var dictionary = new Dictionary<string, string> { { "key", null } };

            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => dictionary["key"]));

            // assert
            AssertValid(exception);
        }

        [Fact]
        public void ValueArray()
        {
            // arrange
            var array = new int?[] { 1, null };

            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => array[1]));

            // assert
            AssertValid(exception);
        }

        [Fact]
        public void ValueList()
        {
            // arrange
            var list = new List<int?>(new int?[] { 1, null });

            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => list[1]));

            // assert
            AssertValid(exception);
        }

        [Fact]
        public void ValueDictionary()
        {
            // arrange
            var dictionary = new Dictionary<int?, string> { { 2, null } };

            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => dictionary[2]));

            // assert
            AssertValid(exception);
        }

        [Fact]
        public void MethodTest()
        {
            // arrange
            var thing = new Thing();

            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => thing.Method()));

            // assert
            AssertValid(exception);
        }

        [Fact]
        public void NestedMethodTest()
        {
            // arrange
            var thing = new Thing();

            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => thing.NestedThing.Method()));

            // assert
            AssertValid(exception);
        }

        [Fact]
        public void ExtensionMethod()
        {
            // arrange
            var array = new string[] { };

            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => array.FirstOrDefault()));

            // assert
            AssertValid(exception);
        }

        [Fact]
        public void MethodWithArguments()
        {
            // arrange
            var parameter = new Thing();

            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => parameter.Method("argument")));

            // assert
            AssertValid(exception);
        }

        private static void AssertValid(Exception exception)
        {
#if GuardLoose
            exception.ShouldBeValid<ArgumentException>();
#else
            exception.ShouldBeValid<NotSupportedException>();
#endif
        }
    }
}