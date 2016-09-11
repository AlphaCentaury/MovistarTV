// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace IpTviewr.UiServices.Configuration.Settings.Network
{
    public class NetworkSettingsRegistration : IConfigurationItemRegistration
    {
        public const string ConfigurationGuid = "{BA2C8A80-A12A-48A2-91DE-1615D5CBA791}";
        private static int MyDirectIndex;

        public static NetworkSettings Settings
        {
            get { return AppUiConfiguration.Current[MyDirectIndex] as NetworkSettings; }
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
            get { return typeof(NetworkSettings); }
        } // GetItemType

        public IConfigurationItem CreateDefault()
        {
            return NetworkSettings.GetDefaultSettings();
        } // CreateDefault

        public string EditorDisplayName
        {
            get { return Properties.SettingsTexts.NetworkDisplayName; }
        } // EditorDisplayName

        public string EditorDescription
        {
            get { return Properties.SettingsTexts.NetworkDescription; }
        } // EditorDescription

        public Image EditorImage
        {
            get { return Properties.Resources.NetworkSettings_32; }
        } // EditorImage

        public int EditorPriority
        {
            get { return 9000; }
        } // EditorPriority

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
            get { return MyDirectIndex; }
            set { MyDirectIndex = value; }
        } // DirectIndex
    } // class NetworkSettingsRegistration
} // namespace
