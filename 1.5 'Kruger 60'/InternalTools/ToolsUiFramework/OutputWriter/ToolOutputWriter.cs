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

namespace IpTviewr.Internal.Tools.UiFramework.OutputWriter
{
    public abstract class ToolOutputWriter : IToolOutputWriter
    {
        private bool _started;

        public TraceLevel Level { get; set; }

        public bool Started
        {
            get => _started;
            protected set
            {
                _started = value;
                if (_started) UtcStartTime = DateTime.UtcNow;
            } // set
        } // Started

        #region IToolOuputWriter implementation

        public virtual bool WriteTimestamps { get; set; }

        public virtual bool AbsoluteTimestamps { get; set; }

        public TimeSpan ElapsedTime => (DateTime.UtcNow - UtcStartTime);

        public DateTime UtcStartTime { get; private set; }

        public int IndentLevel { get; private set; }

        public virtual int IncreaseIndent()
        {
            if (!Started) return 0;

            return (++IndentLevel);
        } // IncreaseIndent

        public virtual int DecreaseIndent()
        {
            if (!Started) return 0;

            IndentLevel--;
            if (IndentLevel < 0) IndentLevel = 0;

            return IndentLevel;
        } // DecreaseIndent

        public virtual void WriteException(Exception ex, string message = null)
        {
            throw new NotImplementedException();
        } // WriteException

        public void WriteLine(TraceLevel level = TraceLevel.Info)
        {
            if (level > Level) return;
            throw new NotImplementedException();
        } // WriteLine

        public void WriteLine(string text, TraceLevel level = TraceLevel.Info)
        {
            if (level > Level) return;

            throw new NotImplementedException();
        } // WriteLine

        public void WriteLine(string format, params object[] objects) => WriteLine(TraceLevel.Info, format, objects);

        public void WriteLine(TraceLevel level, string format, params object[] objects)
        {
            if (level > Level) return;

            throw new NotImplementedException();
        } // WriteLine

        #endregion

        #region Protected implementation



        #endregion
    } // class ToolOutputWriter
} // namespace
