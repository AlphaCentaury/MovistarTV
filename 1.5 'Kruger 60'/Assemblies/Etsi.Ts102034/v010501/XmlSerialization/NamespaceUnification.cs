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

namespace Etsi.Ts102034.v010501.XmlSerialization
{
    /// <summary>
    /// This class is intended to help in "unifying" the different TS 102 034 namespaces.
    /// This allows to parse all XML documents from v01.01.01 to v01.05.01 using the same
    /// XML .Net objects, regardless of document version. This is possible as v01.05.01 is
    /// 100% backwards-compatible (at XML schema level, but not at code level)
    /// </summary>
    public static class NamespaceUnification
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ns">Original TS 102 034 XML document namespace, possibly obsolete or not up-to-date</param>
        /// <returns>v01.05.01 equivalent up-to-date namespace</returns>
        public static string Replacer(string ns)
        {
            switch (ns)
            {
                //v01.01.01
                case "urn:dvb:ipisdns:2003": return "urn:dvb:metadata:iptv:sdns:2012-1";
                // v01.02.01
                case "urn:dvb:ipisdns:2006": return "urn:dvb:metadata:iptv:sdns:2012-1";
                case "urn:tva:metadata:2004": return "urn:tva:metadata:2011";
                case "urn:mpeg:mpeg7:schema:2001": return "urn:tva:mpeg7:2008";
                // v01.03.01
                case "urn:dvb:ipi:sdns:2006": return "urn:dvb:metadata:iptv:sdns:2012-1";
                case "urn:tva:metadata:2005": return "urn:tva:metadata:2011";
                case "urn:tva:mpeg7:2005": return "urn:tva:mpeg7:2008";
                // v01.04.01
                case "urn:dvb:metadata:iptv:sdns:2008-1": return "urn:dvb:metadata:iptv:sdns:2012-1";
                default:
                    return ns;
            } // switch
        } // Replacer
    } // NamespaceUnification
} // namespace
