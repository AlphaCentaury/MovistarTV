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

using System.Drawing;

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
