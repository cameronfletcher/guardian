// <copyright file="GuardExtensionsTests.cs" company="Guardian contributors">
//  Copyright (c) Guardian contributors. All rights reserved.
// </copyright>

namespace Guardian.Tests
{
    using System;
    using System.Collections.Generic;
    using Xunit;

    public class GuardExtensionsTests
    {
        [Fact]
        public void NullEnumeration()
        {
            // act
            var exception = Record.Exception(() => Guard.Against.NullOrEmpty((Func<IEnumerable<object>>)null));

            // assert
            exception.ShouldBeValid<ArgumentNullException>().WithParameter("expression");
        }

        [Fact]
        public void NullValueEnumeration()
        {
            // arrange
            var enumeration = (object[])null;

            // act
            var exception = Record.Exception(() => Guard.Against.NullOrEmpty(() => enumeration));

            // assert
            exception.ShouldBeValid<ArgumentNullException>().WithParameter("enumeration");
        }
        
        [Fact]
        public void EmptyValueEnumeration()
        {
            // arrange
            var enumeration = string.Empty; // IEnumerable<char>

            // act
            var exception = Record.Exception(() => Guard.Against.NullOrEmpty(() => enumeration));

            // assert
            exception.ShouldBeValid<ArgumentException>(ExceptionType.Empty).WithParameter("enumeration");
        }

        [Fact]
        public void EmptyObjectEnumeration()
        {
            // arrange
            var enumeration = new object[0];

            // act
            var exception = Record.Exception(() => Guard.Against.NullOrEmpty(() => enumeration));

            // assert
            exception.ShouldBeValid<ArgumentException>(ExceptionType.Empty).WithParameter("enumeration");
        }

        [Fact]
        public void NestedObjectEnumeration()
        {
            // arrange
            var thing = new Thing();

            // act
            var exception = Record.Exception(() => Guard.Against.NullOrEmpty(() => thing.EmptyArray));

            // assert
            exception.ShouldBeValid<ArgumentException>(ExceptionType.Empty).WithParameter("thing.EmptyArray");
        }

        [Fact]
        public void BlockExpressionEnumeration()
        {
            // arrange
            var thing = new Thing();

            // act
            var exception = Record.Exception(() => Guard.Against.NullOrEmpty(() => { return "hello"; }));

            // assert
            exception.ShouldBeStrictNull();
        }

        [Fact]
        public void NonNullEnumeration()
        {
            // arrange
            var thing = new Thing();

            // act
            var exception = Record.Exception(() => Guard.Against.NullOrEmpty(() => thing.ThingArray[1].ThingArray));

            // assert
            exception.ShouldBeStrictNull();
        }
    }
}