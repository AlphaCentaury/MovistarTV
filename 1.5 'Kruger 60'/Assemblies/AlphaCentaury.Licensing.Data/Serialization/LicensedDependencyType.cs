using System.Xml.Serialization;

namespace AlphaCentaury.Licensing.Data.Serialization
{
    public enum LicensedDependencyType
    {
        [XmlEnum("direct")]
        Direct = 0,

        [XmlEnum("dynamic")]
        Dynamic = 5, // a.k.a. runtime

        [XmlEnum("indirect")]
        Indirect = 10
    } // enum LicensedDependencyType
} // namespace