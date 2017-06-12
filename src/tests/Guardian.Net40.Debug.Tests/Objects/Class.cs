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
        public Structure StructField;

        [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "For test purposes only.")]
        public Structure? NullableStructField;

        public Class ClassProperty { get; set; }

        public Structure StructProperty { get; set; }

        public Structure? NullableStructProperty { get; set; }
    }
}
