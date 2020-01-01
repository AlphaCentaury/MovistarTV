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
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Xml.Serialization;

namespace IpTviewr.Services.EpgDiscovery
{
    [Serializable()]
    [DebuggerStepThrough]
    [DesignerCategory("code")]
    [XmlType(TypeName = "Program", Namespace = Common.XmlNamespace)]
    public class EpgProgram
    {
        [XmlAttribute("id")]
        public string Id
        {
            get;
            set;
        } // Id

        [XmlElement("Title")]
        public string Title
        {
            get;
            set;
        } // Tile

        [XmlElement("Genre")]
        public EpgCodedValue Genre
        {
            get;
            set;
        } // Genre

        [XmlElement("ParentalRating")]
        public EpgCodedValue ParentalRating
        {
            get;
            set;
        } // ParentalRating

        [XmlElement("StartTime")]
        public DateTime UtcStartTime
        {
            get;
            set;
        } // UtcStartTime

        [XmlIgnore]
        public DateTime LocalStartTime => UtcStartTime.ToLocalTime();

        [XmlIgnore]
        public DateTime UtcEndTime => UtcStartTime + Duration;

        [XmlIgnore]
        public DateTime LocalEndTime => UtcEndTime.ToLocalTime();

        [XmlIgnore]
        public TimeSpan Duration
        {
            get;
            set;
        } // Duration

        [XmlElement("Duration")]
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public string XmlDuration
        {
            get => SoapDuration.ToString(Duration);
            set => Duration = string.IsNullOrEmpty(value) ? new TimeSpan() : SoapDuration.Parse(value);
        } // XmlDuration

        [XmlAttribute("isBlank")]
        public bool IsBlank
        {
            get;
            set;
        } // IsBlank

        public EpgProgramEpisode Episode
        {
            get;
            set;
        } // Episode

        public bool IsCurrent(DateTime referenceTime)
        {
            if (referenceTime.Kind != DateTimeKind.Utc)
            {
                referenceTime = referenceTime.ToUniversalTime();
            } // if

            return (referenceTime >= UtcStartTime) && (referenceTime < UtcEndTime);
        } // IsCurrent

        public bool IsOld(DateTime referenceTime)
        {
            if (referenceTime.Kind != DateTimeKind.Utc)
            {
                referenceTime = referenceTime.ToUniversalTime();
            } // if

            return (referenceTime >= UtcEndTime);
        } // IsOld

        public bool IsAfter(DateTime referenceTime)
        {
            if (referenceTime.Kind != DateTimeKind.Utc)
            {
                referenceTime = referenceTime.ToUniversalTime();
            } // if

            return (referenceTime < UtcStartTime);
        } // IsAfter

        public static EpgProgram FromScheduleEvent(TvAnytime.TvaScheduleEvent item)
        {
            if (item == null) return null;

            var utcStartTime = item.StartTime ?? item.PublishedStartTime;
            if (utcStartTime == null) return null;

            var result = new EpgProgram()
            {
                Id = item.Program.Crid,
                Duration = (item.Duration.TotalSeconds > 0) ? item.Duration : item.PublishedDuration,
                UtcStartTime = utcStartTime.Value
            };

            if (item.Description == null)
            {
                result.Title = Properties.Texts.EpgBlankTitle;
                result.IsBlank = true;
                return result;
            }

            result.Title = item.Description.Title;
            result.Genre = EpgCodedValue.ToCodedValue(item.Description.Genre);
            result.ParentalRating = (item.Description.ParentalGuidance != null) ? EpgCodedValue.ToCodedValue(item.Description.ParentalGuidance.ParentalRating) : null;

            var releaseDate = item.Description.ReleaseInfo?.ReleaseDate;
            if (releaseDate != null)
            {
                result.Episode = new EpgProgramEpisode()
                {
                    Number = releaseDate.Episode?.Nullable,
                    Season = releaseDate.Season?.Nullable,
                    Year = releaseDate.Year?.Nullable,
                };
            } // if

            if (item.EpisodeOf == null) return result;

            var episode = result.Episode ?? new EpgProgramEpisode();
            episode.SeriesId = item.EpisodeOf.Crid;
            episode.SeriesName = item.EpisodeOf.Title;
            result.Episode = episode;

            return result;
        } // FromScheduleEvent

        public override string ToString() => $"{Title} @ {LocalStartTime}";
    } // EPGEvent
} // namespace
