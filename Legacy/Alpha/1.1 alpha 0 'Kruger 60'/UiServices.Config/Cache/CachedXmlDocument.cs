using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.DvbIpTv.UiServices.Configuration.Cache
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
