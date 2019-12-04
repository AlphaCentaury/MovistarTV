// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Windows.Forms;
using JetBrains.Annotations;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing
{
    [Export(typeof(IMaintenanceTool))]
    [ExportMetadata("Name", "Licensing maintenance")]
    [ExportMetadata("Guid", "{13B1F04C-F4E9-4C13-832F-8FCBC5673098}")]
    [ExportMetadata("HasParameters", false)]
    [ExportMetadata("HasUi", true)]
    public class LicensingMaintenance : IMaintenanceTool
    {
        #region Implementation of IMaintenanceTool

        public void Execute([NotNull] IReadOnlyList<string> arguments, [NotNull] Action<string> writeLine)
        {
            throw new NotImplementedException();
        } // Execute

        public void ShowUsage(Action<string> writeLine)
        {
            throw new NotSupportedException();
        } // ShowUsage

        public Form GetUi() => new LicensingForm();

        public string SelectFileFilter => throw new NotSupportedException();

        #endregion
    } // class LicensingMaintenance
} // namespace
