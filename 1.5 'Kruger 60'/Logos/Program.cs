// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using IpTviewr.Common;
using IpTViewr.Internal.Logos.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IpTViewr.Internal.Logos
{
    static class Program
    {
        public static AssemblyName ToolName;
        private static bool NoLogo;

        public enum Result
        {
            Exception = -10, // unhandled exception occurred
            Arguments = -1, // missing arguments or bad arguments
            Ok = 0,
            Help = 1, // help requested
        } // enum Result

        static int Main(string[] args)
        {
            Result result;

            try
            {
                // Initial setup
                ToolName = Assembly.GetEntryAssembly().GetName();
                Console.Title = Properties.Texts.ProgramName;
                var arguments = GetArguments(args);
                if (arguments == null) return (int)Result.Arguments;

                DisplayLogo(NoLogo);

                if (arguments.ContainsKey("help"))
                {
                    DisplayHelp();
                    return (int)Result.Help;
                } // if

                var program = new PackLogos();
                if (program.ProcessArguments(arguments))
                {
                    result = program.Execute();
                }
                else
                {
                    result = Result.Arguments;
                } // if-else
            }
            catch (Exception ex)
            {
                DisplayException(ex, true);
                result = Result.Exception;
            } // try-catch

            return (int) result;
        } // Main

        public static void DisplayError(string text, bool isFatal = false)
        {
            Console.WriteLine(isFatal ? Texts.DisplayFatalErrorFormat : Texts.DisplayErrorFormat, ToolName.Name, text);
        } // DisplayError

        public static void DisplayException(Exception ex, bool isFatal = false)
        {
            Console.WriteLine(isFatal? Texts.DisplayFatalExceptionFormat : Texts.DisplayExceptionFormat, ToolName.Name, ex.GetType().Name, ex.Message.Replace("\r\n", " "));
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

            NoLogo = parser.Switches.ContainsKey("nologo");

            return parser.Switches;
        } // GetArguments

        private static void DisplayLogo(bool noLogo = false)
        {
            string copyright;

            if (noLogo) return;

            // get copyright text
            object[] attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
            copyright = (attributes.Length > 0)? ((AssemblyCopyrightAttribute)attributes[0]).Copyright : "Copyright (C) http://www.alphacentaury.org";

            Console.WriteLine(Properties.Texts.StartLogo, Properties.Texts.ProgramName, ToolName.Version, copyright);
            Console.WriteLine();
        } // DisplayLogo

        private static void DisplayHelp()
        {
            Console.WriteLine(Properties.Texts.ProgramHelp, ToolName.Name);
        } // DisplayHelp
    } // class Program
} // namespace
