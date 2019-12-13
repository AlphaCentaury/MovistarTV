// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using JetBrains.Annotations;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Interfaces
{
    [PublicAPI]
    public interface IMaintenanceTool
    {
        void Execute([NotNull] IReadOnlyList<string> arguments, [NotNull] IToolOutputWriter writer, CancellationToken token);
        void ShowUsage([NotNull] IToolOutputWriter writer);
        Form GetUi();
        string SelectFileFilter { get; }
    } // interface IMaintenanceTool
} // namespace
