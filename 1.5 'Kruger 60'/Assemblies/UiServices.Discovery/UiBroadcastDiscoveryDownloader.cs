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

using Etsi.Ts102034.v010501.XmlSerialization;
using Etsi.Ts102034.v010501.XmlSerialization.BroadcastDiscovery;
using Etsi.Ts102034.v010501.XmlSerialization.PackageDiscovery;
using IpTviewr.Common;
using IpTviewr.UiServices.Configuration;
using IpTviewr.UiServices.Configuration.Cache;
using IpTviewr.UiServices.DvbStpClient;
using System;
using System.Net;
using System.Windows.Forms;
using Etsi.Ts102034.v010501.XmlSerialization.ContentGuideDiscovery;

namespace IpTviewr.UiServices.Discovery
{
    public class UiBroadcastDiscoveryDownloader
    {
        #region EventArgs
        public class CacheEventArgs : EventArgs
        {
            public CachedXmlDocument<UiBroadcastDiscovery> CachedDiscovery { get; set; }

            public CacheEventArgs()
            {
                // no op
            } // default constructor

            public CacheEventArgs(CachedXmlDocument<UiBroadcastDiscovery> cachedDiscovery)
            {
                CachedDiscovery = cachedDiscovery;
            } // constructor
        } // class CacheEventArgs

        public class DownloadEventArgs : EventArgs
        {
            public UiDvbStpEnhancedDownloader Downloader { get; set; }

            public DownloadEventArgs()
            {
                // no op
            } // default constructor

            public DownloadEventArgs(UiDvbStpEnhancedDownloader downloader)
            {
                Downloader = downloader;
            } // constructor
        } // class DownloadEventArgs

        public class MergeEventArgs: EventArgs
        {
            public UiBroadcastDiscovery Downloaded { get; set; }
            public UiBroadcastDiscovery Current { get; set; }

            public MergeEventArgs()
            {
                // no-op
            } // default constructor

            public MergeEventArgs(UiBroadcastDiscovery downloadedUiDiscovery, UiBroadcastDiscovery currentUiDiscovery)
            {
                Downloaded = downloadedUiDiscovery;
                Current = currentUiDiscovery;
            } // constructor
        } // class MergeEventArgs

        public class AssignNumbersArgs: EventArgs
        {
            public UiBroadcastDiscovery Current { get; set; }
            public PackageDiscoveryRoot PackageDiscovery { get; set; }
            public UiServiceProvider ServiceProvider { get; set; }
            public bool HighDefinitionPriority { get; set; }

            public AssignNumbersArgs()
            {
                // no-op
            } // default constructor

            public AssignNumbersArgs(UiBroadcastDiscovery currentUiDiscovery, PackageDiscoveryRoot packageDiscovery, UiServiceProvider serviceProvider, bool highDefinitionPriority)
            {
                Current = currentUiDiscovery;
                PackageDiscovery = packageDiscovery;
                ServiceProvider = serviceProvider;
                HighDefinitionPriority = highDefinitionPriority;
            } // constructor
        } // AssignNumbersArgs
        #endregion

        public event EventHandler BeforeCacheLoad;
        public event EventHandler<CacheEventArgs> AfterCacheLoad;
        public event EventHandler BeforeDownload;
        public event EventHandler<DownloadEventArgs> AfterDownload;
        public event EventHandler<MergeEventArgs> BeforeMerge;
        public event EventHandler<MergeEventArgs> AfterMerge;
        public event EventHandler<AssignNumbersArgs> BeforeAssignNumbers;
        public event EventHandler<AssignNumbersArgs> AfterAssignNumbers;
        public event EventHandler<HandleExceptionEventArgs> Exception;

        public UiBroadcastDiscovery BroadcastDiscovery { get; private set; }
        public PackageDiscoveryRoot PackageDiscovery { get; private set; }
        public BroadcastContentGuideDiscoveryRoot EpgDiscovery { get; private set; }

