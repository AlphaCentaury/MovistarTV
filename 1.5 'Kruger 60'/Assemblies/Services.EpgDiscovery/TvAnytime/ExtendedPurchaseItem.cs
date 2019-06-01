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
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IpTviewr.Services.EpgDiscovery.TvAnytime
{
    [XmlType(TypeName= "ExtendedPurchaseItemType", Namespace = Common.TvaExtendedNamespace)]
    public class ExtendedPurchaseItem: TvaMain
    {
        [XmlElement("ProgramDescription", Namespace = Common.DefaultXmlNamespace)]
        public TvaProgramDescription ProgramDescription
        {
            get;
            set;
        } // ProgramDescription
    } // ExtendedPurchaseItem
} // namespace
