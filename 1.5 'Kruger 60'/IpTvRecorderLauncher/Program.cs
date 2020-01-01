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
using System.Collections.Generic;
using System.Reflection;

namespace IpTviewr.RecorderLauncher
{
    class Program
    {
        private static AssemblyName _toolName;
        private static bool _pressAnyKey;
        private static bool _noLogo;
        private static Guid _taskId;
        private static string _dbFile;
        private static string _logFolder;
        private static ProgramMode _mode;

        enum ProgramMode
        {
            None = 0,
            Record = 10,
            Help = -1,
        } // ProgramMode

        public enum Result
        {
            Exception = -10, // unhandled exception occurred
            Arguments = -1, // missing arguments or bad arguments
            Ok = 0,
            Help = 1, // help requested
            XmlFile = 10, // exception/error while loading task XML file
            TooLate = 100, // recording beyond the scheduled end date/time
            ExecProblem = 200, // exception while launching recorder
            ExecFailure = 250, // recorder exit code != 0
        } // enum Result

        static int Main(string[] args)
        {
            Result result;

            try
            {
                result = Run(args);
            }
            catch (Exception ex)
            {
                Logger.Exception(ex);
                result = Result.Exception;
            } // try-catch

            if (_pressAnyKey)
            {
                Console.WriteLine();
                Console.Write(Properties.Texts.PressAnyKeyEnd);
                Console.Write(" ");
                Console.ReadKey(true);
            } // if

            Logger.Stop((int)result);
            return (int)result;
        } // Main

        static Result Run(string[] args)
        {
            // Set console icon
            using (var icon = Properties.Resources.RecorderAppIcon)
            {
                UnsafeNativeMethods.SetConsoleIcon(icon.Handle);
            } // using icon

            // Initial setup
            _toolName = Assembly.GetEntryAssembly().GetName();
            Console.Title = Properties.Texts.ProgramCaption;
            _pressAnyKey = true;

            if (!ProcessArguments(args))
            {
                return Result.Arguments;
            } // if

            if (!_noLogo)
            {
                DisplayLogo();
            } // if

            switch (_mode)
            {
                case ProgramMode.Help:
                    DisplayHelp();
                    return Result.Help;

                case ProgramMode.Record:
                    _pressAnyKey = false;
                    var launcher = new Launcher();
                    return launcher.Run(_taskId, _dbFile, _logFolder);

                default:
                    return Result.Arguments;
            } // switch
        } // Run

        static bool ProcessArguments(string[] args)
        {
            if ((args == null) || (args.Length == 0))
            {
                DisplayLogo();
                Console.WriteLine(Properties.Texts.ErrorNoArguments);
                return false;
            } // if

            var parser = new CommandLineArguments()
            {
                SpecialHelpArgument = true
            };

            parser.Parse(args);
            if (!parser.IsOk)
            {
                DisplayLogo();
                Console.WriteLine(Properties.Texts.InvalidArgumentFormat);
                return false;
            } // if

            if (parser.Switches.ContainsKey("nologo"))
            {
                _noLogo = true;
            } // if

            if (parser.Switches.ContainsKey("help"))
            {
                _mode = ProgramMode.Help;
                return true;
            }
            else
            {
                return ProcessArguments(parser.Switches);
            } // if-else
        } // ProcessArguments

        static bool ProcessArguments(IDictionary<string, string> arguments)
        {
            string value;

            if (arguments.TryGetValue("TaskId", out value))
            {
                _taskId = new Guid(value);
            } // if

            if (arguments.TryGetValue("Database", out value))
            {
                _dbFile = value;
            } // if

            if (arguments.TryGetValue("LogFolder", out value))
            {
                _logFolder = value;
            } // if

            if (arguments.TryGetValue("Action", out value))
            {
                if (string.Compare(value, "Record", true) == 0)
                {
                    _mode = ProgramMode.Record;
                }
                else
                {
                    // Unknown action
                    _mode = ProgramMode.None;
                } // if-else
            } // if

            // TODO: display error message if arguments validation fails

            // validate general arguments
            if (!string.IsNullOrEmpty(_logFolder))
            {
                if (!System.IO.Directory.Exists(_logFolder)) return false;
            } // if

            // validate record mode arguments
            if (_mode == ProgramMode.Record)
            {
                if (_taskId == Guid.Empty) return false;
                if (string.IsNullOrEmpty(_dbFile)) return false;
                if (!System.IO.File.Exists(_dbFile)) return false;
            } // if

            return true;
        } // ProcessArguments

        public static void DisplayException(Exception ex)
        {
            Console.WriteLine(Properties.Texts.DisplayExceptionFormat, null, ex.ToString(true, false));
        } // DisplayException

        private static void DisplayLogo()
        {
            string copyright;

            // get copyright text
            var attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
            copyright = (attributes.Length > 0) ? ((AssemblyCopyrightAttribute)attributes[0]).Copyright : "Copyright (C) http://movistartv.alphacentaury.org";

            Console.WriteLine(Properties.Texts.StartLogo, Properties.Texts.ProgramCaption, _toolName.Version, copyright);
            Console.WriteLine();
        } // DisplayLogo

        private static void DisplayHelp()
        {
            Console.WriteLine(Properties.Texts.ProgramHelp, _toolName.Name);
        } // DisplayHelp
    } // class Program
} // namespace
