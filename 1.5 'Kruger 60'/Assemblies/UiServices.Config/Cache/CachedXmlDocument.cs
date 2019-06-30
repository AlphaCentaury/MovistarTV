// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

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
