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
using System.Xml.Serialization;

namespace IpTviewr.UiServices.Configuration.Schema2014.ContentProvider
{
    [Serializable]
    [XmlType(Namespace=SerializationCommon.XmlNamespace, AnonymousType=true)]
    public class Identification
    {
        [XmlAttribute("id")]
        public string Id
        {
            get;
            set;
        } // Id

        [XmlElement("Localized")]
        public LocalizedIdentification[] Localized
        {
            get;
            set;
        } // Localized

        public string LogosPackageName
        {
            get;
            set;
        } // LogosPackageName
    } // class ContentProviderIdentification
} // namespace
