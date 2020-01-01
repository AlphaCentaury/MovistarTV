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
using System.Xml.Serialization;

namespace IpTviewr.UiServices.Configuration.Schema2014.Config
{
    [Serializable]
    [XmlType(TypeName = "EpgConfig", Namespace = ConfigCommon.ConfigXmlNamespace)]
    public class EpgConfig
    {
        public EpgConfig()
        {
            // no op; default constructor
        } // constructor

        public EpgConfig(bool enabled, int autoUpdateHours, int maxDays)
        {
            Enabled = enabled;
            AutoUpdateHours = autoUpdateHours;
            MaxDays = maxDays;
        } // constructor

        public bool Enabled
        {
            get;
            set;
        } // Enabled

        public int AutoUpdateHours
        {
            get;
            set;
        } // AutoUpdateHours

        public int MaxDays
        {
            get;
            set;
        } // MaxDays
    } // class EpgConfig
} // namespace
