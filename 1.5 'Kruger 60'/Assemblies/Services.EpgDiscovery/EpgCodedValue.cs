// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace IpTviewr.Services.EpgDiscovery
{
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = Common.XmlNamespace)]
    public class EpgCodedValue
    {
        public string Code
        {
            get;
            set;
        } // Code

        public string Description
        {
            get;
            set;
        } // Description

        public static EpgCodedValue ToCodedValue(TvAnytime.TvaName name)
        {
            if (name == null) return null;

            return new EpgCodedValue()
            {
                Code = name.HRef,
                Description = name.Name
            };
        } // ToCodedValue

        public static EpgCodedValue ToCodedValue(TvAnytime.Mpeg7Name name)
        {
            if (name == null) return null;

            return new EpgCodedValue()
            {
                Code = name.HRef,
                Description = name.Name
            };
        } // ToCodedValue

        public override string ToString()
        {
            return Description;
        } // ToString
    } // class EpgCode
} // namespace
