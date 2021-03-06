﻿// <copyright file="GuardExtensionsTests.cs" company="Guardian contributors">
//  Copyright (c) Guardian contributors. All rights reserved.
// </copyright>

namespace Guardian.Tests
{
    using System;
    using System.Collections.Generic;
    using FluentAssertions;
    using Guardian.Tests.Extensions;
    using Guardian.Tests.Objects;
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

        [Fact]
        public void Negative()
        {
            // arrange
            var size = -8;

            // act
            var exception = Record.Exception(() => Guard.Against.Negative(() => size));

            // assert
            exception.ShouldBeValid<ArgumentOutOfRangeException>(ExceptionType.Negative).WithActualValue(size).WithParameter("size");
        }

        [Fact]
        public void NegativeOrZero()
        {
            // arrange
            var size = 0;

            // act
            var exception = Record.Exception(() => Guard.Against.NegativeOrZero(() => size));

            // assert
            exception.ShouldBeValid<ArgumentOutOfRangeException>(ExceptionType.NegativeOrZero).WithActualValue(size).WithParameter("size");
        }

        [Fact]
        public void ValidNegative()
        {
            // arrange
            var size = 8;

            // act
            var exception = Record.Exception(() => Guard.Against.Negative(() => size));

            // assert
            exception.Should().BeNull();
        }

        [Fact]
        public void ValidNegativeZero()
        {
            // arrange
            var size = 0;

            // act
            var exception = Record.Exception(() => Guard.Against.Negative(() => size));

            // assert
            exception.Should().BeNull();
        }

        [Fact]
        public void Positive()
        {
            // arrange
            var size = 8;

            // act
            var exception = Record.Exception(() => Guard.Against.Positive(() => size));

            // assert
            exception.ShouldBeValid<ArgumentOutOfRangeException>(ExceptionType.Positive).WithActualValue(size).WithParameter("size");
        }

        [Fact]
        public void PositiveOrZero()
        {
            // arrange
            var size = 0;

            // act
            var exception = Record.Exception(() => Guard.Against.PositiveOrZero(() => size));

            // assert
            exception.ShouldBeValid<ArgumentOutOfRangeException>(ExceptionType.PositiveOrZero).WithActualValue(size).WithParameter("size");
        }

        [Fact]
        public void ValidPositive()
        {
            // arrange
            var size = -8;

            // act
            var exception = Record.Exception(() => Guard.Against.Positive(() => size));

            // assert
            exception.Should().BeNull();
        }

        [Fact]
        public void ValidPositiveZero()
        {
            // arrange
            var size = 0;

            // act
            var exception = Record.Exception(() => Guard.Against.Positive(() => size));

            // assert
            exception.Should().BeNull();
        }

        [Fact]
        public void NullUriRelative()
        {
            // arrange
            var uri = (Uri)null;

            // act
            var exception = Record.Exception(() => Guard.Against.NullOrRelative(() => uri));

            // assert
            exception.ShouldBeValid<ArgumentNullException>().WithParameter("uri");
        }

        [Fact]
        public void RelativeUri()
        {
            // arrange
            var uri = new Uri(@"subdirectory/resource.html", UriKind.Relative);

            // act
            var exception = Record.Exception(() => Guard.Against.NullOrRelative(() => uri));

            // assert
            exception.ShouldBeValid<ArgumentException>(ExceptionType.RelativeUri).WithParameter("uri");
        }

        [Fact]
        public void ValidAbsoluteUri()
        {
            // arrange
            var uri = new Uri(@"http://www.test.com/");

            // act
            var exception = Record.Exception(() => Guard.Against.NullOrRelative(() => uri));

            // assert
            exception.Should().BeNull();
        }

        [Fact]
        public void NullUriAbsolute()
        {
            // arrange
            var uri = (Uri)null;

            // act
            var exception = Record.Exception(() => Guard.Against.NullOrAbsolute(() => uri));

            // assert
            exception.ShouldBeValid<ArgumentNullException>().WithParameter("uri");
        }

        [Fact]
        public void AbsoluteUri()
        {
            // arrange
            var uri = new Uri(@"http://www.test.com/");

            // act
            var exception = Record.Exception(() => Guard.Against.NullOrAbsolute(() => uri));

            // assert
            exception.ShouldBeValid<ArgumentException>(ExceptionType.AbsoluteUri).WithParameter("uri");
        }

        [Fact]
        public void ValidRelativeUri()
        {
            // arrange
            var uri = new Uri(@"subdirectory/resource.html", UriKind.Relative);

            // act
            var exception = Record.Exception(() => Guard.Against.NullOrAbsolute(() => uri));

            // assert
            exception.Should().BeNull();
        }
    }
}