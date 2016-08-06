// Copyright (C) 2014, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using DvbIpTypes.Schema2006;
using Project.DvbIpTv.DvbStp.Client;
using Project.DvbIpTv.RecorderLauncher.Serialization;
using Project.DvbIpTv.UiServices.Configuration;
using Project.DvbIpTv.UiServices.Controls;
using Project.DvbIpTv.UiServices.Discovery;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace Project.DvbIpTv.ChannelList
{
    public partial class ChannelListForm : Form
    {
        UiProviderDiscovery ProvidersDiscovery;
        UiServiceProvider SelectedServiceProvider;
        UiBroadcastDiscovery BroadcastDiscovery;
        UiBroadcastService SelectedBroadcastService;
        MulticastScannerDialog MulticastScanner;
        Font ServiceNameItemFont;
        string LastSelectedServiceProvider;

        public ChannelListForm()
        {
            InitializeComponent();
        } // constructor

        private void ChannelListForm_Load(object sender, EventArgs e)
        {
            this.Text = Properties.Texts.ChannelListFormCaption;
            comboServiceProvider.Enabled = false;
            labelSelectProvider.Enabled = false;
            labelProviderDescription.Enabled = false;

            LastSelectedServiceProvider = Properties.Settings.Default.LastSelectedServiceProvider;
            ServiceProviderChanged();
            BroadcastServiceChanged();
            ServiceNameItemFont = new Font(listViewChannels.Font.FontFamily, 11.0f, FontStyle.Bold);
        }  // ChannelListForm_Load

        private void ChannelListForm_Shown(object sender, EventArgs e)
        {
            // load from cache, if available
            if (!LoadServiceProviderList(true))
            {
                return;
            } // if
            
            // select 'last selected' provider
            for (int index=0;index<comboServiceProvider.Items.Count;index++)
            {
                var provider = comboServiceProvider.Items[index] as UiServiceProvider;
                if (provider.Key == LastSelectedServiceProvider)
                {
                    comboServiceProvider.SelectedIndex = index;
                    break;
                } // if
            } // for
        } // ChannelListForm_Shown

        private void buttonRefreshServiceProviderList_Click(object sender, EventArgs e)
        {
            LoadServiceProviderList(false);
        } // buttonRefreshServiceProviderList_Click

        bool LoadServiceProviderList(bool fromCache)
        {
            try
            {
                ServiceProviderDiscoveryXml discovery;
                var baseIpAddress = AppUiConfiguration.Current.User.ContentProvider.RootMulticastAddress;

                // can load from cache?
                discovery = null;
                if (fromCache)
                {
                    discovery = AppUiConfiguration.Current.Cache.LoadXml<ServiceProviderDiscoveryXml>("ProviderDiscovery", baseIpAddress);
                    if (discovery == null)
                    {
                        return false;
                    } // if
                } // if

                if (discovery == null)
                {
                    var basePort = AppUiConfiguration.Current.User.ContentProvider.RootMulticastPort;

                    var download = new DvbStpDownloadHelper()
                    {
                        Request = new DvbStpDownloadRequest()
                        {
                            PayloadId = 0x01,
                            SegmentId = 0x00,
                            MulticastAddress = IPAddress.Parse(baseIpAddress),
                            MulticastPort = basePort,
                            Description = Properties.Texts.SPObtainingList,
                            DescriptionParsing = Properties.Texts.SPParsingList,
                            PayloadDataType = typeof(ServiceProviderDiscoveryXml)
                        },
                        TextUserCancelled = Properties.Texts.UserCancelListRefresh,
                        TextDownloadException = Properties.Texts.SPListUnableRefresh,
                    };
                    download.ShowDialog(this);
                    if (!download.IsOk) return false;

                    discovery = download.Response.DeserializedPayloadData as ServiceProviderDiscoveryXml;
                    AppUiConfiguration.Current.Cache.SaveXml("ProviderDiscovery", baseIpAddress, download.Response.Version, discovery);
                } // if
                ProvidersDiscovery = new UiProviderDiscovery(discovery);

                comboServiceProvider.DataSource = null;
                comboServiceProvider.DisplayMember = "DisplayName";
                comboServiceProvider.DataSource = ProvidersDiscovery.Providers;

                labelSelectProvider.Enabled = true;
                comboServiceProvider.Enabled = true;
                labelProviderDescription.Enabled = true;

                return true;
            }
            catch (Exception ex)
            {
                MyApplication.HandleException(this, null,
                    Properties.Texts.SPListUnableRefresh, ex);
                return false;
            } // try-catch
        } // LoadServiceProviderList

        private void comboServiceProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            ServiceProviderChanged();
        } // comboServiceProvider_SelectedIndexChanged

        private void buttonProviderDetails_Click(object sender, EventArgs e)
        {
            if (SelectedServiceProvider == null) return;

            using (var dlg = new PropertiesDlg()
                {
                    Caption = Properties.Texts.SPProperties,
                    Properties = DumpProperties(SelectedServiceProvider),
                    Description = string.Format("Service provider: {0}", SelectedServiceProvider.DisplayName),
                    Logo = SelectedServiceProvider.Logo.GetImage(LogoSize.Size64, true),
                })
            {
                dlg.ShowDialog(this);
            } // using
        } // buttonProviderDetails_Click

        private void buttonRefreshChannelsList_Click(object sender, EventArgs e)
        {
            LoadBroadcastDiscovery(false);
        }  // buttonRefreshChannelsList_Click

        bool LoadBroadcastDiscovery(bool fromCache)
        {
            BroadcastDiscoveryXml discovery;
            ListViewItem[] listItems;
            int index;

            try
            {
                discovery = null;
                if (fromCache)
                {
                    discovery = AppUiConfiguration.Current.Cache.LoadXml<BroadcastDiscoveryXml>("BroadcastDiscovery", SelectedServiceProvider.Key);
                    if (discovery == null)
                    {
                        return false;
                    } // if
                } // if

                if (discovery == null)
                {
                    var download = new DvbStpDownloadHelper()
                    {
                        Request = new DvbStpDownloadRequest()
                        {
                            PayloadId = 0x02,
                            SegmentId = 0x00,
                            MulticastAddress = IPAddress.Parse(SelectedServiceProvider.Offering.Push[0].Address),
                            MulticastPort = SelectedServiceProvider.Offering.Push[0].Port,
                            Description = Properties.Texts.BroadcastObtainingList,
                            DescriptionParsing = Properties.Texts.BroadcastParsingList,
                            PayloadDataType = typeof(BroadcastDiscoveryXml)
                        },
                        TextUserCancelled = Properties.Texts.UserCancelListRefresh,
                        TextDownloadException = Properties.Texts.BroadcastListUnableRefresh,
                    };
                    download.ShowDialog(this);
                    if (!download.IsOk) return false;

                    discovery = download.Response.DeserializedPayloadData as BroadcastDiscoveryXml;
                    AppUiConfiguration.Current.Cache.SaveXml("BroadcastDiscovery", SelectedServiceProvider.Key, download.Response.Version, discovery);
                } // if

                BroadcastDiscovery = new UiBroadcastDiscovery(discovery, SelectedServiceProvider.DomainName);

                listItems = new ListViewItem[BroadcastDiscovery.Services.Count()];
                index = 0;
                foreach (var service in BroadcastDiscovery.Services)
                {
                    var item = new ListViewItem(service.DisplayName);
                    item.ImageKey = GetChannelLogoKey(service.Logo);
                    item.SubItems.Add(service.DisplayDescription);
                    item.SubItems.Add(service.DisplayServiceType);
                    item.SubItems.Add(service.DisplayLocationUrl);
                    item.UseItemStyleForSubItems = false;
                    item.Font = ServiceNameItemFont;
                    item.Tag = service;
                    item.Name = service.Key;
                    listItems[index++] = item;
                } // foreach

                listViewChannels.Items.Clear();
                listViewChannels.Items.AddRange(listItems);
                BroadcastServiceChanged();

                return true;
            }
            catch (Exception ex)
            {
                MyApplication.HandleException(this, null,
                    Properties.Texts.BroadcastListUnableRefresh, ex);
                return false;
            } // try-catch
        } // LoadBroadcastDiscovery

        private void ServiceProviderChanged()
        {
            SelectedServiceProvider = comboServiceProvider.SelectedItem as UiServiceProvider;
            BroadcastDiscovery = null;

            Properties.Settings.Default.LastSelectedServiceProvider = (SelectedServiceProvider != null)? SelectedServiceProvider.Key : null;
            Properties.Settings.Default.Save();

            if (SelectedServiceProvider == null)
            {
                labelProviderDescription.Text = Properties.Texts.NotSelectedServiceProvider;
                pictureProviderLogo.Image = null;
                buttonProviderDetails.Enabled = false;
                buttonRefreshChannelsList.Enabled = false;
                BroadcastServiceChanged();

                return;
            } // if

            labelProviderDescription.Text = SelectedServiceProvider.DisplayDescription;
            pictureProviderLogo.Image = SelectedServiceProvider.Logo.GetImage(LogoSize.Size32, true);

            buttonProviderDetails.Enabled = true;
            labelChannelsList.Enabled = true;
            buttonRefreshChannelsList.Enabled = true;

            LoadBroadcastDiscovery(true);
            BroadcastServiceChanged();
        } // ServiceProviderChanged

        private void BroadcastServiceChanged()
        {
            // TODO: cancel multicast services validation if active!

            SelectedBroadcastService = null;

            if ((BroadcastDiscovery == null) || (SelectedServiceProvider == null))
            {
                Properties.Settings.Default.LastSelectedService = null;
                Properties.Settings.Default.Save();

                labelChannelsList.Enabled = false;
                listViewChannels.Enabled = false;
                listViewChannels.Items.Clear();
                buttonValidateChannels.Enabled = false;
                buttonChannelDetails.Enabled = false;
                buttonRecordChannel.Enabled = false;
                buttonDisplayChannel.Enabled = false;
                return;
            } // if

            listViewChannels.Enabled = true;
            buttonValidateChannels.Enabled = true;
            if (listViewChannels.SelectedItems.Count == 0)
            {
                buttonChannelDetails.Enabled = false;
                buttonRecordChannel.Enabled = false;
                buttonDisplayChannel.Enabled = false;
                return;
            } // if

            SelectedBroadcastService = listViewChannels.SelectedItems[0].Tag as UiBroadcastService;
            Properties.Settings.Default.LastSelectedService = SelectedBroadcastService.Key;
            Properties.Settings.Default.Save();
            
            buttonChannelDetails.Enabled = true;
            buttonRecordChannel.Enabled = true;
            buttonDisplayChannel.Enabled = true;
        } // BroadcastServiceChanged

        private IEnumerable<PropertiesDlg.Property> DumpProperties(UiServiceProvider provider)
        {
            MultilingualText text;

            if (provider == null)
            {
                yield break;
            } // if

            text = provider.Data.Name.SafeGetLanguageItem(AppUiConfiguration.Current.User.PreferredLanguages, false);
            yield return GetLanguageProperty("Name", text);
            text = provider.Data.Description.SafeGetLanguageItem(AppUiConfiguration.Current.User.PreferredLanguages, false);
            yield return GetLanguageProperty("Description", text);
            yield return new PropertiesDlg.Property("Domain name", provider.DomainName);
            yield return new PropertiesDlg.Property("Logo URI", provider.Data.LogoUri);

            if (provider.Offering.Push != null)
            {
                foreach (var push in provider.Offering.Push)
                {
                    if (push.PayloadId == null)
                    {
                        yield return new PropertiesDlg.Property("Push offering",
                            string.Format("{0}:{1}", push.Address, push.Port));
                    }
                    else
                    {
                        yield return new PropertiesDlg.Property("Push offering",
                            string.Format("{0}:{1} with {2} payloads", push.Address, push.Port, push.PayloadId.Length));
                    } // if-else
                } // foreach push
            } // if

            if (provider.Offering.Pull != null)
            {
                foreach (var pull in provider.Offering.Pull)
                {
                    if (pull.PayloadId == null)
                    {
                        yield return new PropertiesDlg.Property("Pull offering",
                            string.Format("{0}", pull.Location));
                    }
                    else
                    {
                        yield return new PropertiesDlg.Property("Pull offering",
                            string.Format("{0} with {1} payloads", pull.Location, pull.PayloadId.Length));
                    } // if-else
                } // foreach pull
            } // if

            if (provider.Data.Name != null)
            {
                foreach (var txt in provider.Data.Name)
                {
                    yield return GetLanguageProperty("Name", txt);
                } // foreach
            } // if
            if (provider.Data.Description != null)
            {
                foreach (var txt in provider.Data.Description)
                {
                    yield return GetLanguageProperty("Description", txt);
                } // foreach
            } // if
        } // DumpProperties (UiServiceProvider)

        private IEnumerable<PropertiesDlg.Property> DumpProperties(UiBroadcastService service)
        {
            var data = service.Data;

            if (data.ServiceLocation == null)
            {
                yield return new PropertiesDlg.Property("Service location", null);
            }
            else
            {
                if (data.ServiceLocation.Multicast == null)
                {
                    yield return new PropertiesDlg.Property("Service location (multicast)", null);
                }
                else
                {
                    yield return new PropertiesDlg.Property("Service location (multicast)", data.ServiceLocation.Multicast.RtpUrl);
                } // if-else
                yield return new PropertiesDlg.Property("Service location (RTSP)", data.ServiceLocation.RtspUrl);
            } // if-else

            if (data.TextualIdentifier == null)
            {
                yield return new PropertiesDlg.Property("Textual identifier", null);
            }
            else
            {
                yield return new PropertiesDlg.Property("Identifier: Service name", data.TextualIdentifier.ServiceName);
                yield return new PropertiesDlg.Property("Identifier: Domain", data.TextualIdentifier.DomainName);
            } // if-else

            if (service.Data.DvbTriplet == null)
            {
                yield return new PropertiesDlg.Property("DVB Triplet", null);
            }
            else
            {
                yield return new PropertiesDlg.Property("DVB Triplet", string.Format("OrigNetId='{0}', TSId='{1}', ServiceId='{2}'",
                    data.DvbTriplet.OrigNetId, data.DvbTriplet.TSId, data.DvbTriplet.ServiceId));
            } // if-else

            yield return new PropertiesDlg.Property("Max bitarate", data.MaxBitrate);

            if (data.ServiceInformation == null)
            {
                yield return new PropertiesDlg.Property("Service information", null);
            }
            else
            {
                yield return new PropertiesDlg.Property("Service type", data.ServiceInformation.ServiceType);
                yield return new PropertiesDlg.Property("Primary SI source", data.ServiceInformation.PrimarySISource.ToString());
                if (data.ServiceInformation.Name == null)
                {
                    yield return new PropertiesDlg.Property("Name", null);
                }
                else
                {
                    foreach (var txt in data.ServiceInformation.Name)
                    {
                        yield return GetLanguageProperty("Name", txt);
                    } // foreach
                } // if-else
                if (data.ServiceInformation.Description == null)
                {
                    yield return new PropertiesDlg.Property("Description", null);
                }
                else
                {
                    foreach (var txt in data.ServiceInformation.Description)
                    {
                        yield return GetLanguageProperty("Description", txt);
                    } // foreach
                } // if-else

                if ((data.ServiceInformation.ServiceDescriptionLocation == null) || (data.ServiceInformation.ServiceDescriptionLocation.Length == 0))
                {
                    yield return new PropertiesDlg.Property("Description location", null);
                }
                else
                {
                    foreach (var location in data.ServiceInformation.ServiceDescriptionLocation)
                    {
                        yield return new PropertiesDlg.Property("Description location", location);
                    } // foreach
                } // if-else
                if (data.ServiceInformation.ContentGenre == null)
                {
                    yield return new PropertiesDlg.Property("Content genre", null);
                }
                else
                {
                    var buffer = new StringBuilder();
                    foreach (var b in data.ServiceInformation.ContentGenre)
                    {
                        buffer.AppendFormat("{0:X2} ", b);
                    } // foreach
                    yield return new PropertiesDlg.Property("Content genre", buffer.ToString());
                } // if-else

                // ServiceInformation.ReplacementService
                if (data.ServiceInformation.ReplacementService == null)
                {
                    yield return new PropertiesDlg.Property("Replacement service", null);
                }
                else
                {
                    foreach (var replacement in data.ServiceInformation.ReplacementService)
                    {
                        var triplet = replacement.Item as DvbTriplet;
                        if (triplet != null)
                        {
                            yield return new PropertiesDlg.Property("Replacement service", string.Format("DVB Triplet: OrigNetId='{0}', TSId='{1}', ServiceId='{2}'",
                                                data.DvbTriplet.OrigNetId, data.DvbTriplet.TSId, data.DvbTriplet.ServiceId));
                        } // if
                        var textual = replacement.Item as TextualIdentifier;
                        if (textual != null)
                        {
                            yield return new PropertiesDlg.Property("Replacement service", string.Format("Identifier: Name='{0}', Domain='{1}'",
                                                textual.ServiceName, textual.DomainName));

                        } // if
                        if ((triplet == null) && (textual == null))
                        {
                            yield return new PropertiesDlg.Property("Replacement service", null);
                        } // if
                        yield return new PropertiesDlg.Property("Replacement type", replacement.ReplacementType);
                    } // foreach
                } // if-else

                // ServiceInformation.MosaicDescription
                yield return new PropertiesDlg.Property("Has mosaic description", (data.ServiceInformation.MosaicDescription != null).ToString());

                // ServiceInformation.AnnouncementSupport
                yield return new PropertiesDlg.Property("Has announcement support", (data.ServiceInformation.AnnouncementSupport != null).ToString());

                // ServiceInformation.ServiceAvailability
                yield return new PropertiesDlg.Property("Has service availability", (data.ServiceInformation.ServiceAvailability != null).ToString());

                // ServiceInformation.ExtraData
                yield return new PropertiesDlg.Property("Has out-of-schema data", (data.ServiceInformation.ExtraData != null).ToString());
            } // if-else

            // AudioAttibutes
            yield return new PropertiesDlg.Property("Has audio details", (data.AudioAttibutes != null).ToString());

            // VideoAttibutes
            yield return new PropertiesDlg.Property("Has video details", (data.VideoAttibutes != null).ToString());
        } // DumpProperties (UiBroadcastService)

        private PropertiesDlg.Property GetLanguageProperty(string name, MultilingualText text)
        {
            if (text == null) return new PropertiesDlg.Property(name, null);
            if (text.Language == null) return new PropertiesDlg.Property(name, text.Value);
            return new PropertiesDlg.Property(string.Format("{0} ({1})", name, text.Language), text.Value);
        } // GetLanguageProperty

        private void buttonDisplayChannel_Click(object sender, EventArgs e)
        {
            try
            {
                // TODO: player must be user-selectable
                ExternalPlayer.Launch(AppUiConfiguration.Current.User.Players[0], SelectedBroadcastService, true);
            }
            catch (Exception ex)
            {
                MyApplication.HandleException(this, ex);
            } // try-catch
        } // buttonDisplayChannel_Click

        private void listViewChannels_SelectedIndexChanged(object sender, EventArgs e)
        {
            BroadcastServiceChanged();
        } // listViewChannels_SelectedIndexChanged

        private string GetChannelLogoKey(ServiceLogo logo)
        {
            if (!imageListChannels.Images.ContainsKey(logo.Key))
            {
                using (var image = logo.GetImage(LogoSize.Size32, true))
                {
                    imageListChannels.Images.Add(logo.Key, image);
                } // using image
            } // if

            return logo.Key;
        } // GetChannelLogoKey

        private void buttonChannelDetails_Click(object sender, EventArgs e)
        {
            if (SelectedBroadcastService == null) return;

            using (var dlg = new PropertiesDlg()
            {
                Caption = Properties.Texts.BroadcastServiceProperties,
                Properties = DumpProperties(SelectedBroadcastService),
                Description = string.Format("Broadcast Service: {0}", SelectedBroadcastService.DisplayName),
                Logo = SelectedBroadcastService.Logo.GetImage(LogoSize.Size64, true),
            })
            {
                dlg.ShowDialog(this);
            } // using
        } // buttonChannelDetails_Click

        private void buttonValidateChannels_Click(object sender, EventArgs e)
        {
            int timeout;

            if ((MulticastScanner != null) && (!MulticastScanner.IsDisposed))
            {
                MulticastScanner.Visible = false;
                MulticastScanner.Show(this);
                return;
            } // if

            using (var dialog = new MulticastScannerOptionsDialog())
            {
                if (dialog.ShowDialog(this) != DialogResult.OK) return;
                // TODO: get "what" & "action" and proceed accordingly
                timeout = dialog.Timeout;
            } // using

            MulticastScanner = new MulticastScannerDialog();
            MulticastScanner.ChannelScanResult += MulticastScanner_ChannelScanResult;
            MulticastScanner.Timeout = timeout;
            // TODO: filter as indicated in "what"
            MulticastScanner.BroadcastDiscovery = BroadcastDiscovery;
            MulticastScanner.Show(this);
        } // buttonValidateChannels_Click

        void MulticastScanner_ChannelScanResult(object sender, MulticastScannerDialog.ChannelScanResultEventArgs e)
        {
            // TODO: implement "disable" or delete as indicated in "action"

            var item = listViewChannels.Items[e.ServiceKey];
            if (e.IsDead)
            {
                item.Font = item.ListView.Font;
                item.UseItemStyleForSubItems = true;
                item.ForeColor = SystemColors.GrayText;
                item.BackColor = SystemColors.Control;
                item.Font = item.ListView.Font;
            }
            else
            {
                item.ForeColor = item.ListView.ForeColor;
                item.BackColor = item.ListView.BackColor;
                item.UseItemStyleForSubItems = false;
                item.Font = ServiceNameItemFont;
            } // if-else
        }  // MulticastScanner_ChannelScanResult

        private void buttonRecordChannel_Click(object sender, EventArgs e)
        {
            RecordTask task;

            using (var dlg = new RecordChannelDialog())
            {
                dlg.Task = RecordTask.CreateWithDefaultValues(new RecordChannel()
                    {
                        LogicalNumber = "---",
                        Name = SelectedBroadcastService.DisplayName,
                        Description = SelectedBroadcastService.DisplayDescription,
                        LogoKey = SelectedBroadcastService.Logo.Key,
                        ServiceName = SelectedBroadcastService.ServiceName,
                        ChannelUrl = SelectedBroadcastService.LocationUrl,
                    });
                dlg.IsNewTask = true;
                dlg.ShowDialog(this);
                task = dlg.Task;
                if (dlg.DialogResult != DialogResult.OK) return;
            } // using dlg

            RecordHelper helper = new RecordHelper();
            helper.Record(task);
        } // buttonRecordChannel_Click
    }
}
