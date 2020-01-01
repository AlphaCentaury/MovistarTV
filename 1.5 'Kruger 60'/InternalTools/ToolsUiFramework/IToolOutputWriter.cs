using System;
using System.Diagnostics;
using JetBrains.Annotations;

namespace IpTviewr.Internal.Tools.UiFramework
{
    [PublicAPI]
    public interface IToolOutputWriter
    {
        void WriteLine(TraceLevel level = TraceLevel.Info);

        void WriteLine(string text, TraceLevel level = TraceLevel.Info);
        
        void WriteLine(string format, params object[] objects);

        void WriteLine(TraceLevel level, string format, params object[] objects);

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