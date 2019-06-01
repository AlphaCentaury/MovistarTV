// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using IpTviewr.UiServices.Configuration;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace IpTviewr.UiServices.Discovery.BroadcastList
{
    public class UiBroadcastListSettingsRegistration: IConfigurationItemRegistration
    {
        public const string RegistrationGuid = "{68B9F98B-DB50-4A08-AF04-35457F0224FB}";
        private static int MyDirectIndex;

        public static UiBroadcastListSettings Settings
        {
            get { return AppUiConfiguration.Current[MyDirectIndex] as UiBroadcastListSettings; }
            set { AppUiConfiguration.Current[MyDirectIndex] = value; }
        } // Settings

        public static Guid SettingsGuid
        {
            get { return new Guid(RegistrationGuid); }
        } // SettingsGuid

        public Guid Id
        {
            get { return new Guid(RegistrationGuid); }
        } // Id

        public bool HasEditor
        {
            get { return true; }
        } // HasEditor

        public Type ItemType
        {
            get { return typeof(UiBroadcastListSettings); }
        } // GetItemType

        public IConfigurationItem CreateDefault()
        {
            return UiBroadcastListSettings.GetDefaultSettings();
        } // CreateDefault

        public string EditorDisplayName
        {
            get { return Properties.SettingsEditor.DisplayName; }
        } // EditorDisplayName

        public string EditorDescription
        {
            get { return Properties.SettingsEditor.Description; }
        } // EditorDescription

        public Image EditorImage
        {
            get { return Properties.Resources.BroadcastListSettings_32x32; }
        } // EditorImage

        public int EditorPriority
        {
            get { return 50; }
        } // EditorPriority

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
            get { return MyDirectIndex; }
            set { MyDirectIndex = value; }
        } // DirectIndex
    } // UiBroadcastListSettingsRegistration
} // namespace
