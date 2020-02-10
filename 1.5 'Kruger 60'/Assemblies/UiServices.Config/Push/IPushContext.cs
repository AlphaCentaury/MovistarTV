using System;

namespace IpTviewr.UiServices.Configuration.Push
{
    public interface IPushContext
    {
        bool IsHidden(Guid message);

        void AddHidden(Guid message);
    } // interface IPushContext
} // namespace