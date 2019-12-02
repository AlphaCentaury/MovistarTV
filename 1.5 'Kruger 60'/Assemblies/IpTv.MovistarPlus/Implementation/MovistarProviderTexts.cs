// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using IpTviewr.IpTvServices.MovistarPlus.Properties;

using IpTviewr.UiServices.Configuration.IpTvService;

namespace IpTviewr.IpTvServices.MovistarPlus.Implementation
{
    internal class MovistarProviderTexts : ITvServiceProviderTexts
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
    } // class MovistarProviderTexts
} // namespace
