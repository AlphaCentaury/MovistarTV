using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Helpers
{
    public abstract class BaseOutputWriter
    {
        private readonly string[] _indents;
        protected bool _writeTimestamps;
        protected bool _absoluteTimestamps;
        private bool _started;

        protected BaseOutputWriter(int indentSize)
        {
            if (indentSize < 1) throw new ArgumentOutOfRangeException(nameof(indentSize));

            _indents = new string[9];
            IndentSize = indentSize;

            for (var indent = 0; indent < _indents.Length; indent++)
            {
                _indents[indent] = new string(' ', (indent + 1) * indentSize);
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

        public void WriteTimestamps(bool enable, bool absolute)
        {
            _writeTimestamps = enable;
            _absoluteTimestamps = absolute;
        } // WriteTimestamps

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

        protected string GetTimestamp()
        {
            if (!_writeTimestamps) return string.Empty;

            if (!_absoluteTimestamps) return $"[{ElapsedTime:g}]{(IndentLevel > 0 ? "" : " ")}";

            var now = DateTime.UtcNow;
            return $"[{now.ToShortDateString()} {now:HH:mm:ss.ff tt zz}]{(IndentLevel > 0 ? "" : " ")}";
        } // GetTimestamp

        protected string GetIndent()
        {
            return (IndentLevel < _indents.Length) ? _indents[IndentLevel] : new string(' ', IndentLevel * IndentSize);
        } // GetIndent

        protected void AppendText(StringBuilder buffer, string text)
        {
            if (!_writeTimestamps)
            {
                buffer.AppendFormat("{0}{1}", (IndentLevel > 0) ? "" : " ", GetIndent());
                buffer.AppendLine(text);
                return;
            } // if

            if (_absoluteTimestamps)
            {
                var now = DateTime.UtcNow;
                buffer.AppendFormat("[{0} {1:HH:mm:ss.ff tt zz}]{2}{3}", now.ToShortDateString(), DateTime.UtcNow, (IndentLevel > 0) ? "" : " ", GetIndent());
            }
            else
            {
                buffer.AppendFormat("[{0:g}{1}{2}", ElapsedTime, (IndentLevel > 0) ? "" : " ", GetIndent());
            } // if

            if (text != null) buffer.AppendLine(text);
        } // AppendText
    } // class BaseOutputWriter
} // namespace
