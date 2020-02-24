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

using System.Collections.Generic;

namespace IpTviewr.Services.EpgDiscovery
{
    public class EpgProgramExtended
    {
        public EpgProgram Program
        {
            get;
            set;
        } // Program

        public string UrlThumbnail
        {
            get;
            set;
        } // UrlThumbnail

        public string Title
        {
            get;
            set;
        } // Title

        public string OriginalTitle
        {
            get;
            set;
        } // OriginalTitle

        public string Genre
        {
            get;
            set;
        } // Genre

        public string SubGenre
        {
            get;
            set;
        } // SubGenre

        public string Synopsis
        {
            get;
            set;
        } // Synopsis

        public string[] Directors
        {
            get;
            set;
        } // Directors

        public string[] Actors
        {
            get;
            set;
        } // Actors

        public string[] Producers
        {
            get;
            set;
        } // Producers

        public string[] ScriptWriters
        {
            get;
            set;
        } // ScriptWriters

        public string[] Country
        {
            get;
            set;
        } // Country

        public string[] ProductionDate
        {
            get;
            set;
        } // ProductionDate

        public int RecordMarginBefore
        {
            get;
            set;
        } // RecordMarginBefore

        public int RecordMarginAfter
        {
            get;
            set;
        } // RecordMarginAfter

        public IDictionary<string, string> AdditionalProperties
        {
            get;
            set;
        } // AdditionalProperties
    } // class EpgProgramExtended
} // namespace
