// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Etsi.Ts102034.v010501.XmlSerialization;
using Etsi.Ts102034.v010501.XmlSerialization.BroadcastDiscovery;
using Etsi.Ts102034.v010501.XmlSerialization.Common;
using IpTviewr.Common;
using IpTviewr.UiServices.Configuration;
using IpTviewr.UiServices.Configuration.Logos;
using IpTviewr.UiServices.Configuration.Schema2014;
using IpTviewr.UiServices.Configuration.Settings.Network;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using Property = System.Collections.Generic.KeyValuePair<string, string>;

namespace IpTviewr.UiServices.Discovery
{
    [Serializable]
    [XmlType(TypeName="UI-BroadcastService", Namespace=SerializationCommon.XmlNamespace)]
    public class UiBroadcastService
    {
        private string fieldDisplayOriginalName;
        private string fieldDisplayShortName;
        private string fieldDisplayDescription;
        private string fieldDisplayGenre;
        private string fieldDisplayGenreCode;
        private string fieldDisplayParentalRating;
        private string fieldDisplayParentalRatingCode;
        private string fieldDisplayLockLevel;
        private string fieldOriginalLocationUrl;
        private string fieldDisplayServiceType;
        [NonSerialized]
        private ServiceLogo fieldLogo;

        /// <remarks>Used by Serialization</remarks>
        public UiBroadcastService()
        {
        } // constructor

        public static string GetKey(TextualIdentifier serviceIdentifier, string defaultDomainName)
        {
            var domain = serviceIdentifier.DomainName ?? defaultDomainName;

            return string.Format(Properties.InvariantTexts.FormatServiceProviderKey, serviceIdentifier.ServiceName.ToLowerInvariant(), domain.ToLowerInvariant());
        } // CreateKey

        public UiBroadcastService(IpService service, string providerDomainName)
        {
            if (service == null) throw new ArgumentNullException("IpService service");

            Data = service;
            DomainName = (Data.TextualIdentifier.DomainName ?? providerDomainName).ToLowerInvariant();
            Key = GetKey(service.TextualIdentifier, providerDomainName);
        } // constructor

        #region Data for UI display

        [XmlIgnore]
        public bool IsHighDefinitionTv
        {
            get
            {
                switch (ServiceType)
                {
                    case "17": // HD TV (MPEG-2)
                    case "25": // HD TV (AVC)
                        return true;
                    default:
                        return false;
                } // switch
            } // get
        } // IsHighDefinitionTv

        [XmlIgnore]
        public bool IsStandardDefinitionTv
        {
            get
            {
                switch (ServiceType)
                {
                    case "1": // SD TV
                    case "22": // SD TV (AVC)
                        return true;
                    default:
                        return false;
                } // switch
            } // get
        } // IsStandardDefinitionTv

        [XmlIgnore]
        public bool IsTelevionService
        {
            get
            {
                switch (ServiceType)
                {
                    case "1": // SD TV
                    case "22": // SD TV (AVC)
                    case "17": // HD TV (MPEG-2)
                    case "25": // HD TV (AVC)
                        return true;
                    default:
                        return false;
                } // switch
            } // get
        } // IsTelevionService

        [XmlIgnore]
        public string DisplayLogicalNumber
        {
            get { return (UserLogicalNumber ?? (ServiceLogicalNumber ?? Properties.Texts.ChannelNumberNone)); }
        } // DisplayLogicalNumber

        [XmlIgnore]
        public string DisplayName
        {
            get { return (UserDisplayName ?? DisplayOriginalName); }
        } // DisplayName

        [XmlIgnore]
        public string DisplayShortName
        {
            get
            {
                if (fieldDisplayShortName == null)
                {
                    fieldDisplayShortName = GetDisplayShortName();
                } // if

                return fieldDisplayShortName;
            } // get
        } // DisplayShortName

        [XmlIgnore]
        public string DisplayOriginalName
        {
            get
            {
                if (fieldDisplayOriginalName == null)
                {
                    fieldDisplayOriginalName = GetDisplayOriginalName();
                } // if

                return fieldDisplayOriginalName;
            } // get
        } // DisplayOriginalName

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
        public string DisplayLocationUrl
        {
            get
            {
                var locationUrl = LocationUrl;

                return (locationUrl != null) ? locationUrl : Properties.Texts.NotProvidedValue;
            } // get
        } // DisplayLocationUrl

