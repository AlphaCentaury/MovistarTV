using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IpTviewr.UiServices.Common.Start
{
    public interface ISplashScreen
    {
        Form SplashForm { get; }
        void DisplayProgress(string text);
        void DisplayMessage(string caption, string message, MessageBoxIcon icon);
        void DisplayException(string caption, string message, Exception exception);
        void Ready(Form form);
        void Invoke(Delegate method, params object[] args);
    } // interface ISplashScreen
} // namespace
