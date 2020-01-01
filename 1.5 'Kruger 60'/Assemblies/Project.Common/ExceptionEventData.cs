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

namespace IpTviewr.Common
{
    public class ExceptionEventData
    {
        public string Caption { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }

        public ExceptionEventData()
        {
            // no-op
        } // default constructor

        public ExceptionEventData(Exception ex)
        {
            Exception = ex;
        } // default constructor

        public ExceptionEventData(string message, Exception ex)
        {
            Message = message;
            Exception = ex;
        } // constructor

        public ExceptionEventData(string caption, string message, Exception ex)
        {
            Caption = caption;
            Message = message;
            Exception = ex;
        } // constructor
    } // class ExceptionEventData
} // namespace
