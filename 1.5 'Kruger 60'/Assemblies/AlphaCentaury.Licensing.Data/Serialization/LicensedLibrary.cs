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
using JetBrains.Annotations;

namespace AlphaCentaury.Licensing.Data.Serialization
{
    [Serializable]
    public class LicensedLibrary: LicensedItem, ICloneable<LicensedLibrary>
    {
        public override LicensedItemType Type => LicensedItemType.Library;

        public new LicensedLibrary Clone()
        {
            return (LicensedLibrary)base.Clone();
        } // Clone

        public void Inherit([CanBeNull] LicensedLibrary from)
        {
            ProtectedInherit(from);
        } // Inherit

        public override void Inherit([CanBeNull] LicensedItem from)
        {
            if (from is LicensedLibrary library)
            {
                Inherit(library);
            }
            else
            {
                ProtectedInherit(from);
            } // if-else
        } // Inherit

        protected override LicensedItem CreateNewForCloning() => new LicensedLibrary();
    } // class LicensedLibrary
} // namespace
