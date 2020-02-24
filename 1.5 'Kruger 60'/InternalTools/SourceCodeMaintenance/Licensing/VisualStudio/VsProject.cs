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

using System;
using System.Collections.Generic;
using AlphaCentaury.Licensing.Data.Serialization;
using JetBrains.Annotations;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.VisualStudio
{
    public abstract class VsProject
    {
        public Guid Guid { get; set; }

        public abstract string Language { get; }

        public string Name { get; set; }

        public string Filename { get; set; }

        public string AssemblyName { get; set; }

        public string Namespace { get; set; }

        public List<Guid> ReferencedProjects { get; set; }

        public string Type { get; set; }

        public string LicensingDefaultsKey { get; set; }

        public override string ToString()
        {
            return $"{Type}:{Namespace}";
        } // ToString

        public abstract bool IsLibrary { get; }

        public abstract bool IsGui { get; }

        public abstract string ImageKey { get; }

        public virtual LicensingData GetLicensingData(LicensingDefaults defaults)
        {
            var licensed = GetLicensedItem(defaults);
            licensed.Name = Namespace;

            return new LicensingData
            {
                Licensed = licensed,
                Licenses = defaults.Licenses
            };
        } // GetLicensingData

        protected abstract LicensedItem GetLicensedItem(LicensingDefaults defaults);
    } // VsProject
} // 
