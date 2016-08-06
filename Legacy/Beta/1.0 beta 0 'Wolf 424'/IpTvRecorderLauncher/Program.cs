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
        private static string TaskXmlFilename;
        private static ProgramMode Mode;

        enum ProgramMode
        {
            Record = 0,
            Help = 10,
        } // ProgramMode

        public enum Result
        {
            Exception = -10,
            Arguments = -1,
            Ok = 0,
            Help = 1,
            XmlFile = 10,
            TooLate = 100,
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
                DisplayException(ex);
                result = Result.Exception;
            } // try-catch

            if (PressAnyKey)
            {
                Console.WriteLine();
                Console.Write(Properties.Texts.PressAnyKeyEnd);
                Console.Write(" ");
                Console.ReadKey(true);
            } // if

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
                    return launcher.Start(TaskXmlFilename);

                default:
                    return Result.Arguments;
            } // switch
        } // Run

        static bool ProcessArguments(string[] args)
        {
            int skip;

            if ((args == null) || (args.Length == 0))
            {
                DisplayLogo();
                Console.WriteLine(Properties.Texts.ErrorNoArguments);
                return false;
            } // if

            var arg1 = args[0].Trim();
            if (arg1 == "")
            {
                DisplayLogo();
                Console.WriteLine(Properties.Texts.ErrorNoArguments);
                return false;
            } // if

            skip = 0;
            if (!arg1.StartsWith("/") && !arg1.StartsWith("-"))
            {
                skip = 1;
                Mode = ProgramMode.Record;
                TaskXmlFilename = arg1;
            } // if

            foreach (var arg in args.Skip(skip))
            {
                var partialArg = arg.Substring(1, (arg.Length <= 10) ? (arg.Length - 1) : 10).ToLower();
                if ((arg[0] == '/') || (arg[0] == '-'))
                {
                    if (partialArg.StartsWith("nologo"))
                    {
                        NoLogo = true;
                        continue;
                    } // if
                    if ((partialArg.StartsWith("h")) || (partialArg.StartsWith("?")))
                    {
                        Mode = ProgramMode.Help;
                        continue;
                    } // if
                }
                else
                {
                    return false;
                } // if-else
            } // foreach arg

            return true;
        } // ProcessArguments

        public static void DisplayException(Exception ex)
        {
            Console.WriteLine(Properties.Texts.DisplayExceptionFormat, null, ex.ToString(true, false));
        } // DisplayException

        static void DisplayLogo()
        {
            Console.WriteLine(Properties.Texts.StartLogo, Assembly.GetEntryAssembly().GetName().Version);
            Console.WriteLine();
        } // DisplayLogo

        static void DisplayHelp()
        {
            PressAnyKey = false;
            Console.WriteLine(Properties.Texts.ProgramHelp, Assembly.GetEntryAssembly().GetName().Name);
        } // DisplayHelp
    } // class Program
} // namespace
