using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Project.DvbIpTv.UiServices.Configuration;

namespace Project.DvbIpTv.Core.IpTvProvider
{
    public class IpTvProviderSettingsRegistration : IConfigurationItemRegistration
    {
        private static int MyDirectIndex;

        public static IpTvProviderSettings Settings
        {
            get { return AppUiConfiguration.Current[MyDirectIndex] as IpTvProviderSettings; }
            set { AppUiConfiguration.Current[MyDirectIndex] = value; }
        } // Settings

        #region IConfigurationItemRegistration Members

        public Guid Id
        {
            get { return new Guid(AppUiConfiguration.IpTvProviderSettingsRegistrationGuid); }
        } // Id

        public bool HasEditor
        {
            get { return false; }
        } // HasEditor

        public IConfigurationItem CreateDefault()
        {
            throw new NotSupportedException();
        } // CreateDefault

        public Type ItemType
        {
            get { return typeof(IpTvProviderSettings); }
        } // ItemType

        public string EditorDisplayName
        {
            get { throw new NotSupportedException(); }
        } // EditorDisplayName

        public string EditorDescription
        {
            get { throw new NotSupportedException(); }
        } // EditorDescription

        public Image EditorImage
        {
            get { throw new NotSupportedException(); }
        } // EditorImage

        public int EditorPriority
        {
            get { throw new NotSupportedException(); }
        } // EditorPriority

        public IConfigurationItemEditor CreateEditor(IConfigurationItem data)
        {
            throw new NotSupportedException();
        } // CreateEditor

        public int DirectIndex
        {
            get { return MyDirectIndex; }
            set { MyDirectIndex = value; }
        } // DirectIndex

        #endregion
    } // class IpTvProviderSettingsRegistration
} // namespace
