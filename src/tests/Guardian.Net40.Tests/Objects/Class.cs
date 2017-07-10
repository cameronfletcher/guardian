// <copyright file="Class.cs" company="Guardian contributors">
//  Copyright (c) Guardian contributors. All rights reserved.
// </copyright>

#pragma warning disable 0649

namespace Guardian.Tests.Objects
{
    using System.Diagnostics.CodeAnalysis;

    internal class Class
    {
        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "For test purposes only.")]
        public Class ClassField;

        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "For test purposes only.")]
        public Struct StructField;

        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "For test purposes only.")]
        public Struct? NullableStructField;

        public Class ClassProperty { get; set; }

        public Struct StructProperty { get; set; }

        public Struct? NullableStructProperty { get; set; }
    }
}
