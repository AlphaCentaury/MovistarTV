// Copyright (C) 2014-2017, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://movistartv.alphacentaury.org/ https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
