using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Project.DvbIpTv.Services.EPG
{
    public class ExtendedEpgEvent
    {
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
        } // Actores

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

        public IDictionary<string, string> ProviderProperties
        {
            get;
            set;
        } // ProviderProperties
    } // class ExtendedEpgEvent
} // namespace
