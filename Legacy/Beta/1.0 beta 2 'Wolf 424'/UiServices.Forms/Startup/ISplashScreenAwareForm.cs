using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.DvbIpTv.UiServices.Forms.Startup
{
    public interface ISplashScreenAwareForm : IDisposable
    {
        event EventHandler FormLoadCompleted;
        bool DisposeOnFormClose
        {
            get;
        } // DisposeOnClose
    } // ISplashScreenAwareForm
} // namespace
