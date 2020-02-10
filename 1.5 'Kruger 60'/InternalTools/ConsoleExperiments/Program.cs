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

using System.Windows.Forms;

namespace IpTviewr.Internal.Tools.ConsoleExperiments
{
    internal class Program
    {
        private static int Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //var experiment = new Find();
            var experiment = new PushBuilder();

            return experiment.Execute(args);
        } // Main
    } // class Program
} // namespace
