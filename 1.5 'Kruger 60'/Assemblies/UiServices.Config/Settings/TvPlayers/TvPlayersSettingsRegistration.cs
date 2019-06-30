// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Drawing;

namespace IpTviewr.UiServices.Configuration.Settings.TvPlayers
{
    public class TvPlayersSettingsRegistration : IConfigurationItemRegistration
    {
        public const string ConfigurationGuid = "{AE75DE5D-11A9-4B0F-9EFB-242E70C022C9}";
        private static int MyDirectIndex;

        public static TvPlayersSettings Settings
        {
            get { return AppUiConfiguration.Current[MyDirectIndex] as TvPlayersSettings; }
            set { AppUiConfiguration.Current[MyDirectIndex] = value; }
        } // Settings

        public Guid Id
        {
            get { return new Guid(ConfigurationGuid); }
        } // Id

        public bool HasEditor
        {
            get { return true; }
        } // HasEditor

        public Type ItemType
        {
            get { return typeof(TvPlayersSettings); }
        } // GetItemType

        public IConfigurationItem CreateDefault()
        {
            return null;
        } // CreateDefault

        public string EditorDisplayName
        {
            get { return Properties.SettingsTexts.TvPlayersDisplayName; }
        } // EditorDisplayName

        public string EditorDescription
        {
            get { return Properties.SettingsTexts.TvPlayersDescription; }
        } // EditorDescription

        public Image EditorImage
        {
            get { return Properties.Resources.TvPlayersSettings_32; }
        } // EditorImage

        public int EditorPriority
        {
            get { return 250; }
        } // EditorPriority

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
            get { return MyDirectIndex; }
            set { MyDirectIndex = value; }
        } // DirectIndex
    } // class TvPlayersSettingsRegistration
} // namespace
