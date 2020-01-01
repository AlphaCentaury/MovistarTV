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
using System.ComponentModel;
using System.Xml.Serialization;
using JetBrains.Annotations;

namespace AlphaCentaury.Licensing.Data.Serialization
{
    [Serializable]
    public class LicensedProgram: LicensedItem, ICloneable<LicensedProgram>
    {
        [XmlAttribute("userInterface")]
        [DefaultValue(true)]
        public bool IsGuiApp { get; set; }

        public LicensedProgram()
        {
            IsGuiApp = true;
        } // constructor

        public override LicensedItemType Type => LicensedItemType.Program;

        public new LicensedProgram Clone()
        {
            return (LicensedProgram)base.Clone();
        } // Clone

        public void CopyTo(LicensedProgram program)
        {
            if (program == null) return;

            program.IsGuiApp = IsGuiApp;
            CopyTo((LicensedItem) program);
        } // CopyTo

        public void Inherit([CanBeNull] LicensedProgram from)
        {
            if (from == null) return;

            if (!IsGuiApp) IsGuiApp = from.IsGuiApp;
            ProtectedInherit(from);
        } // Inherit

        public override void Inherit([CanBeNull] LicensedItem from)
        {
            if (from is LicensedProgram program)
            {
                Inherit(program);
            }
            else
            {
                ProtectedInherit(from);
            } // if-else
        } // Override

        protected override LicensedItem CreateNewForCloning() => new LicensedProgram
        {
            IsGuiApp = IsGuiApp
        };
    } // class LicensedProgram
} // namespace
