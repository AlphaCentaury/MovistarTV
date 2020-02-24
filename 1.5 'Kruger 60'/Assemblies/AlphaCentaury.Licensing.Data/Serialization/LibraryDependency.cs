// ==============================================================================
// 
//   Copyright (C) 2014-2020, GitHub/Codeplex user AlphaCentaury
//   All rights reserved.
// 
//     See 'LICENSE.MD' file (or 'license.txt' if missing) in the project root
//     for complete license information.
// 
//   http://www.alphacentaury.org/movistartv
//   https://github.com/AlphaCentaury
// 
// ==============================================================================

using System;
using System.Xml.Serialization;
using JetBrains.Annotations;

namespace AlphaCentaury.Licensing.Data.Serialization
{
    [Serializable]
    public sealed class LibraryDependency : LicensedComponentDependency, ICloneable<LibraryDependency>
    {
        [XmlElement("File")]
        public string Assembly { get; set; }

        public LicensedItemType Type { get; set; }

        public override string ToString() => $"{DependencyTypeMark}{Name}";

        public LibraryDependency Clone()
        {
            var clone = new LibraryDependency
            {
                Assembly = Assembly
            };
            CopyToDependency(clone);

            return clone;
        } // Clone

        object ICloneable.Clone() => Clone();

        public void Inherit([CanBeNull] LibraryDependency from)
        {
            if (from == null) return;

            Assembly ??= from.Assembly;
            if (Type == LicensedItemType.Unknown) Type = from.Type;

            base.Inherit(from);
        } // Inherit
    } // class LibraryDependency
} // namespace
