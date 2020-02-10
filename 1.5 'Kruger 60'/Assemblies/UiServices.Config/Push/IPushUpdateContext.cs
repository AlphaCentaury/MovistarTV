using System;

namespace IpTviewr.UiServices.Configuration.Push
{
    public interface IPushUpdateContext: IPushContext
    {
        Version GetAppVersion();

        DateTime LastChecked { get; set; }
    } // interface IPushUpdateContext
} // namespace