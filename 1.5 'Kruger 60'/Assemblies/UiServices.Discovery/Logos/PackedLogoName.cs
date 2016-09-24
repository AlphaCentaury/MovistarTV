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
    [XmlType(Namespace = XmlPackedLogosRoot.Namespace)]
    public class PackedLogoName: IComparable<PackedLogoName>
    {
        [XmlIgnore]
        public string Key
        {
            get { return IsHD ? "hd_" + Name : Name; }
        } // Key

        [XmlAttribute("name")]
        public string Name
        {
            get;
            set;
        } // 

        [XmlIgnore]
        public bool IsHD
        {
            get
            {
                return XmlIsHDSpecified;
            } // get
            set
            {
                XmlIsHD = value;
                XmlIsHDSpecified = (value == true);
            } // set
        } // IsHD

        /// <remarks/>
        [XmlAttribute("isHD")]
        public bool XmlIsHD
        {
            get;
            set;
        } // XmlIsHD

        /// <remarks/>
        [XmlIgnore]
        public bool XmlIsHDSpecified
        {
            get;
            set;
        } // XmlIsHDSpecified

        public int CompareTo(PackedLogoName other)
        {
            var compare = string.Compare(Name, other.Name);
            if (compare != 0) return compare;

            return (other.IsHD) ? -1 : 1;
        } // CompareTo
    } // class PackedLogoName
} // namespace
