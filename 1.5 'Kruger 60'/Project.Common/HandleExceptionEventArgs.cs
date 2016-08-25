using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IpTviewr.Common
{
    public class HandleExceptionEventArgs: EventArgs
    {
        public IWin32Window OwnerWindow { get; set; }
        public string Caption { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }

        public HandleExceptionEventArgs()
        {
            // no-op
        } // default constructor

        public HandleExceptionEventArgs(IWin32Window ownerWindow, string caption, string message, Exception ex)
        {
            OwnerWindow = ownerWindow;
            Caption = caption;
            Message = message;
            Exception = ex;
        } // constructor
    } // class HandleExceptionEventArgs
} // namespace
