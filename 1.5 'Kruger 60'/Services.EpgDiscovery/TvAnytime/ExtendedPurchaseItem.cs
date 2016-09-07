using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IpTviewr.Services.EpgDiscovery.TvAnytime
{
    [XmlType(TypeName= "ExtendedPurchaseItemType", Namespace = Common.TvaExtendedNamespace)]
    public class ExtendedPurchaseItem: TvaMain
    {
        [XmlElement("ProgramDescription", Namespace = Common.DefaultXmlNamespace)]
        public TvaProgramDescription ProgramDescription
        {
            get;
            set;
        } // ProgramDescription
    } // ExtendedPurchaseItem
} // namespace
