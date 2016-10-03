using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IpTviewr.Services.EpgDiscovery
{
    [Serializable()]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(TypeName = "Episode", Namespace = Common.XmlNamespace)]
    public class EpgProgramEpisode
    {
        [XmlAttribute("seriesId")]
        public string SeriesId
        {
            get;
            set;
        } // SeriesCRID

        public string SeriesName
        {
            get;
            set;
        } // SeriesName

        public int? Number
        {
            get;
            set;
        } // Number

        public int? Season
        {
            get;
            set;
        } // Season

        public int? Year
        {
            get;
            set;
        } // Year
    } // EpgProgramEpisode
} // namespace
