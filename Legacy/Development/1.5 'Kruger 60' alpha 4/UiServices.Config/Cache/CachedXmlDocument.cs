// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.IpTv.UiServices.Configuration.Cache
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
