using System.ComponentModel;
using System.Xml.Serialization;

namespace AlphaCentaury.Licensing.Data.Serialization
{
    public abstract class LicensedComponentDependency: LicensedComponent
    {
        [XmlAttribute("dependency")]
        [DefaultValue(LicensedDependencyType.Direct)]
        public LicensedDependencyType DependencyType { get; set; }

        protected string DependencyTypeMark => DependencyType switch
        {
            LicensedDependencyType.Direct => null,
            LicensedDependencyType.Indirect => "+",
            LicensedDependencyType.Dynamic => "*",
            _ => "?"
        };

        protected void CopyToDependency(LicensedComponentDependency other)
        {
            other.DependencyType = DependencyType;
            CopyToComponent(other);
        } // CopyToDependency
    } // class ComponentDependency:
} // namespace