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
using System.ComponentModel;
using System.Diagnostics;
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
