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
using IpTviewr.UiServices.Configuration;

namespace IpTviewr.Core
{
    public class IpTvProviderSettingsRegistration : IConfigurationItemRegistration
    {
        private static int _myDirectIndex;

        public static IpTvProviderSettings Settings
        {
            get => AppConfig.Current[_myDirectIndex] as IpTvProviderSettings;
            set => AppConfig.Current[_myDirectIndex] = value;
        } // Settings

        #region IConfigurationItemRegistration Members

        public Guid Id => new Guid(AppConfig.IpTvProviderSettingsRegistrationGuid);

        public bool HasEditor => false;

        public IConfigurationItem CreateDefault()
        {
            throw new NotSupportedException();
        } // CreateDefault

        public Type ItemType => typeof(IpTvProviderSettings);

        public string EditorDisplayName => throw new NotSupportedException();

        public string EditorDescription => throw new NotSupportedException();

        public Image EditorImage => throw new NotSupportedException();

        public int EditorPriority => throw new NotSupportedException();

        public IConfigurationItemEditor CreateEditor(IConfigurationItem data)
        {
            throw new NotSupportedException();
        } // CreateEditor

        public int DirectIndex
        {
            get => _myDirectIndex;
            set => _myDirectIndex = value;
        } // DirectIndex

        #endregion
    } // class IpTvProviderSettingsRegistration
} // namespace
