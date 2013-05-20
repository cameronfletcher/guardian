// <copyright file="SupportedGuardClauses.cs" company="Guardian contributors">
//  Copyright (c) Guardian contributors. All rights reserved.
// </copyright>

namespace Guardian.Tests
{
    using System;
    using Xunit;

    public class SupportedGuardClauses
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
    }
}