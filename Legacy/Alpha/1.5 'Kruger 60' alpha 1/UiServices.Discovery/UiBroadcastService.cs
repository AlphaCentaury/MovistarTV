// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using DvbIpTypes.Schema2006;
using Project.DvbIpTv.UiServices.Configuration;
using Project.DvbIpTv.UiServices.Configuration.Logos;
using Project.DvbIpTv.UiServices.Configuration.Schema2014;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Property = System.Collections.Generic.KeyValuePair<string, string>;

namespace Project.DvbIpTv.UiServices.Discovery
{
    [Serializable]
    [XmlType(TypeName="UI-BroadcastService", Namespace=SerializationCommon.XmlNamespace)]
    public class UiBroadcastService
    {
        private string fieldKey;
        private string fieldDisplayName;
        private string fieldDisplayDescription;
        private string fieldLocationUrl;
        private string fieldDisplayServiceType;
        [NonSerialized]
        private ServiceLogo fieldLogo;

        /// <remarks>Used by Serialization</remarks>
        public UiBroadcastService()
        {
        } // constructor

        public UiBroadcastService(IpService service, string providerDomainName)
        {
            if (service == null) throw new ArgumentNullException("IpService service");

            Data = service;
            DomainName = Data.TextualIdentifier.DomainName ?? providerDomainName;
            Key = string.Format(Properties.InvariantTexts.FormatServiceProviderKey, ServiceName, DomainName);
        } // constructor

        [XmlElement(Namespace = IpService.XmlNamespace)]
        public IpService Data
        {
            get;
            set;
        } // Data

        public string DomainName
        {
            get;
            set;
        } // DomainName

        [XmlIgnore]
        public string ServiceName
        {
            get { return Data.TextualIdentifier.ServiceName; }
        } // ServiceName

        [XmlIgnore]
        public string FullServiceName
        {
            get { return ServiceName + "." + DomainName; }
        } // FullServiceName

        [XmlIgnore]
        public TextualIdentifier ReplacementService
        {
            get
            {
                var si = Data.ServiceInformation;
                if (si == null) return null;

                var replacements = si.ReplacementService;
                if ((replacements == null) || (replacements.Length == 0)) return null;

                var q = from r in replacements
                        let ti = r.Item as TextualIdentifier
                        where ti != null
                        select ti;
                var replacement = q.FirstOrDefault();

                return replacement;
            } // get
        } // ReplacementServiceName

        [XmlIgnore]
        public string DisplayName
        {
            get
            {
                if (fieldDisplayName == null)
                {
                    fieldDisplayName = GetDisplayName();
                } // if

                return fieldDisplayName;
            } // get
        } // DisplayName

        [XmlIgnore]
        public string DisplayDescription
        {
            get
            {
                if (fieldDisplayDescription == null)
                {
                    fieldDisplayDescription = GetDisplayDescription();
                } // if

                return fieldDisplayDescription;
            } // get
        } // DisplayDescription

        [XmlIgnore]
        public string ServiceType
        {
            get { return (Data.ServiceInformation == null) ? null : Data.ServiceInformation.ServiceType; }
        } // ServiceType

        [XmlIgnore]
        public string DisplayServiceType
        {
            get
            {
                if (fieldDisplayServiceType == null)
                {
                    fieldDisplayServiceType = GetDisplayServiceType();
                } // if

                return fieldDisplayServiceType;
            } // get
        } //  DisplayServiceType

        [XmlIgnore]
        public string LocationUrl
        {
            get
            {
                if (fieldLocationUrl == null)
                {
                    fieldLocationUrl = GetLocationUrl();
                } // if

                return fieldLocationUrl;
            } // get
        } // LocationUrl

        [XmlIgnore]
        public string DisplayLocationUrl
        {
            get
            {
                var locationUrl = LocationUrl;

                return (locationUrl != null) ? locationUrl : Properties.Texts.NotProvidedValue;
            } // get
        } // DisplayLocationUrl

        [XmlIgnore]
        public ServiceLogo Logo
        {
            get
            {
                if (fieldLogo == null)
                {
                    fieldLogo = GetLogo();
                } // if

                return fieldLogo;
            }
        } // Logo

