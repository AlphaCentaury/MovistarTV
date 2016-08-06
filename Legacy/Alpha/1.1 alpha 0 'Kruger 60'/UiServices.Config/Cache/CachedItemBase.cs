using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.DvbIpTv.UiServices.Configuration.Cache
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
