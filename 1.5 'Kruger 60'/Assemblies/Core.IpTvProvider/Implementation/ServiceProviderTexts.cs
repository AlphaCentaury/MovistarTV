using IpTviewr.IpTvServices.Properties;
using IpTviewr.UiServices.Configuration.IpTvService;

namespace IpTviewr.IpTvServices.Implementation
{
    public class ServiceProviderTexts : ITvServiceProviderTexts
    {
        public string MenuEntry => Texts.ProviderMenuEntry;
        public string MenuSelect => Texts.ProviderMenuSelect;
        public string MenuDetails => Texts.ProviderMenuDetails;
        public string NoSelection => Texts.ProviderNoSelection;
        public string ListRefreshError => Texts.ProviderListRefreshError;
        public string ObtainingList => Texts.ProviderObtainingList;
        public string ParsingList => Texts.ProviderParsingList;
        public string PropertiesCaption => Texts.ProviderPropertiesCaption;
        public string LogoLoadError => Texts.ProviderLogoLoadError;
        public string LogoNotFound => Texts.ProviderLogoNotFound;
        public string UnknownDisplayDescription => Texts.ProviderUnknownDisplayDescription;
        public string FormatFriendlyName => Texts.ProviderFormatFriendlyName;
        public string FormatUnknownName => Texts.ProviderFormatUnknownName;
        public string SelectCaption => Texts.ProviderSelectCaption;
    } // class ServiceProviderTexts;
} // namespace
