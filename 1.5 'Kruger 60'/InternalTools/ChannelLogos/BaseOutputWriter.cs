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
using System.Text;

namespace IpTviewr.Internal.Tools.ChannelLogos
{
    public abstract class BaseOutputWriter
    {
        private readonly string[] _indents;
        private bool _started;

        protected BaseOutputWriter(int indentSize)
        {
            if (indentSize < 1) throw new ArgumentOutOfRangeException(nameof(indentSize));

            _indents = new string[9];
            IndentSize = indentSize;

            for (var indent = 0; indent < _indents.Length; indent++)
            {
                _indents[indent] = new string(' ', indent * indentSize);
            } // for indent
        } // constructor

        public TimeSpan ElapsedTime => (DateTime.UtcNow - UtcStartTime);

        public DateTime UtcStartTime { get; private set; }

        public int IndentLevel { get; private set; }

        public int IndentSize { get; }

        public bool Started
        {
            get => _started;
            protected set
            {
                _started = value;
                if (_started) UtcStartTime = DateTime.UtcNow;
            }
        } // Started

        public abstract void WriteLine(string text);

        public void WriteException(Exception ex, string message = null)
        {
            StringBuilder buffer = null;
            var level = IndentLevel;

            while (ex != null)
            {
                if (buffer == null) buffer = new StringBuilder();
                if (ex is AggregateException aggregateException)
                {
                    ex = aggregateException.Flatten();
                } // if

                buffer.Append(GetIndent());
                buffer.AppendFormat(">>>>>  {0}  <<<<<", ex.GetType().Name);
                buffer.AppendLine();
                if (message != null)
                {
                    buffer.Append(GetIndent());
                    buffer.AppendLine(message);
                    message = null;
                } // if

                IncreaseIndent();

                buffer.Append(GetIndent());
                buffer.AppendLine(ex.Message);

                var trace = ex.StackTrace.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                foreach (var traceLine in trace)
                {
                    buffer.Append(GetIndent());
                    buffer.AppendLine(traceLine);
                } // foreach
                ex = ex.InnerException;
            } // while

            if (buffer != null)
            {
                IndentLevel = 0;
                WriteLine(buffer.ToString());
            } // if

            IndentLevel = level;
        } // WriteException

        public bool WriteTimestamps { get; set; }
        public bool AbsoluteTimestamps { get; set; }

        public int IncreaseIndent()
        {
            if (!Started) return 0;

            return (++IndentLevel);
        } // IncreaseIndent

        public int DecreaseIndent()
        {
            if (!Started) return 0;

            IndentLevel--;
            if (IndentLevel < 0) IndentLevel = 0;

            return IndentLevel;
        } // DecreaseIndent

        protected void Reset()
        {
            IndentLevel = 0;
        } // Reset

        protected string GetTimestamp()
        {
            if (!WriteTimestamps) return string.Empty;

            if (!AbsoluteTimestamps) return $"[{ElapsedTime:g}]{(IndentLevel != 0 ? "" : " ")}";

            var now = DateTime.Now;
            return $"[{now.ToShortDateString()} {now:HH:mm:ss.ff zz}]{(IndentLevel != 0 ? "" : " ")}";
        } // GetTimestamp

        protected string GetIndent()
        {
            return (IndentLevel < _indents.Length) ? _indents[IndentLevel] : new string(' ', IndentLevel * IndentSize);
        } // GetIndent

        protected void AppendText(StringBuilder buffer, string text)
        {
            if (!WriteTimestamps)
            {
                buffer.Append(GetIndent());
                if (text != null) buffer.AppendLine(text);
                return;
            } // if

            if (AbsoluteTimestamps)
            {
                var now = DateTime.Now;
                buffer.AppendFormat("[{0} {1:HH:mm:ss.ff zz}]{2}{3}", now.ToShortDateString(), now, (IndentLevel != 0) ? "" : " ", GetIndent());
            }
            else
            {
                buffer.AppendFormat("[{0:g}]{1}{2}", ElapsedTime, (IndentLevel != 0) ? "" : " ", GetIndent());
            } // if

            if (text != null) buffer.AppendLine(text);
        } // AppendText
    } // class BaseOutputWriter
} // namespace
