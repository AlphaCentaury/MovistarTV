// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace IpTviewr.Services.EpgDiscovery
{
    [Serializable()]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(TypeName = "Episode", Namespace = Common.XmlNamespace)]
    public class EpgProgramEpisode
    {
        [XmlAttribute("seriesId")]
        public string SeriesId
        {
            get;
            set;
        } // SeriesCRID

        public string SeriesName
        {
            get;
            set;
        } // SeriesName

        public int? Number
        {
            get;
            set;
        } // Number

        public int? Season
        {
            get;
            set;
        } // Season

        public int? Year
        {
            get;
            set;
        } // Year
    } // EpgProgramEpisode
} // namespace
