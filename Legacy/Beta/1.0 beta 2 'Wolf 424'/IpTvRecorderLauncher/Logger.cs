// Copyright (C) 2014, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Project.DvbIpTv.RecorderLauncher
{
    internal static class Logger
    {
        private static object syncLock;
        private static string logFilename;
        private static DateTime startTime;
        private static int processId;
        private static Level minLevel;

        public enum Level
        {
            Verbose = 0,
            Info = 1,
            Warning = 2,
            Error = 3,
            Exception = 9
        } // Level

        /// <remarks>NOT THREAD SAFE. Call from MAIN thread before being used in other threads</remarks>
        public static void Start(string logFilename, Level minLevel)
        {
            Logger.logFilename = logFilename;
            Logger.syncLock = new object();
            Logger.minLevel = minLevel;
            startTime = DateTime.Now;

            using (var process = Process.GetCurrentProcess())
            {
                processId = process.Id;
            } // using process

            if (!File.Exists(logFilename))
            {
                var folder = Path.GetDirectoryName(logFilename);
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                } // if
            } // if

            WriteDate("PROGRAM-START", string.Format("ProcessId={0}", processId), false);
        } // Start

        /// <remarks>NOT THREAD SAFE. Call from main() just before returning the exit code</remarks>
        public static void Stop(int exitCode)
        {
            if (syncLock == null) return;

            WriteDate("PROGRAM-STOP ", string.Format("ProcessId={0} & ExitCode={1}", processId, exitCode), true);

            syncLock = null;
            logFilename = null;
        } // Stop

        #region Log methods

        public static Level MinLevel
        {
            get { return minLevel; }
        } // MinLevel

        public static void Log(Level level, string text)
        {
            if (syncLock == null) return;
            if (level < minLevel) return;

            Write(level, text, null);
        } // Log

        public static void Log(Level level, string format, params object[] args)
        {
            if (syncLock == null) return;
            if (level < minLevel) return;

            Write(level, format, args);
        } // Log

        public static void Exception(Exception ex)
        {
            if (syncLock == null) return;

            Write(Level.Exception, ex.ToString(), null);
        } // Exception

        public static void Exception(Exception ex, string context)
        {
            if (syncLock == null) return;

            Write(Level.Exception, Properties.Texts.LoggerExceptionFormat, context, ex.ToString());
        } // Exception

        public static void Exception(Exception ex, string context, params object[] args)
        {
            if (syncLock == null) return;

            Write(Level.Exception, Properties.Texts.LoggerExceptionFormat, string.Format(context, args), ex.ToString());
        } // Exception

        #endregion

        #region Implementation

        private static void WriteDate(string specialOperation, string text, bool newLine)
        {
            try
            {
                var buffer = new StringBuilder();
                buffer.Append(specialOperation);
                buffer.Append(' ');
                buffer.AppendFormat("{0:X8}", processId);
                buffer.Append(' ');
                buffer.AppendFormat("{0:O}", DateTime.Now);
                buffer.Append(' ');
                buffer.AppendLine(text);
                if (newLine) buffer.AppendLine();

                lock (syncLock)
                {
                    File.AppendAllText(logFilename, buffer.ToString(), Encoding.UTF8);
                } // lock
            }
            catch
            {
                // ignore if we can't write to log file
            } // try-catch
        } // WriteDate

        private static void Write(Level level, string text, params object[] args)
        {
            try
            {
                var ellapsed = DateTime.Now - startTime;
                var buffer = new StringBuilder();

                switch (level)
                {
                    case Level.Verbose: buffer.Append("----"); break;
                    case Level.Info: buffer.Append("Info"); break;
                    case Level.Warning: buffer.Append("Warn"); break;
                    case Level.Error: buffer.Append("ERR "); break;
                    case Level.Exception: buffer.Append("EXCP"); break;
                    default:
                        buffer.Append("???? "); break;
                } // switch

                buffer.Append(' ');
                buffer.AppendFormat("{0:X8}", processId);
                buffer.Append(' ');

                var format = (ellapsed.Days > 0) ? "P{0:00}DT{1:00}H{2:00}M{3:00}.{4:000}S" : "PT{1:00}H{2:00}M{3:00}.{4:000}S";
                buffer.AppendFormat(format, ellapsed.Days, ellapsed.Hours, ellapsed.Minutes, ellapsed.Seconds, ellapsed.Milliseconds);
                buffer.Append(' ');

                if ((args == null) || (args.Length == 0))
                {
                    buffer.Append(text);
                }
                else
                {
                    buffer.AppendFormat(text, args);
                }// if-else

                buffer.Replace("\r\n", "\r\n  >> ");
                buffer.AppendLine();

                lock (syncLock)
                {
                    File.AppendAllText(logFilename, buffer.ToString(), Encoding.UTF8);
                } // lock
            }
            catch
            {
                // ignore if we can't write to log file
            } // try-catch
        } // Write

        #endregion
    } // class Logger
} // namespace
