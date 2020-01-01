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