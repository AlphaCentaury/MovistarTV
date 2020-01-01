// ==============================================================================
// 
//   Copyright (C) 2014-2020, GitHub/Codeplex user AlphaCentaury
//   All rights reserved.
// 
//     See 'LICENSE.MD' file (or 'license.txt' if missing) in the project root
//     for complete license information.
// 
//   http://www.alphacentaury.org/movistartv
//   https://github.com/AlphaCentaury
// 
// ==============================================================================

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
