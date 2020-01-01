// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

namespace IpTviewr.IpTvServices
{
    public interface ITvServiceProviderTexts
    {
        string MenuEntry { get; }

        string MenuSelect { get; }

        string MenuDetails { get; }

        string NoSelection { get; }

        string ListRefreshError { get; }

        string ObtainingList { get; }

        string ParsingList { get; }

        string PropertiesCaption { get; }

        string LogoLoadError { get; }

        string LogoNotFound { get; }

        string UnknownDisplayDescription { get; }

        // UiServices.Discovery.Texts: FormatProviderFriendlyDisplayName
        string FormatFriendlyName { get; }

        // UiServices.Discovery.Texts: FormatProviderUnknownDisplayName
        string FormatUnknownName { get; }

        string SelectCaption { get; }
    } // interface ITvServiceProviderTexts
} // namespace
