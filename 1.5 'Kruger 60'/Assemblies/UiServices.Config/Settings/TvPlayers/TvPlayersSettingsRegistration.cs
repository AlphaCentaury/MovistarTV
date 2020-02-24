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
using System.Drawing;

namespace IpTviewr.UiServices.Configuration.Settings.TvPlayers
{
    public class TvPlayersSettingsRegistration : IConfigurationItemRegistration
    {
        public const string ConfigurationGuid = "{AE75DE5D-11A9-4B0F-9EFB-242E70C022C9}";
        private static int _myDirectIndex;

        public static TvPlayersSettings Settings
        {
            get => AppConfig.Current[_myDirectIndex] as TvPlayersSettings;
            set => AppConfig.Current[_myDirectIndex] = value;
        } // Settings

        public Guid Id => new Guid(ConfigurationGuid);

        public bool HasEditor => true;

        public Type ItemType
            // GetItemType
            =>
                typeof(TvPlayersSettings);

        public IConfigurationItem CreateDefault()
        {
            return null;
        } // CreateDefault

        public string EditorDisplayName => Properties.SettingsTexts.TvPlayersDisplayName;

        public string EditorDescription => Properties.SettingsTexts.TvPlayersDescription;

        public Image EditorImage => Properties.Resources.TvPlayersSettings_32;

        public int EditorPriority => 250;

        public IConfigurationItemEditor CreateEditor(IConfigurationItem data)
        {
            var editor = new Editors.TvPlayersSettingsEditor()
            {
                Settings = data as TvPlayersSettings
            };

            return editor;
        } // CreateEditor

        public int DirectIndex
        {
            get => _myDirectIndex;
            set => _myDirectIndex = value;
        } // DirectIndex
    } // class TvPlayersSettingsRegistration
} // namespace
