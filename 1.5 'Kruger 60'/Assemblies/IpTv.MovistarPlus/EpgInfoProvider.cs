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

namespace IpTviewr.IpTvServices.MovistarPlus
{
    /*
    internal class EpgInfoProvider: IEpgInfoProvider
    {
        #region IEpgInfoProvider Members

        // TODO: get from xml settings, updated from http://www-60.svc.imagenio.telefonica.net:2001/appserver/mvtv.do?action=getConfigurationParams
        private const string EpgThumbnailScheme = "http";
        private const string EpgThumbnailHost = "www-60.svc.imagenio.telefonica.net";
        private const int EpgThumbnailPort = 2001;
        private const string EpgThumbnailPortraitSubPath = "portrait/";
        private const string EpgThumbnailLandscapeSubPath = "landscape/";
        private const string EpgThumbnailBigSubPath = "big/";
        private const string EpgThumbnailUrlFormat = "appclient/incoming/covers/programmeImages/{0}{1}{2}/{3}.jpg";

        public EpgInfoProviderCapabilities Capabilities
        {
            get
            {
                return EpgInfoProviderCapabilities.ExtendedInfo |
                    EpgInfoProviderCapabilities.IndependentProgramThumbnail;
            } // get
        } // Capabilities

        public ExtendedEpgEvent GetEpgInfo(UiBroadcastService service, EpgEvent epgEvent, bool portrait)
        {
            var request = GetExtendedInfoRequest(epgEvent);
            if (request == null) return null;

            var movistarEpgInfoResponse = SendRequest<MovistarJsonEpgInfoResponse>(request);
            if (movistarEpgInfoResponse.Code != 0) return null;

            var result = ToExtendedEpgEvent(movistarEpgInfoResponse.Data);
            result.UrlThumbnail = GetEpgProgramThumbnailUrl(service, epgEvent, portrait);

            return result;
        } // GetEpgInfo

        public string GetEpgProgramThumbnailUrl(UiBroadcastService service, EpgEvent epgEvent, bool portrait)
        {
            try
            {
                var crid = MovistarCrId.Get(epgEvent.CRID);
                if (crid == null) return null;

                var builder = new UriBuilder();
                builder.Scheme = EpgThumbnailScheme;
                builder.Host = EpgThumbnailHost;
                builder.Port = EpgThumbnailPort;
                builder.Path = string.Format(EpgThumbnailUrlFormat,
                    portrait? EpgThumbnailPortraitSubPath: EpgThumbnailLandscapeSubPath,
                    EpgThumbnailBigSubPath,
                    crid.ContentIdRoot, crid.ContentId);

                return builder.Uri.ToString();
            }
            catch
            {
                // ignore
                return null;
            } // try-catch
        } // GetEpgProgramThumbnailUrl

        #endregion

        public ExtendedEpgEvent ToExtendedEpgEvent(MovistarEpgInfo info)
        {
            var result = new ExtendedEpgEvent();

            // Title
            if ((info.LongTitle != null) && (info.LongTitle.Length > 0))
            {
                result.Title = info.LongTitle[0];
            }
            else if (info.Title != null)
            {
                result.Title = info.Title;
            } // if-else if

            // Original title
            if ((info.OriginalLongTitle != null) && (info.OriginalLongTitle.Length > 0))
            {
                result.OriginalTitle = info.OriginalLongTitle[0];
            }
            else if ((info.OriginalTitle != null) && (info.OriginalTitle.Length > 0))
            {
                result.OriginalTitle = info.OriginalTitle[0];
            } // if-else if

            // Genre / Subgrenre
            result.Genre = info.Genre;
            result.SubGenre = info.SubGenre;

            // Synopsis
            result.Synopsis = info.Description;

            // Directors
            result.Directors = Split(info.Directors, ',');

            // Actors
            result.Actors = Split(info.MainActors, ',');

            // Producers
            result.Producers = Split(info.Producer, ',');

            // ScriptWriters
            result.ScriptWriters = Split(info.ScriptWriter, ',');

            // Country
            result.Country = Split(info.Countries, ',');

            // Production date
            result.ProductionDate = Split(info.ProductionDate, ',');

            return result;
        } // ToExtendedEpgEvent

        private string[] Split(string[] data, params char[] chars)
        {
            if ((data == null) || (data.Length == 0)) return null;

            var q = from element in data
                    let split = element.Split(chars)
                    from item in split
                    let text = item.Trim()
                    where text != ""
                    select text;

            return q.ToArray();
        } // Split

        private UriBuilder GetExtendedInfoRequest(EpgEvent epgEvent)
        {
            var crid = MovistarCrId.Get(epgEvent.CRID);
            if (crid == null) return null;

            var builder = new UriBuilder();
            builder.Scheme = "http";
            builder.Host = "www-60.svc.imagenio.telefonica.net";
            builder.Port = 2001;
            builder.Path = "appserver/mvtv.do";
            builder.Query = string.Format("action=getEpgInfo&extInfoID={0}&tvWholesaler={1}", crid.ContentId, 1);

            return builder;
        } // GetExtendedInfoRequest

        public static T SendRequest<T>(UriBuilder uri)
        {
            using (var client = new WebClient())
            {
                var data = client.DownloadData(uri.Uri);
                var jsonData = Encoding.UTF8.GetString(data);
                return JsonConvert.DeserializeObject<T>(jsonData);
            } // using client
        } // SendRequest
    } // class EpgInfoProvider
    */
} // namespace