        [XmlIgnore]
        public string DisplayGenre
        {
            get
            {
                if (fieldDisplayGenre == null)
                {
                    fieldDisplayGenre = GetDisplayGenre();
                } // if

                return fieldDisplayGenre;
            } // get
        } // DisplayGenre

        [XmlIgnore]
        public string DisplayGenreCode
        {
            get
            {
                if (fieldDisplayGenreCode == null)
                {
                    fieldDisplayGenreCode = GetDisplayGenreCode();
                } // if

                return fieldDisplayGenreCode;
            } // get
        } // DisplayGenreCode

        [XmlIgnore]
        public string DisplayParentalRating
        {
            get
            {
                if (fieldDisplayParentalRating == null)
                {
                    fieldDisplayParentalRating = GetDisplayParentalRating();
                } // if

                return fieldDisplayParentalRating;
            } // get
        } // DisplayParentalRating

        [XmlIgnore]
        public string DisplayParentalRatingCode
        {
            get
            {
                if (fieldDisplayParentalRatingCode == null)
                {
                    fieldDisplayParentalRatingCode = GetDisplayParentalRatingCode();
                } // if

                return fieldDisplayParentalRatingCode;
            } // get
        } // DisplayParentalRatingCode

        [XmlIgnore]
        public string DisplayLockLevel
        {
            get
            {
                if (fieldDisplayLockLevel == null)
                {
                    fieldDisplayLockLevel = GetDisplayLockLevel();
                } // if

                return fieldDisplayLockLevel;
            } // get
        } // DisplayLockLevel

        #endregion

        #region UI additional data

        public string DomainName
        {
            get;
            set;
        } // DomainName

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
            get;
            set;
        } // Key

        [DefaultValue(false)]
        public bool IsInactive
        {
            get;
            set;
        } // IsInactive

        public string ServiceLogicalNumber
        {
            get;
            set;
        } // ServiceLogicalNumber

        #endregion

        #region User defined data

        [DefaultValue(false)]
        public bool IsHidden
        {
            get;
            set;
        } // IsHidden

        [DefaultValue(0)]
        public int LockLevel
        {
            get;
            set;
        } // LockLevel

        [DefaultValue(null)]
        public string UserDisplayName
        {
            get;
            set;
        } // UserDisplayName

        [DefaultValue(null)]
        public string UserLogicalNumber
        {
            get;
            set;
        } // UserLogicalNumber

        #endregion

        #region Shortcuts for underlying BroadcastService data

        [XmlElement(Namespace = IpService.Namespace)]
        public IpService Data
        {
            get;
            set;
        } // Data

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
                        let ti = r.TextualIdentifier
                        where ti != null
                        select ti;
                var replacement = q.FirstOrDefault();

