using DvbIpTypes.Schema2006;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Property = System.Collections.Generic.KeyValuePair<string, string>;

namespace Project.DvbIpTv.UiServices.Discovery
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
