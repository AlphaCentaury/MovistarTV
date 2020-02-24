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

using IpTviewr.UiServices.Configuration;
using System;
using System.Drawing;

namespace IpTviewr.UiServices.Discovery.BroadcastList
{
    public class UiBroadcastListSettingsRegistration : IConfigurationItemRegistration
    {
        public const string RegistrationGuid = "{68B9F98B-DB50-4A08-AF04-35457F0224FB}";
        private static int _myDirectIndex;

        public static UiBroadcastListSettings Settings
        {
            get => AppConfig.Current[_myDirectIndex] as UiBroadcastListSettings;
            set => AppConfig.Current[_myDirectIndex] = value;
        } // Settings

        public static Guid SettingsGuid => new Guid(RegistrationGuid);

        public Guid Id => new Guid(RegistrationGuid);

        public bool HasEditor => true;

        public Type ItemType => typeof(UiBroadcastListSettings);

        public IConfigurationItem CreateDefault()
        {
            return UiBroadcastListSettings.GetDefaultSettings();
        } // CreateDefault

        public string EditorDisplayName => Properties.SettingsEditor.DisplayName;

        public string EditorDescription => Properties.SettingsEditor.Description;

        public Image EditorImage => Properties.Resources.BroadcastListSettings_32x32;

        public int EditorPriority => 50;

        public IConfigurationItemEditor CreateEditor(IConfigurationItem data)
        {
            var editor = new Editors.UiBroadcastListSettingsEditor()
            {
                Settings = data as UiBroadcastListSettings
            };

            return editor;
        } // CreateEditor

        public int DirectIndex
        {
            get => _myDirectIndex;
            set => _myDirectIndex = value;
        } // DirectIndex
    } // UiBroadcastListSettingsRegistration
} // namespace
