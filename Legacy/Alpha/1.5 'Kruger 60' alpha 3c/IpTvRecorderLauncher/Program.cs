// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Project.DvbIpTv.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Project.DvbIpTv.RecorderLauncher
{
    class Program
    {
        private static bool PressAnyKey;
        private static bool NoLogo;
        private static Guid TaskId;
        private static string DbFile;
        private static string LogFolder;
        private static ProgramMode Mode;

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

            if (PressAnyKey)
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
            Console.Title = Properties.Texts.ProgramCaption;
            PressAnyKey = true;

            if (!ProcessArguments(args))
            {
                return Result.Arguments;
            } // if

            if (!NoLogo)
            {
                DisplayLogo();
            } // if

            switch (Mode)
            {
                case ProgramMode.Help:
                    DisplayHelp();
                    return Result.Help;

                case ProgramMode.Record:
                    PressAnyKey = false;
                    var launcher = new Launcher();
                    return launcher.Run(TaskId, DbFile, LogFolder);

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

            var arguments = parser.Parse(args);
            if (!parser.IsOk)
            {
                DisplayLogo();
                Console.WriteLine(Properties.Texts.InvalidArgumentFormat);
                return false;
            } // if

            return ProcessArguments(arguments);
        } // ProcessArguments

        static bool ProcessArguments(IDictionary<string, string> arguments)
        {
            string value;

            if (arguments.TryGetValue("nologo", out value))
            {
                NoLogo = true;
            } // if

            if (arguments.TryGetValue("TaskId", out value))
            {
                TaskId = new Guid(value);
            } // if

            if (arguments.TryGetValue("Database", out value))
            {
                DbFile = value;
            } // if

            if (arguments.TryGetValue("LogFolder", out value))
            {
                LogFolder = value;
            } // if

            if (arguments.TryGetValue("Action", out value))
            {
                if (string.Compare(value, "Record", true) == 0)
                {
                    Mode = ProgramMode.Record;
                }
                else
                {
                    // Unknown action
                    Mode = ProgramMode.None;
                } // if-else
            } // if

            // TODO: display error message if arguments validation fails

            // validate general arguments
            if (!string.IsNullOrEmpty(LogFolder))
            {
                if (!System.IO.Directory.Exists(LogFolder)) return false;
            } // if

            // validate record mode arguments
            if (Mode == ProgramMode.Record)
            {
                if (TaskId == Guid.Empty) return false;
                if (string.IsNullOrEmpty(DbFile)) return false;
                if (!System.IO.File.Exists(DbFile)) return false;
            } // if

            return true;
        } // ProcessArguments

        public static void DisplayException(Exception ex)
        {
            Console.WriteLine(Properties.Texts.DisplayExceptionFormat, null, ex.ToString(true, false));
        } // DisplayException

        static void DisplayLogo()
        {
            string copyright;

            // get copyright text
            object[] attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
            if (attributes.Length == 0)
            {
                copyright = "Copyright (C) http://movistartv.codeplex.com";
            }
            copyright = ((AssemblyCopyrightAttribute)attributes[0]).Copyright;

            Console.WriteLine(Properties.Texts.StartLogo, Assembly.GetEntryAssembly().GetName().Version, copyright);
            Console.WriteLine();
        } // DisplayLogo

        static void DisplayHelp()
        {
            PressAnyKey = false;
            Console.WriteLine(Properties.Texts.ProgramHelp, Assembly.GetEntryAssembly().GetName().Name);
        } // DisplayHelp
    } // class Program
} // namespace
