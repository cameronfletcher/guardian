// <copyright file="Thing.cs" company="Guardian contributors">
//  Copyright (c) Guardian contributors. All rights reserved.
// </copyright>

namespace Guardian.Tests
{
    using System.Diagnostics.CodeAnalysis;

    public class Thing
    {
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "To aid distinction.")]
        public int? field
        {
            get { return null; }
        }

        public string Property
        {
            get { return null; }
        }

        public Thing[] ThingArray
        {
            get { return new[] { new Thing(), new Thing() }; }
        }

        public string[] EmptyArray
        {
            get { return new string[0]; }
        }

        public Thing NestedThing
        {
            get { return new Thing(); }
        }

        public string Method()
        {
            return null;
        }

        public string Method(string argument)
        {
            return null;
        }
    }
}
