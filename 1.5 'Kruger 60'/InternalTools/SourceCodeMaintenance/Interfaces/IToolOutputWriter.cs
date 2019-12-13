using System;
using JetBrains.Annotations;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Interfaces
{
    [PublicAPI]
    public interface IToolOutputWriter
    {
        void WriteLine();
        void WriteLine(string text);
        void WriteLine(string format, params object[] objects);
        void WriteException(Exception ex, string message = null);
        int IncreaseIndent();
        int DecreaseIndent();
        bool WriteTimestamps { get; set; }
        bool AbsoluteTimestamps { get; set; }
        TimeSpan ElapsedTime { get; }
        DateTime UtcStartTime { get; }
        int IndentLevel { get; }
    } // IToolOutputWriter
} // namespace