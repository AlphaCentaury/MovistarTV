// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlphaCentaury.Tools.SourceCodeMaintenance
{
    public interface IMaintenanceTool
    {
        void Execute([NotNull] IReadOnlyList<string> arguments, [NotNull] Action<string> writeLine);
        void ShowUsage([NotNull] Action<string> writeLine);
        Form GetUi();
        string SelectFileFilter { get; }
    } // interface IMaintenanceTool
} // namespace
