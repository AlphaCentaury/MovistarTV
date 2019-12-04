// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using AlphaCentaury.Licensing.Data.Serialization;

namespace AlphaCentaury.Licensing.Data
{
    public class ThirdPartyDependencyComparer: IEqualityComparer<ThirdPartyDependency>, IComparer<ThirdPartyDependency>
    {
        #region Implementation of IEqualityComparer<in ThirdPartyLibrary>

        public bool Equals(ThirdPartyDependency x, ThirdPartyDependency y)
        {
            return string.Equals(x?.Name, y?.Name, StringComparison.InvariantCulture);
        } // Equals

        public int GetHashCode(ThirdPartyDependency obj)
        {
            return obj.Name.GetHashCode();
        } // GetHashCode

        #endregion

        #region Implementation of IComparer<in ThirdPartyLibrary>

        public int Compare(ThirdPartyDependency x, ThirdPartyDependency y)
        {
            return string.Compare(x?.Name, y?.Name, StringComparison.InvariantCulture);
        } // Compare

        #endregion
    } // class ThirdPartyLibraryComparer
} // namespace
