// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
