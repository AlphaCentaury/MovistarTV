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
using System.Collections.Generic;
using System.Reflection;
using IpTviewr.Common;
using IpTViewr.Setup.Logos.Properties;

namespace IpTViewr.Setup.Logos
{
    internal static class Program
    {
        public static AssemblyName ToolName;
        public static Assembly EntryAssembly;

        public enum Result
        {
            Exception = -10, // unhandled exception occurred
            Arguments = -1, // missing arguments or bad arguments
            Ok = 0,
            Help = 1, // help requested
        } // enum Result

        public static bool Verbose { get; private set; }

        public static bool NoLogo { get; private set; }

        private static int Main(string[] args)
        {
            Result result;

            try
            {
                // Initial setup
                EntryAssembly = Assembly.GetEntryAssembly() ?? Assembly.GetAssembly(typeof(Program));
                ToolName = EntryAssembly.GetName();
                Console.Title = Texts.ProgramName;
                var arguments = GetArguments(args);
                if (arguments == null) return (int)Result.Arguments;

                DisplayLogo();

                if (arguments.ContainsKey("help"))
                {
                    DisplayHelp();
                    return (int)Result.Help;
                } // if

                var program = new PackLogos();
                result = program.ProcessArguments(arguments) ? program.Execute() : Result.Arguments;
            }
            catch (Exception ex)
            {
                DisplayException(ex, true);
                result = Result.Exception;
            } // try-catch

            return (int)result;
        } // Main

        public static void DisplayError(string text, bool isFatal = false)
        {
            Console.WriteLine(isFatal ? Texts.DisplayFatalErrorFormat : Texts.DisplayErrorFormat, ToolName.Name, text);
        } // DisplayError

        public static void DisplayException(Exception ex, bool isFatal = false)
        {
            Console.WriteLine(isFatal ? Texts.DisplayFatalExceptionFormat : Texts.DisplayExceptionFormat, ToolName.Name, ex.GetType().Name, ex.Message.Replace("\r\n", " "));
        } // DisplayException

        private static IDictionary<string, string> GetArguments(string[] args)
        {
            var parser = new CommandLineArguments()
            {
                SpecialHelpArgument = true
            };

            parser.Parse(args);
            if (!parser.IsOk)
            {
                Console.WriteLine(Texts.InvalidArgumentFormat);

                return null;
            } // if

            NoLogo = parser.Switches.ContainsKey(@"nologo");
            Verbose = parser.Switches.ContainsKey(@"verbose");

            return parser.Switches;
        } // GetArguments

        private static void DisplayLogo()
        {
            if (NoLogo) return;

            // get copyright text
            var attributes = EntryAssembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
            var copyright = (attributes.Length > 0) ? ((AssemblyCopyrightAttribute)attributes[0]).Copyright : "Copyright (C) http://www.alphacentaury.org";

            Console.WriteLine(Texts.StartLogo, Texts.ProgramName, ToolName.Version, copyright);
            Console.WriteLine();
        } // DisplayLogo

        private static void DisplayHelp()
        {
            Console.WriteLine(Texts.ProgramHelp, ToolName.Name);
        } // DisplayHelp
    } // class Program
} // namespace
