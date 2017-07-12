// <copyright file="Thing.cs" company="Guardian contributors">
//  Copyright (c) Guardian contributors. All rights reserved.
// </copyright>

#pragma warning disable 0649

namespace Guardian.Tests.Objects
{
    using System.Diagnostics.CodeAnalysis;

    internal class Thing
    {
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "For test purposes only.")]
        public int? field;

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