        public bool Download(Form ownerForm, UiServiceProvider serviceProvider, UiBroadcastDiscovery currentUiDiscovery, bool fromCache, bool? highDefinitionPriority = null, bool skipEpg = false)
        {
            try
            {
                BroadcastDiscovery = null;
                PackageDiscovery = null;
                if (!skipEpg) EpgDiscovery = null;

                if (fromCache)
                {
                    OnBeforeCacheLoad(this, EventArgs.Empty);
                    var cachedBroadcastDiscovery = AppConfig.Current.Cache.LoadXmlDocument<UiBroadcastDiscovery>("UiBroadcastDiscovery", serviceProvider.Key);
                    var cachedPackageDiscovery = AppConfig.Current.Cache.LoadXmlDocument<PackageDiscoveryRoot>("PackageDiscovery", serviceProvider.Key);
                    var cachedEpgDiscovery = AppConfig.Current.Cache.LoadXmlDocument<BroadcastContentGuideDiscoveryRoot>("BroadcastContentGuideDiscovery", serviceProvider.Key);
                    OnAfterCacheLoad(this, new CacheEventArgs(cachedBroadcastDiscovery));

                    BroadcastDiscovery = cachedBroadcastDiscovery?.Document;
                    PackageDiscovery = cachedPackageDiscovery?.Document;
                    if (!skipEpg) EpgDiscovery = cachedEpgDiscovery?.Document;
                } // if

                if ((BroadcastDiscovery != null) && (PackageDiscovery != null) && (skipEpg || EpgDiscovery != null))
                {
                    return true;
                } // if

                BroadcastDiscovery = null;
                PackageDiscovery = null;
                if (!skipEpg) EpgDiscovery = null;

                OnBeforeDownload(this, EventArgs.Empty);
                var downloader = new UiDvbStpEnhancedDownloader()
                {
                    Request = new UiDvbStpEnhancedDownloadRequest(3)
                    {
                        MulticastAddress = IPAddress.Parse(serviceProvider.Offering.Push[0].Address),
                        MulticastPort = serviceProvider.Offering.Push[0].Port,
                        Description = Properties.Texts.BroadcastObtainingList,
                        DescriptionParsing = Properties.Texts.BroadcastParsingList,
                        AllowXmlExtraWhitespace = false,
                        XmlNamespaceReplacer = NamespaceUnification.Replacer,
                        NoDataTimeout = 60000, // 60 seconds
                        ReceiveDatagramTimeout = 60000, // 60 seconds
#if DEBUG
                        DumpToFolder = AppConfig.Current.Folders.Cache
#endif
                    },
                    TextUserCancelled = Properties.Texts.UserCancelListRefresh,
                    TextDownloadException = Properties.Texts.BroadcastListUnableRefresh,
                };
                downloader.Request.AddPayload(0x02, null, Properties.Texts.Payload02DisplayName, typeof(BroadcastDiscoveryRoot));
                downloader.Request.AddPayload(0x05, null, Properties.Texts.Payload05DisplayName, typeof(PackageDiscoveryRoot));
                if (!skipEpg) downloader.Request.AddPayload(0x06, null, Properties.Texts.Payload06DisplayName, typeof(BroadcastContentGuideDiscoveryRoot));
                downloader.Download(ownerForm);
                OnAfterDownload(this, new DownloadEventArgs(downloader));
                if (!downloader.IsOk) return false;

                var xmlDiscovery = downloader.Request.Payloads[0].XmlDeserializedData as BroadcastDiscoveryRoot;
                BroadcastDiscovery = new UiBroadcastDiscovery(xmlDiscovery, serviceProvider.DomainName, downloader.Request.Payloads[0].SegmentVersion);

                OnBeforeMerge(this, new MergeEventArgs(BroadcastDiscovery, currentUiDiscovery));
                UiBroadcastDiscoveryMergeResultDialog.Merge(ownerForm, currentUiDiscovery, BroadcastDiscovery);
                OnAfterMerge(this, new MergeEventArgs(BroadcastDiscovery, currentUiDiscovery));

                PackageDiscovery = downloader.Request.Payloads[1].XmlDeserializedData as PackageDiscoveryRoot;
                highDefinitionPriority ??= !AppConfig.Current.User.ChannelNumberStandardDefinitionPriority;

                OnBeforeAssignNumbers(this, new AssignNumbersArgs(BroadcastDiscovery, PackageDiscovery, serviceProvider, highDefinitionPriority.Value));
                UiServicesLogicalNumbers.AssignLogicalNumbers(BroadcastDiscovery, PackageDiscovery, serviceProvider.DomainName, highDefinitionPriority.Value);
                OnAfterAssignNumbers(this, new AssignNumbersArgs(BroadcastDiscovery, PackageDiscovery, serviceProvider, highDefinitionPriority.Value));

                if (!skipEpg) EpgDiscovery = downloader.Request.Payloads[2].XmlDeserializedData as BroadcastContentGuideDiscoveryRoot;

                AppConfig.Current.Cache.SaveXml("UiBroadcastDiscovery", serviceProvider.Key, BroadcastDiscovery.Version, BroadcastDiscovery);
                AppConfig.Current.Cache.SaveXml("PackageDiscovery", serviceProvider.Key, 0, PackageDiscovery);
                if (!skipEpg) AppConfig.Current.Cache.SaveXml("BroadcastContentGuideDiscovery", serviceProvider.Key, 0, EpgDiscovery);

                return true;
            }
            catch (Exception ex)
            {
                OnHandleException(this, new HandleExceptionEventArgs(ownerForm, null, Properties.Texts.BroadcastListUnableRefresh, ex));
                return false;
            } // try-catch
        } // Download

        protected void OnBeforeCacheLoad(object sender, EventArgs e)
        {
            BeforeCacheLoad?.Invoke(sender, e);
        } // OnBeforeCacheLoad

        protected void OnAfterCacheLoad(object sender, CacheEventArgs e)
        {
            AfterCacheLoad?.Invoke(sender, e);
        } // OnAfterCacheLoad

        protected void OnBeforeDownload(object sender, EventArgs e)
        {
            BeforeDownload?.Invoke(sender, e);
        } // OnBeforeDownload

        protected void OnAfterDownload(object sender, DownloadEventArgs e)
        {
            AfterDownload?.Invoke(sender, e);
        } // OnAfterDownload

        private void OnBeforeMerge(object sender, MergeEventArgs e)
        {
            BeforeMerge?.Invoke(sender, e);
        } // OnBeforeMerge

        private void OnAfterMerge(object sender, MergeEventArgs e)
        {
            AfterMerge?.Invoke(sender, e);
        } // OnAfterMerge

        private void OnBeforeAssignNumbers(object sender, AssignNumbersArgs e)
        {
            BeforeAssignNumbers?.Invoke(sender, e);
        } // OnBeforeAssignNumbers

        private void OnAfterAssignNumbers(object sender, AssignNumbersArgs e)
        {
            AfterAssignNumbers?.Invoke(sender, e);
        } // OnAfterAssignNumbers

        protected void OnHandleException(object sender, HandleExceptionEventArgs e)
        {
            Exception?.Invoke(sender, e);
        } // OnHandleException
    } // class UiBroadcastDiscoveryDownloader
} // namespace