        public string Key
        {
            get
            {
                if (fieldKey == null)
                {
                    fieldKey = string.Format(Properties.InvariantTexts.FormatServiceProviderKey, ServiceName, DomainName);
                } // if

                return fieldKey;
            } // get
            set
            {
                fieldKey = value;
            } // set
        } // Key

        public bool IsDead
        {
            get;
            set;
        } // IsDead

        // v1.0 RC 0: code moved from ChannelList > ChanneListForm.cs > DumpProperties(UiBroadcastService)

        public IEnumerable<Property> DumpProperties()
        {
            var properties = new List<Property>();

            properties.Add(new Property("Name (display)", DisplayName));
            properties.Add(new Property("Description (display)", DisplayDescription));
            properties.Add(new Property("Type (display)", DisplayServiceType));
            properties.Add(new Property("Location URL (display)", DisplayLocationUrl));
            properties.Add(new Property("Is active", (!IsDead).ToString()));

            if (Data.ServiceLocation == null)
            {
                properties.Add(new Property("Service location", null));
            }
            else
            {
                if (Data.ServiceLocation.Multicast == null)
                {
                    properties.Add(new Property("Service location (multicast)", null));
                }
                else
                {
                    properties.Add(new Property("Service location (multicast)", Data.ServiceLocation.Multicast.RtpUrl));
                } // if-else
                properties.Add(new Property("Service location (RTSP)", Data.ServiceLocation.RtspUrl));
            } // if-else

            if (Data.TextualIdentifier == null)
            {
                properties.Add(new Property("Textual identifier", null));
            }
            else
            {
                properties.Add(new Property("Identifier: Service name", Data.TextualIdentifier.ServiceName));
                properties.Add(new Property("Identifier: Domain", Data.TextualIdentifier.DomainName));
            } // if-else

            if (Data.DvbTriplet == null)
            {
                properties.Add(new Property("DVB Triplet", null));
            }
            else
            {
                properties.Add(new Property("DVB Triplet", string.Format("OrigNetId='{0}', TSId='{1}', ServiceId='{2}'",
                    Data.DvbTriplet.OrigNetId, Data.DvbTriplet.TSId, Data.DvbTriplet.ServiceId)));
            } // if-else

            properties.Add(new Property("Max bitarate", Data.MaxBitrate));

            if (Data.ServiceInformation == null)
            {
                properties.Add(new Property("Service information", null));
            }
            else
            {
                properties.Add(new Property("Service type", Data.ServiceInformation.ServiceType));
                properties.Add(new Property("Primary SI source", Data.ServiceInformation.PrimarySISource.ToString()));
                if (Data.ServiceInformation.Name == null)
                {
                    properties.Add(new Property("Name", null));
                }
                else
                {
                    foreach (var txt in Data.ServiceInformation.Name)
                    {
                        properties.Add(Utils.GetLanguageProperty("Name", txt));
                    } // foreach
                } // if-else
                if (Data.ServiceInformation.Description == null)
                {
                    properties.Add(new Property("Description", null));
                }
                else
                {
                    foreach (var txt in Data.ServiceInformation.Description)
                    {
                        properties.Add(Utils.GetLanguageProperty("Description", txt));
                    } // foreach
                } // if-else

                if ((Data.ServiceInformation.ServiceDescriptionLocation == null) || (Data.ServiceInformation.ServiceDescriptionLocation.Length == 0))
                {
                    properties.Add(new Property("Description location", null));
                }
                else
                {
                    foreach (var location in Data.ServiceInformation.ServiceDescriptionLocation)
                    {
                        properties.Add(new Property("Description location", location));
                    } // foreach
                } // if-else
                if (Data.ServiceInformation.ContentGenre == null)
                {
                    properties.Add(new Property("Content genre", null));
                }
                else
                {
                    var buffer = new StringBuilder();
                    foreach (var b in Data.ServiceInformation.ContentGenre)
                    {
                        buffer.AppendFormat("{0:X2} ", b);
                    } // foreach
                    properties.Add(new Property("Content genre", buffer.ToString()));
                } // if-else

                // ServiceInformation.ReplacementService
                if (Data.ServiceInformation.ReplacementService == null)
                {
                    properties.Add(new Property("Replacement service", null));
                }
                else
                {
                    foreach (var replacement in Data.ServiceInformation.ReplacementService)
                    {
                        var triplet = replacement.Item as DvbTriplet;
                        if (triplet != null)
                        {
                            properties.Add(new Property("Replacement service", string.Format("DVB Triplet: OrigNetId='{0}', TSId='{1}', ServiceId='{2}'",
                                                Data.DvbTriplet.OrigNetId, Data.DvbTriplet.TSId, Data.DvbTriplet.ServiceId)));
                        } // if
                        var textual = replacement.Item as TextualIdentifier;
                        if (textual != null)
                        {
                            properties.Add(new Property("Replacement service", string.Format("Identifier: Name='{0}', Domain='{1}'",
                                                textual.ServiceName, textual.DomainName)));

                        } // if
                        if ((triplet == null) && (textual == null))
                        {
                            properties.Add(new Property("Replacement service", null));
                        } // if
                        properties.Add(new Property("Replacement type", replacement.ReplacementType));
                    } // foreach
                } // if-else

                // ServiceInformation.MosaicDescription
                properties.Add(new Property("Has mosaic description", (Data.ServiceInformation.MosaicDescription != null).ToString()));

                // ServiceInformation.AnnouncementSupport
                properties.Add(new Property("Has announcement support", (Data.ServiceInformation.AnnouncementSupport != null).ToString()));

                // ServiceInformation.ServiceAvailability
                properties.Add(new Property("Has service availability", (Data.ServiceInformation.ServiceAvailability != null).ToString()));

                // ServiceInformation.ExtraData
                properties.Add(new Property("Has out-of-schema data", (Data.ServiceInformation.ExtraData != null).ToString()));
            } // if-else

            // AudioAttibutes
            properties.Add(new Property("Has audio details", (Data.AudioAttibutes != null).ToString()));

            // VideoAttibutes
            properties.Add(new Property("Has video details", (Data.VideoAttibutes != null).ToString()));

            return properties;
        } // DumpProperties

