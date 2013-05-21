﻿// <copyright file="GuardTests.cs" company="Guardian contributors">
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
            exception.ShouldBeValid<ArgumentNullException>().WithParameter("expression");
        }

        [Fact]
        public void ParameterTest()
        {
            // arrange
            var thing = (Thing)null;

            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => thing));

            // assert
            exception.ShouldBeValid<ArgumentNullException>().WithParameter("thing");
        }

        [Fact]
        public void FieldTest()
        {
            // arrange
            var thing = new Thing();

            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => thing.field));

            // assert
            exception.ShouldBeValid<ArgumentException>().WithParameter("thing.field");
        }

        [Fact]
        public void PropertyTest()
        {
            // arrange
            var thing = new Thing();

            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => thing.Property));

            // assert
            exception.ShouldBeValid<ArgumentException>().WithParameter("thing.Property");
        }

        [Fact]
        public void NestedFieldTest()
        {
            // arrange
            var thing = new Thing();

            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => thing.NestedThing.field));

            // assert
            exception.ShouldBeValid<ArgumentException>().WithParameter("thing.NestedThing.field");
        }

        [Fact]
        public void NestedPropertyTest()
        {
            // arrange
            var thing = new Thing();

            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => thing.NestedThing.Property));

            // assert
            exception.ShouldBeValid<ArgumentException>().WithParameter("thing.NestedThing.Property");
        }

        [Fact]
        public void DefaultExpression()
        {
            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => default(object)));

            // assert
            exception.ShouldBeStrictArgumentNullException();
        }

        [Fact]
        public void BlockExpression()
        {
            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => { return (object)null; }));

            // assert
            exception.ShouldBeStrictArgumentNullException();
        }

        [Fact]
        public void ConditionalExpression()
        {
            // act
#pragma warning disable 429
            var exception = Record.Exception(() => Guard.Against.Null(() => 10 > 2 ? null : string.Empty));
#pragma warning restore 429

            // assert
            exception.ShouldBeStrictArgumentNullException();
        }

        [Fact]
        public void ConstantExpression()
        {
            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => (object)null));

            // assert
            exception.ShouldBeStrictArgumentNullException();
        }

        [Fact]
        public void ObjectArray()
        {
            // arrange
            var array = new object[] { new object(), null };

            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => array[1]));

            // assert
            exception.ShouldBeStrictArgumentNullException();
        }

        [Fact]
        public void ObjectList()
        {
            // arrange
            var list = new List<object>(new object[] { new object(), null });

            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => list[1]));

            // assert
            exception.ShouldBeStrictArgumentNullException();
        }

        [Fact]
        public void ObjectDictionary()
        {
            // arrange
            var dictionary = new Dictionary<string, string> { { "key", null } };

            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => dictionary["key"]));

            // assert
            exception.ShouldBeStrictArgumentNullException();
        }

        [Fact]
        public void ValueArray()
        {
            // arrange
            var array = new int?[] { 1, null };

            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => array[1]));

            // assert
            exception.ShouldBeStrictArgumentNullException();
        }

        [Fact]
        public void ValueList()
        {
            // arrange
            var list = new List<int?>(new int?[] { 1, null });

            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => list[1]));

            // assert
            exception.ShouldBeStrictArgumentNullException();
        }

        [Fact]
        public void ValueDictionary()
        {
            // arrange
            var dictionary = new Dictionary<int?, string> { { 2, null } };

            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => dictionary[2]));

            // assert
            exception.ShouldBeStrictArgumentNullException();
        }

        [Fact]
        public void MethodTest()
        {
            // arrange
            var thing = new Thing();

            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => thing.Method()));

            // assert
            exception.ShouldBeStrictArgumentNullException();
        }

        [Fact]
        public void NestedMethodTest()
        {
            // arrange
            var thing = new Thing();

            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => thing.NestedThing.Method()));

            // assert
            exception.ShouldBeStrictArgumentNullException();
        }

        [Fact]
        public void ExtensionMethod()
        {
            // arrange
            var array = new string[] { };

            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => array.FirstOrDefault()));

            // assert
            exception.ShouldBeStrictArgumentNullException();
        }

        [Fact]
        public void MethodWithArguments()
        {
            // arrange
            var parameter = new Thing();

            // act
            var exception = Record.Exception(() => Guard.Against.Null(() => parameter.Method("argument")));

            // assert
            exception.ShouldBeStrictArgumentNullException();
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
    }
}