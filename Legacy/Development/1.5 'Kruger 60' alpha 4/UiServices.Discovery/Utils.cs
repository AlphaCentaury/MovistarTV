// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Etsi.Ts102034.v010501.XmlSerialization.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Property = System.Collections.Generic.KeyValuePair<string, string>;

namespace Project.IpTv.UiServices.Discovery
{
    internal class Utils
    {
        public static Property GetLanguageProperty(string name, MultilingualText text)
        {
            if (text == null) return new Property(name, null);
            if (text.Language == null) return new Property(name, text.Value);
            return new Property(string.Format("{0} ({1})", name, text.Language), text.Value);
        } // GetLanguageProperty
    } // internal class Utils
} // namespace
