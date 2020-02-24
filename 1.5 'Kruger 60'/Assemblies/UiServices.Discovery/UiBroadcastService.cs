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
using Etsi.Ts102034.v010501.XmlSerialization.Common;
using IpTviewr.UiServices.Configuration;
using IpTviewr.UiServices.Configuration.Logos;
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
    [XmlType(TypeName = "UI-BroadcastService", Namespace = SerializationCommon.XmlNamespace)]
    public class UiBroadcastService
    {
        private string _displayOriginalName;
        private string _displayShortName;
        private string _displayDescription;
        private string _displayGenre;
        private string _displayGenreCode;
        private string _displayParentalRating;
        private string _displayParentalRatingCode;
        private string _displayLockLevel;
        private string _originalLocationUrl;
        private string _displayServiceType;
        [NonSerialized]
        private ServiceLogo _logo;

        /// <remarks>Used by Serialization</remarks>
        public UiBroadcastService()
        {
        } // constructor

        public override string ToString() => $"{ServiceLogicalNumber} {DisplayName}";

        public static string GetKey(TextualIdentifier serviceIdentifier, string defaultDomainName)
        {
            var domain = serviceIdentifier.DomainName ?? defaultDomainName;

            return string.Format(Properties.InvariantTexts.FormatServiceProviderKey, serviceIdentifier.ServiceName.ToLowerInvariant(), domain.ToLowerInvariant());
        } // CreateKey

        public UiBroadcastService(IpService service, string providerDomainName)
        {
            Data = service ?? throw new ArgumentNullException("IpService service");
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
        public bool IsTelevisionService
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
        } // IsTelevisionService

        [XmlIgnore]
        public string DisplayLogicalNumber => UserLogicalNumber ?? (ServiceLogicalNumber ?? Properties.Texts.ChannelNumberNone);

        [XmlIgnore]
        public string DisplayName => UserDisplayName ?? DisplayOriginalName;

        [XmlIgnore]
        public string DisplayShortName => _displayShortName ?? (_displayShortName = GetDisplayShortName());

        [XmlIgnore]
        public string DisplayOriginalName => _displayOriginalName ?? (_displayOriginalName = GetDisplayOriginalName());

        [XmlIgnore]
        public string DisplayDescription => _displayDescription ?? (_displayDescription = GetDisplayDescription());

        [XmlIgnore]
        public string DisplayServiceType => _displayServiceType ?? (_displayServiceType = GetDisplayServiceType());

        [XmlIgnore]
        public string DisplayLocationUrl => LocationUrl ?? Properties.Texts.NotProvidedValue;

        [XmlIgnore]
        public string DisplayGenre => _displayGenre ?? (_displayGenre = GetDisplayGenre());

        [XmlIgnore]
        public string DisplayGenreCode => _displayGenreCode ?? (_displayGenreCode = GetDisplayGenreCode());

        [XmlIgnore]
        public string DisplayParentalRating => _displayParentalRating ?? (_displayParentalRating = GetDisplayParentalRating());

        [XmlIgnore]
        public string DisplayParentalRatingCode => _displayParentalRatingCode ?? (_displayParentalRatingCode = GetDisplayParentalRatingCode());

        [XmlIgnore]
        public string DisplayLockLevel => _displayLockLevel ?? (_displayLockLevel = GetDisplayLockLevel());

        #endregion

        #region UI additional data

        public string DomainName
        {
            get;
            set;
        } // DomainName

        [XmlIgnore]
        public ServiceLogo Logo => _logo ?? (_logo = GetLogo());

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
        public string ServiceName => Data.TextualIdentifier.ServiceName;

        [XmlIgnore]
        public string FullServiceName => ServiceName + "." + DomainName;

        [XmlIgnore]
        public TextualIdentifier ReplacementService
        {
            get
            {
                var replacements = Data.ServiceInformation?.ReplacementService;
                if (replacements == null || (replacements.Length == 0)) return null;

                var q = from r in replacements
                        let ti = r.TextualIdentifier
                        where ti != null
                        select ti;
                var replacement = q.FirstOrDefault();

                return replacement;
            } // get
        } // ReplacementService

        [XmlIgnore]
        public string ServiceType => Data.ServiceInformation?.ServiceType;

        [XmlIgnore]
        public string LocationUrl
        {
            get
            {
                var networkSettings = NetworkSettingsRegistration.Settings;
                if (networkSettings?.MulticastProxy == null || (networkSettings.MulticastProxy.IsEnabled == false))
                {
                    return OriginalLocationUrl;
                } // if

                return GetProxiedLocationUrl(networkSettings.MulticastProxy);
            } // get
        } // LocationUrl

        [XmlIgnore]
        public string OriginalLocationUrl => _originalLocationUrl ?? (_originalLocationUrl = GetLocationUrl());

        #endregion

        public bool IsSameService(UiBroadcastService service)
        {
            if (Key != service.Key) return false;
            if (LocationUrl != service.LocationUrl) return false;
            if (DisplayOriginalName != service.DisplayOriginalName) return false;
            return ServiceType == service.ServiceType;
        } // IsSameService

        // v1.0 RC 0: code moved from ChannelList > ChanneListForm.cs > DumpProperties(UiBroadcastService)

        public IList<Property> DumpProperties()
        {
            var properties = new List<Property>
            {
                new Property("Name (display)", DisplayName),
                new Property("Description (display)", DisplayDescription),
                new Property("Type (display)", DisplayServiceType),
                new Property("Location URL (display)", DisplayLocationUrl),
                new Property("Is active", (!IsInactive).ToString())
            };


            if (Data.ServiceLocation == null)
            {
                properties.Add(new Property("Service location", null));
            }
            else
            {
                properties.Add(new Property("Service location (multicast)", Data.ServiceLocation?.IpMulticastAddress?.Url));

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
                var q = from triplet in Data.DvbTriplet
                        select new Property("DVB Triplet", $"OrigNetId='{triplet.OrigNetId}', TSId='{triplet.TsId}', ServiceId='{triplet.ServiceId}'");
                properties.AddRange(q);
            } // if-else

            properties.Add(new Property("Max bitrate", Data.MaxBitrate));

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
                    var q = from txt in Data.ServiceInformation.Name
                            select Utils.GetLanguageProperty("Name", txt);
                    properties.AddRange(q);
                } // if-else
                if (Data.ServiceInformation.Description == null)
                {
                    properties.Add(new Property("Description", null));
                }
                else
                {
                    var q = from txt in Data.ServiceInformation.Description
                            select Utils.GetLanguageProperty("Description", txt);
                    properties.AddRange(q);
                } // if-else

                if ((Data.ServiceInformation.ServiceDescriptionLocation == null) || (Data.ServiceInformation.ServiceDescriptionLocation.Length == 0))
                {
                    properties.Add(new Property("Description location", null));
                }
                else
                {
                    var q = from location in Data.ServiceInformation.ServiceDescriptionLocation
                            select new Property("Description location", location.Value);
                    properties.AddRange(q);
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
                        if (replacement.DvbTriplet != null)
                        {
                            properties.Add(new Property("Replacement service",
                                $"DVB Triplet: OrigNetId='{triplet.OrigNetId}', TSId='{triplet.TsId}', ServiceId='{triplet.ServiceId}'"));
                        } // if
                        var textual = replacement.TextualIdentifier;
                        if (textual != null)
                        {
                            properties.Add(new Property("Replacement service",
                                $"Identifier: Name='{textual.ServiceName}', Domain='{textual.DomainName}'"));

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

            // AudioAttributes
            properties.Add(new Property("Has audio details", (Data.AudioAttributes != null).ToString()));

            // VideoAttributes
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
            if (Data.ServiceInformation == null)
            {
                return string.Format(Properties.Texts.FormatBroadcastUnknownDisplayName,
                    Data.TextualIdentifier.ServiceName, DomainName);
            } // if

            var text = Data.ServiceInformation.Name.SafeGetLanguageValue(AppConfig.Current.User.PreferredLanguagesList, true, null);
            return text ?? string.Format(Properties.Texts.FormatBroadcastUnknownDisplayName, Data.TextualIdentifier.ServiceName, DomainName);
        } // GetDisplayOriginalName

        private string GetDisplayShortName()
        {
            if (Data.ServiceInformation == null) return Properties.Texts.NotProvidedValue;

            var text = Data.ServiceInformation.ProprietaryShortName.SafeGetLanguageValue(AppConfig.Current.User.PreferredLanguagesList, true, null);
            return text ?? Properties.Texts.NotProvidedValue;
        } // GetDisplayShortName

        private string GetDisplayDescription()
        {
            if (Data.ServiceInformation == null) return Properties.Texts.BroadcastUnknownDisplayDescription;

            var text = Data.ServiceInformation.Description.SafeGetLanguageValue(AppConfig.Current.User.PreferredLanguagesList, true, null);
            return text ?? Properties.Texts.BroadcastUnknownDisplayDescription;
        } // GetDisplayDescription

        private string GetLocationUrl()
        {
            return Data.ServiceLocation?.LocationUrl;
        } // GetLocationUrl

        private string GetProxiedLocationUrl(MulticastProxy proxy)
        {
            if (Data?.ServiceLocation == null) return null;
            if (Data.ServiceLocation.IpMulticastAddress == null)
            {
                return Data.ServiceLocation.RtspUrl?.Value;
            } // if

            var multicast = Data.ServiceLocation.IpMulticastAddress;
            return proxy.GetProxiedLocationUrl(multicast.Protocol, multicast.Address, multicast.Port);
        } // GetProxiedLocationUrl

        private string GetDisplayServiceType()
        {
            if (Data.ServiceInformation == null)
            {
                return Properties.Texts.NotProvidedValue;
            } // if-else

            var serviceType = ServiceType;
            if (!AppConfig.Current.DescriptionServiceTypes.TryGetValue(serviceType, out var serviceTypeDescription))
            {
                serviceTypeDescription = string.Format(Properties.Texts.FormatServiceTypeIdUnknown, serviceType);
            } // if

            return serviceTypeDescription;
        } // GetDisplayServiceType

        private string GetDisplayGenre()
        {
            return Data.ServiceInformation?.ProprietaryGenre != null ? Data.ServiceInformation.ProprietaryGenre.Name : Properties.Texts.NotProvidedValue;
        } // GetDisplayGenre

        private string GetDisplayGenreCode()
        {
            if (Data.ServiceInformation?.ProprietaryGenre == null)
            {
                return Properties.Texts.NotProvidedValue;
            } // if

            var code = Data.ServiceInformation.ProprietaryGenre.Code;
            return code.StartsWith("urn:miviewtv:cs:GenreCS:", StringComparison.InvariantCultureIgnoreCase) ? code.Substring(21) : code;
        } // GetDisplayGenreCode

        private string GetDisplayParentalRating()
        {
            if ((Data.ServiceInformation?.ProprietaryParentalGuidance?.ParentalRating?.Name?.Length ?? 0) == 0)
            {
                return Properties.Texts.NotProvidedValue;
            } // if

            return Data.ServiceInformation.ProprietaryParentalGuidance.ParentalRating.Name[0].Value;
        } // GetDisplayParentalRating

        private string GetDisplayParentalRatingCode()
        {
            if (Data.ServiceInformation?.ProprietaryParentalGuidance?.ParentalRating == null)
            {
                return Properties.Texts.NotProvidedValue;
            } // if

            var code = Data.ServiceInformation.ProprietaryParentalGuidance.ParentalRating.TermUrl;
            return code.StartsWith("urn:dvb:metadata:cs:ParentalGuidanceCS:", StringComparison.InvariantCultureIgnoreCase) ? code.Substring(36) : code;
        } // GetDisplayParentalRatingCode

        private string GetDisplayLockLevel()
        {
            // TODO: GetDisplayLockLevel
            return Properties.Texts.NotProvidedValue;
        } // GetDisplayLockLevel

        private ServiceLogo GetLogo()
        {
            return AppConfig.Current.ServiceLogoMappings.Get(Data.TextualIdentifier.DomainName,
                DomainName,
                Data.TextualIdentifier.ServiceName,
                ServiceType);
        } // GetLogo

        #endregion
    } // class UiBroadcastService
} // namespace
