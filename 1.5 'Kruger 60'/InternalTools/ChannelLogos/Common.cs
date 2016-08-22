using Etsi.Ts102034.v010501.XmlSerialization;
using Etsi.Ts102034.v010501.XmlSerialization.BroadcastDiscovery;
using Etsi.Ts102034.v010501.XmlSerialization.PackageDiscovery;
using Project.IpTv.UiServices.Configuration;
using Project.IpTv.UiServices.Discovery;
using Project.IpTv.UiServices.DvbStpClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace Project.IpTv.Internal.Tools.ChannelLogos
{
    static class Common
    {
        public static UiBroadcastDiscovery LoadBroadcastDiscovery(IWin32Window owner, UiServiceProvider serviceProvider, UiBroadcastDiscovery currentUiBroadcast, bool fromCache, bool highDefinitionPriority)
        {
            UiBroadcastDiscovery uiDiscovery;

            try
            {
                uiDiscovery = null;
                if (fromCache)
                {
                    var cachedDiscovery = AppUiConfiguration.Current.Cache.LoadXmlDocument<UiBroadcastDiscovery>("UiBroadcastDiscovery", serviceProvider.Key);
                    if (cachedDiscovery == null)
                    {

                    }
                    else
                    {
                        uiDiscovery = cachedDiscovery.Document;
                    } // if
                } // if

                if (uiDiscovery == null)
                {
                    var downloader = new UiDvbStpEnhancedDownloader()
                    {
                        Request = new UiDvbStpEnhancedDownloadRequest(2)
                        {
                            MulticastAddress = IPAddress.Parse(serviceProvider.Offering.Push[0].Address),
                            MulticastPort = serviceProvider.Offering.Push[0].Port,
                            Description = "BroadcastObtainingList",
                            DescriptionParsing = "BroadcastParsingList",
                            AllowXmlExtraWhitespace = false,
                            XmlNamespaceReplacer = NamespaceUnification.Replacer,
#if DEBUG
                            DumpToFolder = AppUiConfiguration.Current.Folders.Cache
#endif
                        },
                        TextUserCancelled = "UserCancelListRefresh",
                        TextDownloadException = "BroadcastListUnableRefresh",
                    };
                    downloader.Request.AddPayload(0x02, null, "Payload02DisplayName", typeof(BroadcastDiscoveryRoot));
                    downloader.Request.AddPayload(0x05, null, "Payload05DisplayName", typeof(PackageDiscoveryRoot));
                    downloader.Download(owner);
                    if (!downloader.IsOk) return null;

                    var xmlDiscovery = downloader.Request.Payloads[0].XmlDeserializedData as BroadcastDiscoveryRoot;
                    uiDiscovery = new UiBroadcastDiscovery(xmlDiscovery, serviceProvider.DomainName, downloader.Request.Payloads[0].SegmentVersion);

                    UiBroadcastDiscoveryMergeResultDialog.Merge(owner, currentUiBroadcast, uiDiscovery);

                    var xmlPackageDiscovery = downloader.Request.Payloads[1].XmlDeserializedData as PackageDiscoveryRoot;
                    UiServicesLogicalNumbers.AssignLogicalNumbers(uiDiscovery, xmlPackageDiscovery, serviceProvider.DomainName, highDefinitionPriority);
                    AppUiConfiguration.Current.Cache.SaveXml("UiBroadcastDiscovery", serviceProvider.Key, uiDiscovery.Version, uiDiscovery);
                } // if

                return uiDiscovery;
            }
            catch (Exception ex)
            {
                Program.HandleException(owner, null, "BroadcastListUnableRefresh", ex);
                return null;
            } // try-catch
        } // LoadBroadcastDiscovery

    } // static class Common
} // namespace
