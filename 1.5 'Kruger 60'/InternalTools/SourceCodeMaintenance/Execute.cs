// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;

namespace AlphaCentaury.Tools.SourceCodeMaintenance
{
    public class Execute
    {
        public static void ExecuteToolConsole(IMaintenanceTool tool, string[] arguments)
        {
            ExecuteTool(tool, arguments, Console.WriteLine);
        } // ExecuteConsole

        public static void ExecuteTool(IMaintenanceTool tool, string[] arguments, Action<string> writeLine)
        {
            if (tool == null) throw new ArgumentNullException(nameof(tool));
            if (arguments == null) throw new ArgumentNullException(nameof(arguments));
            if (writeLine == null) throw new ArgumentNullException(nameof(writeLine));

            try
            {
                tool.Execute(arguments, writeLine);
            }
            catch (Exception e)
            {
                writeLine(e.ToString());
            } // catch
        } // Execute
    }
}
