// <copyright file="Bug002.cs" company="Guardian contributors">
//  Copyright (c) Guardian contributors. All rights reserved.
// </copyright>

namespace Guardian.Tests.Bugs
{
    using FluentAssertions;
    using Guardian.Tests.Objects;
    using Xunit;

    public class Bug002
    {
        [Fact]
        public void BugFix002()
        {
            // arrange
            var thing = new Thing();


            // act
            var parsedExpression = Guard.Expression.Parse(() => thing.ThingArray[1].ThingArray);

            // assert
            parsedExpression.Should().BeNull();
        }
    }
}
