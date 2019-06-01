// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace IpTviewr.UiServices.Configuration.Schema2014.ContentProvider
{
    [Serializable]
    public class FriendlyNames
    {
        [XmlElement("ServiceProviders")]
        public SpFriendlyNames[] Providers
        {
            get;
            set;
        } // Providers
    } // class FriendlyNames
} // namespace
