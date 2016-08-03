using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.DvbIpTv.UiServices.Configuration
{
    public class InitializationResult
    {
        /// <summary>
        /// Gets an <see cref="InitializationResult"/> with IsOk set to <see cref="true"/> and all remaining fields set to <see cref="null"/>
        /// </summary>
        public static InitializationResult Ok
        {
            get
            {
                return new InitializationResult()
                {
                    IsOk = true
                };
            } // get
        } // Ok

        public bool IsOk
        {
            get;
            set;
        } // IsOk

        public string Caption
        {
            get;
            set;
        } // Caption

        public string Message
        {
            get;
            set;
        } // Message

        public Exception InnerException
        {
            get;
            set;
        } // InnerException
    } // InitializationResult
} // namespace