                return replacement;
            } // get
        } // ReplacementService

        [XmlIgnore]
        public string ServiceType
        {
            get { return (Data.ServiceInformation == null) ? null : Data.ServiceInformation.ServiceType; }
        } // ServiceType

        [XmlIgnore]
        public string LocationUrl
        {
            get
            {
                var networkSettings = NetworkSettingsRegistration.Settings;
                if ((networkSettings == null) || (networkSettings.MulticastProxy == null) || (networkSettings.MulticastProxy.IsEnabled == false))
                {
                    return OriginalLocationUrl;
                } // if

                return GetProxiedLocationUrl(networkSettings.MulticastProxy);
            } // get
        } // LocationUrl

        [XmlIgnore]
        public string OriginalLocationUrl
        {
            get
            {
                if (fieldOriginalLocationUrl == null)
                {
                    fieldOriginalLocationUrl = GetLocationUrl();
                } // if

                return fieldOriginalLocationUrl;
            } // get
        } // OriginalLocationUrl

        #endregion

        public bool IsSameService(UiBroadcastService service)
        {
            if (this.Key != service.Key) return false;
            if (this.LocationUrl != service.LocationUrl) return false;
            if (this.DisplayOriginalName != service.DisplayOriginalName) return false;
            if (this.ServiceType != service.ServiceType) return false;

            return true;
        } // IsSameService

        // v1.0 RC 0: code moved from ChannelList > ChanneListForm.cs > DumpProperties(UiBroadcastService)

        public IEnumerable<Property> DumpProperties()
        {
            var properties = new List<Property>();

            properties.Add(new Property("Name (display)", DisplayName));
            properties.Add(new Property("Description (display)", DisplayDescription));
            properties.Add(new Property("Type (display)", DisplayServiceType));
            properties.Add(new Property("Location URL (display)", DisplayLocationUrl));
            properties.Add(new Property("Is active", (!IsInactive).ToString()));

            if (Data.ServiceLocation == null)
            {
                properties.Add(new Property("Service location", null));
            }
            else
            {
                if (Data.ServiceLocation.IpMulticastAddress == null)
                {
                    properties.Add(new Property("Service location (multicast)", null));
                }
                else
                {
                    properties.Add(new Property("Service location (multicast)", Data.ServiceLocation.IpMulticastAddress.Url));
                } // if-else

                if (Data.ServiceLocation.RtspUrl == null)
                {
                    properties.Add(new Property("Service location (RTSP)", null));
                }
                else
                {
                    properties.Add(new Property("Service location (RTSP control URL)", Data.ServiceLocation.RtspUrl.ControlUrl));
                    properties.Add(new Property("Service location (RTSP)", Data.ServiceLocation.RtspUrl.Value));
                } // if

                properties.Add(new Property("Service location (broadcast system)", Data.ServiceLocation.BroadcastSystem));
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

            if ((Data.DvbTriplet == null) || (Data.DvbTriplet.Length == 0))
            {
                properties.Add(new Property("DVB Triplet", null));
            }
            else
            {
                foreach (var triplet in Data.DvbTriplet)
                {
                    properties.Add(new Property("DVB Triplet", string.Format("OrigNetId='{0}', TSId='{1}', ServiceId='{2}'",
                        triplet.OrigNetId, triplet.TSId, triplet.ServiceId)));
                } // foreach
            } // if-else

            properties.Add(new Property("Max bitarate", Data.MaxBitrate));

            if (Data.ServiceInformation == null)
            {
                properties.Add(new Property("Service information", null));
            }
            else
            {
                properties.Add(new Property("Service type", Data.ServiceInformation.ServiceType));
                properties.Add(new Property("Primary information source", Data.ServiceInformation.PrimaryServiceInformationSource.ToString()));
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
                        properties.Add(new Property("Description location", location.Value));
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
                        var triplet = replacement.DvbTriplet as DvbTriplet;
                        if (triplet != null)
                        {
                            properties.Add(new Property("Replacement service", string.Format("DVB Triplet: OrigNetId='{0}', TSId='{1}', ServiceId='{2}'",
                                                triplet.OrigNetId, triplet.TSId, triplet.ServiceId)));
                        } // if
                        var textual = replacement.TextualIdentifier;
                        if (textual != null)
                        {
                            properties.Add(new Property("Replacement service", string.Format("Identifier: Name='{0}', Domain='{1}'",
                                                textual.ServiceName, textual.DomainName)));

                        } // if
                        if ((triplet == null) && (textual == null))
                        {
                            properties.Add(new Property("Replacement service", null));
                        } // if
                        properties.Add(new Property("Replacement type", replacement.Kind));
                    } // foreach
                } // if-else

                // ServiceInformation.MosaicDescription
                properties.Add(new Property("Has mosaic description", (Data.ServiceInformation.MosaicDescription != null).ToString()));

                // ServiceInformation.AnnouncementSupport
                properties.Add(new Property("Has announcement support", (Data.ServiceInformation.AnnouncementSupport != null).ToString()));

                // ServiceInformation.ExtraData
                properties.Add(new Property("Has out-of-schema data", (Data.ServiceInformation.ExtraData != null).ToString()));
            } // if-else

            // AudioAttibutes
            properties.Add(new Property("Has audio details", (Data.AudioAttributes != null).ToString()));

            // VideoAttibutes
            properties.Add(new Property("Has video details", (Data.VideoAttributes != null).ToString()));

            return properties;
        } // DumpProperties

        internal void TransferMergeProperties(UiBroadcastService service)
        {
            IsInactive = service.IsInactive;
            IsHidden = service.IsHidden;
            LockLevel = service.LockLevel;
            UserDisplayName = service.UserDisplayName;
            UserLogicalNumber = service.UserLogicalNumber;
        } // TransferMergeProperties

        #region Data extraction for underlying BroadcastService

        private string GetDisplayOriginalName()
        {
            if (Data.ServiceInformation != null)
            {
                var text = Data.ServiceInformation.Name.SafeGetLanguageValue(AppUiConfiguration.Current.User.PreferredLanguagesList, true, null);
                if (text != null) return text;
            } // if

            return string.Format(Properties.Texts.FormatBroadcastUnknownDisplayName, Data.TextualIdentifier.ServiceName, DomainName);
        } // GetDisplayOriginalName

        private string GetDisplayShortName()
        {
            if (Data.ServiceInformation != null)
            {
                var text = Data.ServiceInformation.ProprietaryShortName.SafeGetLanguageValue(AppUiConfiguration.Current.User.PreferredLanguagesList, true, null);
                if (text != null) return text;
            } // if

            return Properties.Texts.NotProvidedValue;
        } // GetDisplayShortName

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

            return Data.ServiceLocation.LocationUrl;
        } // GetLocationUrl

        private string GetProxiedLocationUrl(MulticastProxy proxy)
        {
            if (Data == null) return null;
            if (Data.ServiceLocation == null) return null;
            if (Data.ServiceLocation.IpMulticastAddress != null)
            {
                var multicast = Data.ServiceLocation.IpMulticastAddress;
                return proxy.GetProxiedLocationUrl(multicast.Protocol, multicast.Address, multicast.Port);
            } // if
            if (Data.ServiceLocation.RtspUrl != null) return Data.ServiceLocation.RtspUrl.Value;
            return null;
        } // GetProxiedLocationUrl

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

        private string GetDisplayGenre()
        {
            if ((Data.ServiceInformation != null) && (Data.ServiceInformation.ProprietaryGenre != null))
            {
                return Data.ServiceInformation.ProprietaryGenre.Name;
            } // if

            return Properties.Texts.NotProvidedValue;
        } // GetDisplayGenre

        private string GetDisplayGenreCode()
        {
            if ((Data.ServiceInformation != null) && (Data.ServiceInformation.ProprietaryGenre != null))
            {
                var code = Data.ServiceInformation.ProprietaryGenre.Code;
                if (code.StartsWith("urn:miviewtv:cs:GenreCS:", StringComparison.InvariantCultureIgnoreCase))
                {
                    return code.Substring(21);
                } // if
                return code;
            } // if

            return Properties.Texts.NotProvidedValue;
        } // GetDisplayGenreCode

        private string GetDisplayParentalRating()
        {
            if ((Data.ServiceInformation != null) && (Data.ServiceInformation.ProprietaryParentalGuidance != null) && (Data.ServiceInformation.ProprietaryParentalGuidance.ParentalRating != null))
            {
                return Data.ServiceInformation.ProprietaryParentalGuidance.ParentalRating.Name[0].Value;
            } // if

            return Properties.Texts.NotProvidedValue;
        } // GetDisplayParentalRating

        private string GetDisplayParentalRatingCode()
        {
            if ((Data.ServiceInformation != null) && (Data.ServiceInformation.ProprietaryParentalGuidance != null) && (Data.ServiceInformation.ProprietaryParentalGuidance.ParentalRating != null))
            {
                var code = Data.ServiceInformation.ProprietaryParentalGuidance.ParentalRating.TermUrl;
                if (code.StartsWith("urn:dvb:metadata:cs:ParentalGuidanceCS:", StringComparison.InvariantCultureIgnoreCase))
                {
                    return code.Substring(36);
                } // if
                return code;
            } // if

            return Properties.Texts.NotProvidedValue;
        } // GetDisplayParentalRatingCode

        private string GetDisplayLockLevel()
        {
            // TODO: GetDisplayLockLevel
            return Properties.Texts.NotProvidedValue;
        } // GetDisplayLockLevel

        private ServiceLogo GetLogo()
        {
            return AppUiConfiguration.Current.ServiceLogoMappings.Get(Data.TextualIdentifier.DomainName,
                DomainName,
                Data.TextualIdentifier.ServiceName,
                ServiceType);
        } // GetLogo

        #endregion
    } // class UiBroadcastService
} // namespace
