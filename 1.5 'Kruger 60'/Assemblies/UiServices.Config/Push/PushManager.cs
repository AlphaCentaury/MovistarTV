using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using IpTviewr.Common.Serialization;
using IpTviewr.UiServices.Configuration.Push.v1;

namespace IpTviewr.UiServices.Configuration.Push
{
    public static class PushManager
    {
        public const string BaseUrl = "https://raw.githubusercontent.com/wiki/AlphaCentaury/MovistarTV/push/";
        public const string BaseUrlAlt = "https://movistartv.alphacentaury.org/push/";

        public const string UpdateFile = "update.v1.xml";
        public const string NewsFile = "news.v1.xml";

        public static async void CheckForUpdatesAsync(Action<PushUpdateData, PushNewsData> processPush)
        {
            var updateData = await Deserialize<PushUpdateData>(UpdateFile);
            var newsData = await Deserialize<PushNewsData>(NewsFile);

            processPush?.Invoke(updateData, newsData);
        } // CheckForUpdatesAsync

        public static (PushUpdate Update, PushNews News) GetLatest(PushUpdateData updateData, PushNewsData newsData)
        {
            return (GetLatest(updateData), GetLatest(newsData));
        } // GetLatest

        public static PushUpdate GetLatest(PushUpdateData data)
        {
            return null;
        } // GetLatest

        public static PushNews GetLatest(PushNewsData data)
        {
            return null;
        } // GetLatest

        private static async Task<T> Deserialize<T>(string file) where T : class
        {
            HttpClient client = null;
            T data = null;

            // first-try
            try
            {
                client = new HttpClient();
                var uri = new Uri(BaseUrl + file);
                var stream = await client.GetStreamAsync(uri);
                data = await Task.Run(() => XmlSerialization.Deserialize<T>(stream));
            }
            catch
            {
                // ignore
            } // try-catch

            // second-try
            try
            {
                if (client != null)
                {
                    var uri = new Uri(BaseUrlAlt + file);
                    var stream = await client.GetStreamAsync(uri);
                    data = await Task.Run(() => XmlSerialization.Deserialize<T>(stream));
                } // if
            }
            catch
            {
                // ignore
            } // try-catch

            client?.Dispose();

            return data;
        } // Deserialize
    } // PushManager
} // class PushManager