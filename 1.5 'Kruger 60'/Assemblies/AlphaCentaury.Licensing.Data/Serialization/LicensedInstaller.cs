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
    public class LicensedInstaller: LicensedProgram, ICloneable<LicensedInstaller>
    {
        public string Technology { get; set; }

        public override LicensedItemType Type => LicensedItemType.Installer;

        public new LicensedInstaller Clone()
        {
            return (LicensedInstaller) base.Clone();
        } // Clone

        object ICloneable.Clone() => Clone();

        public void Inherit([CanBeNull] LicensedInstaller from)
        {
            if (from == null) return;

            Technology ??= from.Technology;

            ProtectedInherit(from);
        } // Inherit

        public override void Inherit([CanBeNull] LicensedItem from)
        {
            if (from is LicensedInstaller installer)
            {
                Inherit(installer);
            }
            else
            {
                ProtectedInherit(from);
            } // if-else
        } // Override

        protected override LicensedItem CreateNewForCloning()
        {
            var result = new LicensedInstaller
            {
                Technology = Technology
            };

            return result;
        } // CreateNewForCloning
    } // class LicensedInstaller
} // namespace
