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
    public class ThirdPartyLibraryComparer: IEqualityComparer<ThirdPartyLibrary>, IComparer<ThirdPartyLibrary>
    {
        #region Implementation of IEqualityComparer<in ThirdPartyLibrary>

        public bool Equals(ThirdPartyLibrary x, ThirdPartyLibrary y)
        {
            return string.Equals(x?.Name, y?.Name, StringComparison.InvariantCulture);
        } // Equals

        public int GetHashCode(ThirdPartyLibrary obj)
        {
            return obj.Name.GetHashCode();
        } // GetHashCode

        #endregion

        #region Implementation of IComparer<in ThirdPartyLibrary>

        public int Compare(ThirdPartyLibrary x, ThirdPartyLibrary y)
        {
            return string.Compare(x?.Name, y?.Name, StringComparison.InvariantCulture);
        } // Compare

        #endregion
    } // class ThirdPartyLibraryComparer
} // namespace
