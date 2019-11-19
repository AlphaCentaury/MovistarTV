using IpTviewr.UiServices.Configuration.IpTvService;

namespace IpTviewr.IpTvServices.Implementation
{
    public class ServiceTexts: ITvServiceTexts
    {
        public ITvServiceProviderTexts Provider { get; private set; }

        public void Initialize()
        {
            Provider = GetProviderTexts();
        } // Initialize

        protected virtual ITvServiceProviderTexts GetProviderTexts() => new ServiceProviderTexts();
    } // class ServiceTexts
} // namespace
