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

using System.Configuration;
using IpTviewr.UiServices.Configuration.Settings;

namespace IpTviewr.Tools.FirstTimeConfig.Properties
{
    [SettingsProvider(typeof(JsonSettingsProvider))]
    internal partial class Settings
    {
        // no additional method
        // used to add [SettingsProvider]
    } // class Settings
} // namespace
