// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using AlphaCentaury.Tools.SourceCodeMaintenance.Interfaces;

namespace AlphaCentaury.Tools.SourceCodeMaintenance
{
    public class Execute
    {
        public static void ExecuteTool(IMaintenanceTool tool, string[] arguments, IToolOutputWriter writer)
        {
            if (tool == null) throw new ArgumentNullException(nameof(tool));
            if (arguments == null) throw new ArgumentNullException(nameof(arguments));
            if (writer == null) throw new ArgumentNullException(nameof(writer));

            try
            {
                tool.Execute(arguments, writer);
            }
            catch (Exception e)
            {
                writer.WriteException(e);
            } // catch
        } // Execute
    }
}
