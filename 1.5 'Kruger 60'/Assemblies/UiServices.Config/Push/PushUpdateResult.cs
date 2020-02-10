using IpTviewr.UiServices.Configuration.Push.v1;

namespace IpTviewr.UiServices.Configuration.Push
{
    public sealed class PushUpdateResult
    {
        public IPushUpdateContext Context { get; set; }

        public PushUpdates Updates { get; set; }
        
        public PushUpdate LastUpdate { get; set; }
    } // class PushUpdateResult
} // namespace