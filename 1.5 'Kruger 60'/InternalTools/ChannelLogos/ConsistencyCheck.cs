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
using System.Windows.Forms;

namespace IpTviewr.Internal.Tools.ChannelLogos
{
    internal abstract class ConsistencyCheck
    {
        public class ProgressChangedEventArgs : EventArgs
        {
            public string[] Messages;
        } // class ProgressChangedEventArgs

        public enum Severity
        {
            None = 0,
            Info = 1,
            Ok = 2,
            Warning = 3,
            Error = 4
        } // Severity

        public sealed class Result
        {
            public DateTime Timestamp
            {
                get;
            } // Timestamp

            public Severity Severity
            {
                get;
            } // Severity

            public string[] Data
            {
                get;
            } // Data

            public Result(Severity severity, params string[] data)
            {
                Timestamp = DateTime.Now;
                Severity = severity;
                Data = data;
            } // constructor
        } // class Result

        protected ConsistencyCheck()
        {
            Results = new List<Result>();
            StartTime = DateTime.Now;
        } // constructor

        public event EventHandler<ProgressChangedEventArgs> ProgressChanged;

        public ConsistencyChecksData Data { get; set; }

        public DateTime StartTime
        {
            get;
        } // Start Time

        public IList<Result> Results
        {
            get;
        } // Results

        public void Execute(ConsistencyChecksData data)
        {
            try
            {
                Data = data ?? throw new ArgumentNullException(nameof(data));
                AddResult(Severity.Info, "Check started");
                Run();
            }
            catch (Exception e)
            {
                AddResult(Severity.Error, "Exception", e.Message);
            } // try-catch

            AddResult(Severity.Info, "Check ended");
        } // Execute

        protected abstract void Run();

        protected void AddResult(Severity severity, params string[] data)
        {
            Results.Add(new Result(severity, data));

            if (severity != Severity.Info) return;

            OnProgressChanged(this, new ConsistencyCheck.ProgressChangedEventArgs { Messages = data });
        } // // AddResult

        protected virtual void OnProgressChanged(ConsistencyCheck sender, ProgressChangedEventArgs e)
        {
            ProgressChanged?.Invoke(sender, e);
        } // OnProgressChanged
    } // abstract class ConsistencyCheck
} // namespace
