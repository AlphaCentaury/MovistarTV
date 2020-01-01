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

namespace IpTviewr.RecorderLauncher
{
    internal static class InternalExtensions
    {
        public static string ToString(this Exception ex, bool withMessage, bool withStackTrace)
        {
            StringBuilder buffer;

            buffer = new StringBuilder();
            ex.ToString(buffer, withMessage, withStackTrace);

            return buffer.ToString();
        } // Exception.ToString

        public static void ToString(this Exception ex, StringBuilder buffer, bool withMessage, bool withStackTrace)
        {
            var message = withMessage ? ex.Message : null;

            buffer.Append(ex.GetType().FullName);
            if (message != null && message.Length > 0)
            {
                buffer.Append(": ");
                buffer.Append(message);
            } // if-else

            if (ex.InnerException != null)
            {
                buffer.Append(Environment.NewLine);
                buffer.Append("  --> ");
                ex.InnerException.ToString(buffer, withMessage, withStackTrace);
                buffer.Append(Environment.NewLine);
                buffer.Append("      ");
                buffer.Append(Properties.Texts.ExceptionEndInnerList);
            } // if

            if (withStackTrace)
            {
                var stackTrace = ex.StackTrace;
                if (stackTrace != null)
                {
                    buffer.Append(Environment.NewLine);
                    buffer.Append(stackTrace);
                } // if
            } // if
        } // Exception.ToString
    } // class Extensions
} // namespace
