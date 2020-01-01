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

namespace IpTviewr.UiServices.Configuration.Cache
{
    public sealed class CachedXmlDocument<T> : CachedItemBase
    {
        public CachedXmlDocument(T document, string documentType, string name, Version version, DateTime date)
            : base(documentType, name, version, date)
        {
            Document = document;
        } // constructor

        public T Document
        {
            get;
            private set;
        } // Document
    } // CachedXmlDocument<T>
} // namespace
