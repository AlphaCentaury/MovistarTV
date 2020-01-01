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
using System.Diagnostics;
using System.IO;
using System.Text;

namespace IpTviewr.RecorderLauncher
{
    internal static class Logger
    {
        private static object _syncLock;
        private static string _logFilename;
        private static DateTime _startTime;
        private static int _processId;

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
            _logFilename = logFilename;
            _syncLock = new object();
            MinLevel = minLevel;
            _startTime = DateTime.Now;

            using (var process = Process.GetCurrentProcess())
            {
                _processId = process.Id;
            } // using process

            if (!File.Exists(logFilename))
            {
                var folder = Path.GetDirectoryName(logFilename);
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                } // if
            } // if

            WriteDate("PROGRAM-START", $"ProcessId={_processId}", false);
        } // Start

        /// <remarks>NOT THREAD SAFE. Call from main() just before returning the exit code</remarks>
        public static void Stop(int exitCode)
        {
            if (_syncLock == null) return;

            WriteDate("PROGRAM-STOP ", $"ProcessId={_processId} & ExitCode={exitCode}", true);

            _syncLock = null;
            _logFilename = null;
        } // Stop

        #region Log methods

        public static Level MinLevel { get; private set; }

        public static void Log(Level level, string text)
        {
            if (_syncLock == null) return;
            if (level < MinLevel) return;

            Write(level, text, null);
        } // Log

        public static void Log(Level level, string format, params object[] args)
        {
            if (_syncLock == null) return;
            if (level < MinLevel) return;

            Write(level, format, args);
        } // Log

        public static void Exception(Exception ex)
        {
            if (_syncLock == null) return;

            Write(Level.Exception, ex.ToString(), null);
        } // Exception

        public static void Exception(Exception ex, string context)
        {
            if (_syncLock == null) return;

            Write(Level.Exception, Properties.Texts.LoggerExceptionFormat, context, ex.ToString());
        } // Exception

        public static void Exception(Exception ex, string context, params object[] args)
        {
            if (_syncLock == null) return;

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
                buffer.AppendFormat("{0:X8}", _processId);
                buffer.Append(' ');
                buffer.AppendFormat("{0:O}", DateTime.Now);
                buffer.Append(' ');
                buffer.AppendLine(text);
                if (newLine) buffer.AppendLine();

                lock (_syncLock)
                {
                    File.AppendAllText(_logFilename, buffer.ToString(), Encoding.UTF8);
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
                var ellapsed = DateTime.Now - _startTime;
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
                buffer.AppendFormat("{0:X8}", _processId);
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

                lock (_syncLock)
                {
                    File.AppendAllText(_logFilename, buffer.ToString(), Encoding.UTF8);
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
