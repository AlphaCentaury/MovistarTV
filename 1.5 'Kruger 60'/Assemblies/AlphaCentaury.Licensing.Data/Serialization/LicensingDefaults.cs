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
using System.CodeDom;
using System.Collections.Generic;
using System.Xml.Serialization;
using JetBrains.Annotations;

namespace AlphaCentaury.Licensing.Data.Serialization
{
    [Serializable]
    [XmlRoot("Licensing.Defaults", Namespace = LicensingData.Namespace)]
    public class LicensingDefaults : ICloneable<LicensingDefaults>
    {
        public class CommonDefaults : LicensedItem
        {
            public override LicensedItemType Type => LicensedItemType.Unknown;

            public override void Inherit(LicensedItem from) => ProtectedInherit(from);

            protected override LicensedItem CreateNewForCloning() => new CommonDefaults();
        } // class CommonDefaults

        [XmlAttribute("appliesTo")]
        public string AppliesTo { get; set; }

        [XmlAttribute("inheritsFrom")]
        public string InheritsFrom { get; set; }

        [XmlElement("Common", typeof(CommonDefaults))]
        public CommonDefaults Common { get; set; }

        [XmlElement("Libraries", typeof(LicensedLibrary))]
        public LicensedLibrary ForLibraries { get; set; }

        [XmlElement("Programs", typeof(LicensedProgram))]
        public LicensedProgram ForPrograms { get; set; }

        [XmlArrayItem("License")]
        public List<License> Licenses { get; set; }

        public void Inherit([CanBeNull] LicensingDefaults inheritFrom)
        {
            Common ??= new CommonDefaults();
            ForLibraries ??= new LicensedLibrary();
            ForPrograms ??= new LicensedProgram();

            Common.Inherit(inheritFrom?.Common);

            ForLibraries.Inherit(Common);
            ForLibraries.Inherit(inheritFrom?.ForLibraries);

            ForPrograms.Inherit(Common);
            ForPrograms.Inherit(inheritFrom?.ForPrograms);
        } // Inherit

        public LicensingDefaults Clone()
        {
            return new LicensingDefaults
            {
                AppliesTo = AppliesTo,
                ForLibraries = ForLibraries.Clone(),
                ForPrograms = ForPrograms.Clone(),
                Licenses = Licenses.Clone()
            };
        } // Clone

        object ICloneable.Clone() => Clone();

        // avoid empty list when serializing
        public bool LicensesSpecified => (Licenses != null) && (Licenses.Count > 0);
    } // class LicensingDefaults
} // class LicensingDefaults