        private string GetDisplayName()
        {
            if (Data.ServiceInformation != null)
            {
                var text = Data.ServiceInformation.Name.SafeGetLanguageValue(AppUiConfiguration.Current.User.PreferredLanguagesList, true, null);
                if (text != null) return text;
            } // if

            return string.Format(Properties.Texts.FormatBroadcastUnknownDisplayName, Data.TextualIdentifier.ServiceName, DomainName);
        } // GetDisplayName

        private string GetDisplayDescription()
        {
            if (Data.ServiceInformation != null)
            {
                var text = Data.ServiceInformation.Description.SafeGetLanguageValue(AppUiConfiguration.Current.User.PreferredLanguagesList, true, null);
                if (text != null) return text;
            } // if

            return Properties.Texts.BroadcastUnknownDisplayDescription;
        } // GetDisplayDescription

        private string GetLocationUrl()
        {
            if (Data.ServiceLocation == null) return null;
            if (Data.ServiceLocation.Multicast != null) return Data.ServiceLocation.Multicast.RtpUrl;
            if (Data.ServiceLocation.RtspUrl != null) return Data.ServiceLocation.RtspUrl;
            return null;
        } // GetLocationUrl

        private string GetDisplayServiceType()
        {
            if (Data.ServiceInformation != null)
            {
                string serviceTypeDescription;
                string serviceType;

                serviceType = ServiceType;
                if (!AppUiConfiguration.Current.DescriptionServiceTypes.TryGetValue(serviceType, out serviceTypeDescription))
                {
                    serviceTypeDescription = string.Format(Properties.Texts.FormatServiceTypeIdUnknown, serviceType);
                } // if

                return serviceTypeDescription;
            }
            else
            {
                return Properties.Texts.NotProvidedValue;
            } // if-else
        } // GetDisplayServiceType

        private ServiceLogo GetLogo()
        {
            return AppUiConfiguration.Current.ServiceLogoMappings.Get(Data.TextualIdentifier.DomainName,
                DomainName,
                Data.TextualIdentifier.ServiceName,
                ServiceType);
        } // GetLogo
    } // class UiBroadcastService
} // namespace
