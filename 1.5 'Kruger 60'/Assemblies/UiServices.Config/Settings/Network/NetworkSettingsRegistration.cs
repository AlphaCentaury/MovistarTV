// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Drawing;

namespace IpTviewr.UiServices.Configuration.Settings.Network
{
    public class NetworkSettingsRegistration : IConfigurationItemRegistration
    {
        public const string ConfigurationGuid = "{BA2C8A80-A12A-48A2-91DE-1615D5CBA791}";
        private static int _myDirectIndex;

        public static NetworkSettings Settings
        {
            get => AppUiConfiguration.Current[_myDirectIndex] as NetworkSettings;
            set => AppUiConfiguration.Current[_myDirectIndex] = value;
        } // Settings

        public Guid Id => new Guid(ConfigurationGuid);

        public bool HasEditor => true;

        public Type ItemType => typeof(NetworkSettings);

        public IConfigurationItem CreateDefault()
        {
            return NetworkSettings.GetDefaultSettings();
        } // CreateDefault

        public string EditorDisplayName => Properties.SettingsTexts.NetworkDisplayName;

        public string EditorDescription => Properties.SettingsTexts.NetworkDescription;

        public Image EditorImage => Properties.Resources.NetworkSettings_32;

        public int EditorPriority => 9000;

        public IConfigurationItemEditor CreateEditor(IConfigurationItem data)
        {
            var editor = new Editors.NetworkSettingsEditor()
            {
                Settings = data as NetworkSettings
            };

            return editor;
        } // CreateEditor

        public int DirectIndex
        {
            get => _myDirectIndex;
            set => _myDirectIndex = value;
        } // DirectIndex
    } // class NetworkSettingsRegistration
} // namespace
