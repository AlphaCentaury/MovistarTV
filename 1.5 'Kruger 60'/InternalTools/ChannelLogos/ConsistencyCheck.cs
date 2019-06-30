// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace IpTviewr.Internal.Tools.ChannelLogos
{
    internal abstract class ConsistencyCheck
    {
        protected Form Owner;

        public class ProgressChangedEventArgs: EventArgs
        {
            public string[] Messages;
        } // class ProgressChangedEventArgs

        public event EventHandler<ProgressChangedEventArgs> ProgressChanged;

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
                private set;
            } // Timestamp

            public Severity Severity
            {
                get;
                private set;
            } // Severity

            public string[] Data
            {
                get;
                private set;
            } // Data

            public Result(Severity severity, params string[] data)
            {
                Timestamp = DateTime.Now;
                Severity = severity;
                Data = data;
            } // constructor
        } // class Result

        public ConsistencyCheck()
        {
            Results = new List<Result>();
            StartTime = DateTime.Now;
        } // constructor

        public DateTime StartTime
        {
            get;
            private set;
        } // Start Time

        public IList<Result> Results
        {
            get;
            private set;
        } // Results

        public void Execute(Form owner)
        {
            Owner = owner;
            Run();
            Owner = null;
        } // Execute

        protected abstract void Run();

        protected void AddResult(Severity severity, params string[] data)
        {
            Results.Add(new Result(severity, data));

            if (severity != Severity.Info) return;

            var e = new ProgressChangedEventArgs
            {
                Messages = data
            };

            OnProgressChanged(this, e);
        } // AddResult

        protected virtual void OnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressChanged?.Invoke(sender, e);
        } // OnProgressChanged
    } // abstract class ConsistencyCheck
} // namespace
