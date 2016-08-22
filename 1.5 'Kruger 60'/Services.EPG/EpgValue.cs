// Copyright (C) 2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Project.IpTv.Services.EPG
{
    [Serializable()]
    [DebuggerStepThrough()]
    [DesignerCategory("code")]
    [XmlType(AnonymousType = true, Namespace = Common.XmlNamespace)]
    public class EpgValue
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

        public static EpgValue ToValue(TvAnytime.TvaName name)
        {
            if (name == null) return null;

            return new EpgValue()
            {
                Code = name.HRef,
                Description = name.Name
            };
        } // ToValue

        public static EpgValue ToValue(TvAnytime.Mpeg7Name name)
        {
            if (name == null) return null;

            return new EpgValue()
            {
                Code = name.HRef,
                Description = name.Name
            };
        } // ToValue

        public override string ToString()
        {
            return Description;
        } // ToString
    } // class EpgValue
} // namespace
