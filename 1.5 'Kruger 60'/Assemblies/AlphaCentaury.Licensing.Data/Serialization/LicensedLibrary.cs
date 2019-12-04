// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;

namespace AlphaCentaury.Licensing.Data.Serialization
{
    [Serializable]
    public sealed class LicensedLibrary: LicensedItem
    {
        public override string Type => "Library";
    } // class LicensedLibrary
} // namespace
