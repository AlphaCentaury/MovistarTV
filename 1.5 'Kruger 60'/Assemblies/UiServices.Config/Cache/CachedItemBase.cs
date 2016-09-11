// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IpTviewr.UiServices.Configuration.Cache
{
    public abstract class CachedItemBase
    {
        public CachedItemBase(string documentType, string name, Version version, DateTime date)
        {
            DocumentType = documentType;
            Name = name;
            Version = version;
            Date = date;
        } // constructor

        public string DocumentType
        {
            get;
            private set;
        } // DocType

        public string Name
        {
            get;
            private set;
        } // Name

        public Version Version
        {
            get;
            private set;
        } // Version

        public DateTime Date
        {
            get;
            private set;
        } // Date

        public TimeSpan Age
        {
            get { return DateTime.Now - Date; }
        } // Age
    } // CachedItemBase
} // namespace
