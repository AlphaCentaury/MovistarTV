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
using System.Windows.Forms;

namespace IpTviewr.Common
{
    public class HandleExceptionEventArgs: EventArgs
    {
        public Form OwnerForm { get; set; }
        public string Caption { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }

        public HandleExceptionEventArgs()
        {
            // no-op
        } // default constructor

        public HandleExceptionEventArgs(Form ownerForm, string caption, string message, Exception ex)
        {
            OwnerForm = ownerForm;
            Caption = caption;
            Message = message;
            Exception = ex;
        } // constructor
    } // class HandleExceptionEventArgs
} // namespace
