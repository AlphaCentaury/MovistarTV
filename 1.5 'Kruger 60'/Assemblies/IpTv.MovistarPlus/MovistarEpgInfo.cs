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

using Newtonsoft.Json;

namespace IpTviewr.IpTvServices.MovistarPlus
{
    public class MovistarEpgInfo
    {
        public class LanguageInfo
        {
            [JsonProperty("audioType")]
            public string AudioType;

            [JsonProperty("language")]
            public string Language;
        } // LanguageInfo

        [JsonProperty("languages")]
        public LanguageInfo[] Languages;

        [JsonProperty("startOver")]
        public long StartOver;

        [JsonProperty("endtime")]
        public long EndTime;

        [JsonProperty("ageRatingID")]
        public string AgeRatingId;

        [JsonProperty("description")]
        public string Description;

        [JsonProperty("begintime")]
        public long BeginTime;

        [JsonProperty("isHdtv")]
        public int IsHdTv;

        [JsonProperty("title")]
        public string Title;

        [JsonProperty("seriesID")]
        public string SeriesId;

        [JsonProperty("duration")]
        public long Duration;

        [JsonProperty("subgenre")]
        public string SubGenre;

        [JsonProperty("catchUp")]
        public int CatchUp;

        [JsonProperty("genre")]
        public string Genre;

        [JsonProperty("hasDolby")]
        public int HasDolby;

        [JsonProperty("directors")]
        public string[] Directors;

        [JsonProperty("preTime")]
        public long PreTime;

        [JsonProperty("originalLongTitle")]
        public string[] OriginalLongTitle;

        [JsonProperty("countries")]
        public string[] Countries;

        [JsonProperty("version")]
        public string[] Version;

        [JsonProperty("exptime")]
        public long ExpiryTime;

        [JsonProperty("postTime")]
        public long PostTime;

        [JsonProperty("productionDate")]
        public string[] ProductionDate;

        [JsonProperty("originalTitle")]
        public string[] OriginalTitle;

        [JsonProperty("longTitle")]
        public string[] LongTitle;

        [JsonProperty("scriptwriter")]
        public string[] ScriptWriter;

        [JsonProperty("mainActors")]
        public string[] MainActors;

        [JsonProperty("producer")]
        public string[] Producer;

        [JsonProperty("audio")]
        public string[] Audio;

        [JsonProperty("soundtrack")]
        public string[] Soundtrack;
    } // class MovistarEpgInfo
} // namespace
