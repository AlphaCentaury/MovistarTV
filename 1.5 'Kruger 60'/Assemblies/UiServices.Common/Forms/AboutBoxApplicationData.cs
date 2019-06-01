// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace IpTviewr.UiServices.Common.Forms
{
    public class AboutBoxApplicationData
    {
        public Image LargeIcon
        {
            get;
            set;
        } // LargeIcon

        public string Name
        {
            get;
            set;
        } // Name

        public string Version
        {
            get;
            set;
        } // Version

        public string Status
        {
            get;
            set;
        } // Status

        public string LicenseText
        {
            get;
            set;
        } // LicenseText

        public string LicenseTextRtf
        {
            get;
            set;
        } // LicenseTextRtf
    } // class AboutBoxApplicationData
} // namespace
