// <copyright file="GuardBugTests.cs" company="Guardian contributors">
//  Copyright (c) Guardian contributors. All rights reserved.
// </copyright>

namespace Guardian.Tests
{
    using System.Diagnostics.CodeAnalysis;
    using FluentAssertions;
    using Xunit;

    [SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1201:ElementsMustAppearInTheCorrectOrder", Justification = "Not on this occasion.")]
    public class GuardBugTests
    {
        [Fact]
        public void Bug001()
        {
            // arrange
            var thing = new ActualThing();

            // act
            var exception = Record.Exception(() => thing.Check(thing));

            // assert
            exception.Should().BeNull();
        }

        private interface IThing
        {
        }

        private class BaseThing<T> where T : BaseThing<T>
        {
            public void Check(IThing thing)
            {
                Guard.Expression.Parse(() => thing);
            }
        }

        private class ActualThing : BaseThing<ActualThing>, IThing
        {
        }
    }
}
