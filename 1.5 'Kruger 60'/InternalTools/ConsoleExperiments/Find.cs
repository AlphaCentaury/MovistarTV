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

using IpTviewr.Common;
using System;
using System.IO;

namespace IpTviewr.Internal.Tools.ConsoleExperiments
{
    internal class Find : Experiment
    {
        protected override int Run(string[] cmdArgs)
        {
            var args = new CommandLineArguments
            {
                SpecialHelpArgument = true
            };
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
            var matches = 0;
            foreach (var file in Directory.EnumerateFiles(path, pattern, args.Switches.ContainsKey("s") ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly))
            {
                matches += FindInFile(args.Arguments[0], file) ? 1 : 0;
            } // foreach

            if (matches == 0)
            {
                Console.WriteLine($"\"{args.Arguments[0]}\" not found.");
            }
            else
            {
                Console.WriteLine($"\"{args.Arguments[0]}\" found in {matches} files.");
            } // if-else

            return 0;
        } // Run

        private static bool FindInFile(string text, string file)
        {
            var lineNumber = 0;
            var filePrinted = false;
            foreach (var line in File.ReadLines(file))
            {
                lineNumber++;
                var index = line.IndexOf(text, StringComparison.CurrentCultureIgnoreCase);
                if (index < 0) continue;

                var start = index - 20;
                var length = 20 + text.Length + 20;
                if (start < 0) start = 0;
                if (start + length < 70) length = 70 - start;
                if (start + length >= line.Length) length = line.Length - start;

                if (!filePrinted)
                {
                    filePrinted = true;
                    var current = Directory.GetCurrentDirectory();
                    if (file.StartsWith(current))
                    {
                        file = file.Substring(current.Length + 1);
                    } // if
                    Console.WriteLine($"------ {file}");
                } // if

                Console.WriteLine($"{lineNumber,5:D}: {line.Substring(start, length)}");
            } // foreach

            if (filePrinted) Console.WriteLine();

            return filePrinted;
        } // FindInFile
    } // class Find
} // namespace
