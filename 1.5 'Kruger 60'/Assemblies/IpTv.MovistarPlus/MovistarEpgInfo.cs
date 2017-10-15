// Copyright (C) 2014-2017, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://movistartv.alphacentaury.org/ https://github.com/AlphaCentaury

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.IpTv.MovistarPlus
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
