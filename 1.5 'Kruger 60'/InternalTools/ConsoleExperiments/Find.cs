// Copyright (C) 2014-2017, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://movistartv.alphacentaury.org/ https://github.com/AlphaCentaury

using IpTviewr.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpTviewr.Internal.Tools.ConsoleExperiments
{
    class Find : Experiment
    {
        protected override int Run(string[] cmdArgs)
        {
            CommandLineArguments args = new CommandLineArguments();
            args.SpecialHelpArgument = true;
            args.Parse(cmdArgs);

            if ((args.IsHelpRequested) || (args.Arguments.Count == 0))
            {
                Console.WriteLine("Searches for a text string in a file or files within the current directory.");
                Console.WriteLine();
                Console.WriteLine("FIND \"TextToFind\" WildcardFilenames [/S]");
                Console.WriteLine("TextToFind          Text string to find");
                Console.WriteLine("WildcardFilenames   Files to search, using wildcard characters (* and ?)");
                Console.WriteLine("/S                  Include subdirectories");

                return 1;
            } // if

            var pattern = Path.GetFileName(args.Arguments[1]);
            var path = Directory.GetCurrentDirectory();
            foreach (var file in Directory.GetFiles(path, pattern, args.Switches.ContainsKey("s") ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly))
            {
                FindInFile(args.Arguments[0], file);
            } // foreach

            return 0;
        } // Run

        private void FindInFile(string text, string file)
        {
            var contents = File.ReadAllText(file);
            var index = contents.IndexOf(text);
            if (index < 0) return;

            var current = Directory.GetCurrentDirectory();
            if (file.StartsWith(current))
            {
                file = file.Substring(current.Length + 1);
            } // if
            var start = index - 10;
            var length = text.Length + 20;
            if (start < 0) start = 0;
            if ((start + length) >= contents.Length) length = contents.Length - start;
            
            Console.WriteLine("----- {0}", file);
            Console.WriteLine(contents.Substring(start, length));
        } // FindInFile
    } // class Find
} // namespace
