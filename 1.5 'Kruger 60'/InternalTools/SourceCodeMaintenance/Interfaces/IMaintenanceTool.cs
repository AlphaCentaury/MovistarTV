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
