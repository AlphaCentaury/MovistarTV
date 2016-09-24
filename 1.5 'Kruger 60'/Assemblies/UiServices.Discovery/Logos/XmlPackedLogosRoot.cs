using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IpTviewr.UiServices.Discovery.Logos
{
    [Serializable]
    [DebuggerStepThrough]
    [XmlRoot(Namespace = XmlPackedLogosRoot.Namespace)]
    public class XmlPackedLogosRoot
    {
        public const string Namespace = "http://movistartv.codeplex.com/schema/2016:PackedLogos";

        [XmlElement("Logo")]
        public PackedLogo[] Logos
        {
            get;
            set;
        } // Logos
    } // class XmlPackedLogosRoot
} // namespace
