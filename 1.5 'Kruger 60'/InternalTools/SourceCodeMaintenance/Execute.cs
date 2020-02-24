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
using System.Threading;
using AlphaCentaury.Tools.SourceCodeMaintenance.Interfaces;

namespace AlphaCentaury.Tools.SourceCodeMaintenance
{
    public class Execute
    {
        public static void ExecuteTool(IMaintenanceTool tool, string[] arguments, IToolOutputWriter writer, CancellationToken token)
        {
            if (tool == null) throw new ArgumentNullException(nameof(tool));
            if (arguments == null) throw new ArgumentNullException(nameof(arguments));
            if (writer == null) throw new ArgumentNullException(nameof(writer));

            try
            {
                tool.Execute(arguments, writer, token);
            }
            catch (Exception e)
            {
                writer.WriteException(e);
            } // catch
        } // Execute
    }
}
