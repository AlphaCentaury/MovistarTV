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

        public TimeSpan Age => DateTime.Now - Date;
    } // CachedItemBase
} // namespace
