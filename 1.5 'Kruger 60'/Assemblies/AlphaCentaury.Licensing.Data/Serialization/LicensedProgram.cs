// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace AlphaCentaury.Licensing.Data.Serialization
{
    [Serializable]
    public sealed class LicensedProgram: LicensedItem
    {
        [XmlAttribute("interactive")]
        [DefaultValue(true)]
        public bool IsGuiApp { get; set; }

        public override LicensedItemType Type => LicensedItemType.Program;

        protected override LicensedItem CreateNewForCloning() => new LicensedProgram
        {
            IsGuiApp = IsGuiApp
        };
    } // class LicensedProgram
} // namespace
