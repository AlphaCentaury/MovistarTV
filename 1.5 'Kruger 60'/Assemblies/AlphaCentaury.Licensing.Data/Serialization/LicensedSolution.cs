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

using JetBrains.Annotations;

namespace AlphaCentaury.Licensing.Data.Serialization
{
    public class LicensedSolution: LicensedLibrary
    {
        public override LicensedItemType Type => LicensedItemType.Solution;

        public new LicensedSolution Clone()
        {
            return (LicensedSolution)base.Clone();
        } // Clone

        public void Inherit([CanBeNull] LicensedSolution from)
        {
            ProtectedInherit(from);
        } // Inherit

        public override void Inherit([CanBeNull] LicensedItem from)
        {
            if (from is LicensedSolution solution)
            {
                Inherit(solution);
            }
            else
            {
                ProtectedInherit(from);
            } // if-else
        } // Inherit

        protected override LicensedItem CreateNewForCloning() => new LicensedSolution();
    } // class LicensedSolution
} // namespace
