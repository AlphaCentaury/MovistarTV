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

using System;
using System.Windows.Forms;
using IpTviewr.UiServices.Common.Forms;
using IpTviewr.UiServices.Configuration;
using IpTviewr.UiServices.Configuration.Logos;
using IpTviewr.UiServices.Discovery;

namespace IpTviewr.UiServices.Forms
{
    public static class DiscoveryDialogs
    {
        public static void ShowServiceProviderDetails(Form parent, UiServiceProvider provider)
        {
            if (provider == null) throw new ArgumentNullException(nameof(provider));

            using var dlg = new PropertiesDialog
            {
                Caption = AppConfig.Current.IpTvService.Texts.Provider.PropertiesCaption,
                ItemProperties = provider.DumpProperties(),
                Description = provider.DisplayName,
                ItemIcon = provider.Logo.GetImage(LogoSize.Size64),
            };
            dlg.ShowDialog(parent);
        } // ShowServiceProviderDetails
    } // class DiscoveryDialogs
} // namespace
