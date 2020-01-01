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

using Etsi.Ts102034.v010501.XmlSerialization.Common;
using Property = System.Collections.Generic.KeyValuePair<string, string>;

namespace IpTviewr.UiServices.Discovery
{
    internal class Utils
    {
        public static Property GetLanguageProperty(string name, MultilingualText text)
        {
            if (text == null) return new Property(name, null);
            if (text.Language == null) return new Property(name, text.Value);
            return new Property($"{name} ({text.Language})", text.Value);
        } // GetLanguageProperty
    } // internal class Utils
} // namespace
