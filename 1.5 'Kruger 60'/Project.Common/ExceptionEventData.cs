using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
