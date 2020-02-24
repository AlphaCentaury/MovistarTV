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

namespace IpTviewr.Services.EpgDiscovery.TvAnytime
{
    /// <summary>
    /// This class is intended to help in "unifying" the different TVAMain namespaces.
    /// This allows to parse all XML documents from different version using the same
    /// XML .Net objects, regardless of document version. This is possible as newer
    /// versions are expected to be 100% backwards-compatible (at XML schema level,
    /// but not at code level).
    /// </summary>
    public static class NamespaceUnification
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ns">Original XML document namespace, possibly obsolete or not up-to-date</param>
        /// <returns>Equivalent up-to-date namespace</returns>
        public static string Replacer(string ns)
        {
            switch (ns)
            {
                case "urn:tva:metadata:2005": return Common.DefaultXmlNamespace;
                default:
                    return ns;
            } // switch
        } // Replacer
    } // NamespaceUnification
} // namespace
