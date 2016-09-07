using Etsi.Ts102034.v010501.XmlSerialization;
using Etsi.Ts102034.v010501.XmlSerialization.BroadcastDiscovery;
using Etsi.Ts102034.v010501.XmlSerialization.PackageDiscovery;
using IpTviewr.Common;
using IpTviewr.UiServices.Configuration;
using IpTviewr.UiServices.Configuration.Cache;
using IpTviewr.UiServices.DvbStpClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

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

        public UiBroadcastDiscovery Download(Form ownerForm, UiServiceProvider serviceProvider, UiBroadcastDiscovery currentUiDiscovery, bool fromCache, bool? highDefinitionPriority = null)
        {
            UiBroadcastDiscovery uiDiscovery;

            try
            {
                uiDiscovery = null;
                if (fromCache)
                {
                    OnBeforeCacheLoad(this, EventArgs.Empty);
                    var cachedDiscovery = AppUiConfiguration.Current.Cache.LoadXmlDocument<UiBroadcastDiscovery>("UiBroadcastDiscovery", serviceProvider.Key);
                    OnAfterCacheLoad(this, new CacheEventArgs(cachedDiscovery));

                    if (cachedDiscovery != null) uiDiscovery = cachedDiscovery.Document;
                } // if

                if (uiDiscovery == null)
                {
                    OnBeforeDownload(this, EventArgs.Empty);
                    var downloader = new UiDvbStpEnhancedDownloader()
                    {
                        Request = new UiDvbStpEnhancedDownloadRequest(2)
                        {
                            MulticastAddress = IPAddress.Parse(serviceProvider.Offering.Push[0].Address),
                            MulticastPort = serviceProvider.Offering.Push[0].Port,
                            Description = Properties.Texts.BroadcastObtainingList,
                            DescriptionParsing = Properties.Texts.BroadcastParsingList,
                            AllowXmlExtraWhitespace = false,
                            XmlNamespaceReplacer = NamespaceUnification.Replacer,
#if DEBUG
                            DumpToFolder = AppUiConfiguration.Current.Folders.Cache
#endif
                        },
                        TextUserCancelled = Properties.Texts.UserCancelListRefresh,
                        TextDownloadException = Properties.Texts.BroadcastListUnableRefresh,
                    };
                    downloader.Request.AddPayload(0x02, null, Properties.Texts.Payload02DisplayName, typeof(BroadcastDiscoveryRoot));
                    downloader.Request.AddPayload(0x05, null, Properties.Texts.Payload05DisplayName, typeof(PackageDiscoveryRoot));
                    downloader.Download(ownerForm);
                    OnAfterDownload(this, new DownloadEventArgs(downloader));
                    if (!downloader.IsOk) return null;

                    var xmlDiscovery = downloader.Request.Payloads[0].XmlDeserializedData as BroadcastDiscoveryRoot;
                    uiDiscovery = new UiBroadcastDiscovery(xmlDiscovery, serviceProvider.DomainName, downloader.Request.Payloads[0].SegmentVersion);

                    OnBeforeMerge(this, new MergeEventArgs(uiDiscovery, currentUiDiscovery));
                    UiBroadcastDiscoveryMergeResultDialog.Merge(ownerForm, currentUiDiscovery, uiDiscovery);
                    OnAfterMerge(this, new MergeEventArgs(uiDiscovery, currentUiDiscovery));

                    var packageDiscovery = downloader.Request.Payloads[1].XmlDeserializedData as PackageDiscoveryRoot;
                    highDefinitionPriority = highDefinitionPriority.HasValue ? highDefinitionPriority.Value : !AppUiConfiguration.Current.User.ChannelNumberStandardDefinitionPriority;

                    OnBeforeAssignNumbers(this, new AssignNumbersArgs(uiDiscovery, packageDiscovery, serviceProvider, highDefinitionPriority.Value));
                    UiServicesLogicalNumbers.AssignLogicalNumbers(uiDiscovery, packageDiscovery, serviceProvider.DomainName, highDefinitionPriority.Value);
                    OnAfterAssignNumbers(this, new AssignNumbersArgs(uiDiscovery, packageDiscovery, serviceProvider, highDefinitionPriority.Value));

                    AppUiConfiguration.Current.Cache.SaveXml("PackageDiscovery", serviceProvider.Key, 0, packageDiscovery);
                    AppUiConfiguration.Current.Cache.SaveXml("UiBroadcastDiscovery", serviceProvider.Key, uiDiscovery.Version, uiDiscovery);
                } // if

                return uiDiscovery;
            }
            catch (Exception ex)
            {
                OnHandleException(this, new HandleExceptionEventArgs(ownerForm, null, Properties.Texts.BroadcastListUnableRefresh, ex));
                return null;
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
