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

using JetBrains.Annotations;
using System.ComponentModel;
using System.Xml.Serialization;

namespace AlphaCentaury.Licensing.Data.Serialization
{
    public abstract class LicensedComponentDependency: LicensedComponent
    {
        [XmlAttribute("dependency")]
        [DefaultValue(LicensedDependencyType.Direct)]
        public LicensedDependencyType DependencyType { get; set; }

        [XmlAttribute("version")]
        public string Version { get; set; }

        protected string DependencyTypeMark => DependencyType switch
        {
            LicensedDependencyType.Direct => null,
            LicensedDependencyType.Indirect => "+",
            LicensedDependencyType.Dynamic => "*",
            _ => "?"
        };

        public void Inherit([CanBeNull] LicensedComponentDependency from)
        {
            if (from == null) return;

            if (DependencyType != LicensedDependencyType.Direct) DependencyType = from.DependencyType;
            
            base.Inherit(from);
        } // Inherit

        protected void CopyToDependency(LicensedComponentDependency other)
        {
            other.DependencyType = DependencyType;
            other.Version = Version;
            CopyToComponent(other);
        } // CopyToDependency
    } // class ComponentDependency:
} // namespace
