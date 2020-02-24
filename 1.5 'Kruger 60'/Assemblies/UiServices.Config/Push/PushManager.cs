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

using IpTviewr.Common.Serialization;
using IpTviewr.UiServices.Configuration.Push.UI;
using IpTviewr.UiServices.Configuration.Push.v1;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using IpTviewr.UiServices.Common.Forms;

namespace IpTviewr.UiServices.Configuration.Push
{
    public static class PushManager
    {
        public const string BaseUrl = "https://raw.githubusercontent.com/wiki/AlphaCentaury/MovistarTV/push/";
        public const string BaseUrlAlt = "https://movistartv.alphacentaury.org/push/";

        public const string UpdateFile = "update.v1.xml";
        public const string NewsFile = "news.v1.xml";

        public static async Task<PushUpdateResult> CheckForUpdatesAsync(IPushUpdateContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            var result = new PushUpdateResult
            {
                Context = context
            };

            var updateData = await Deserialize<PushUpdates>(UpdateFile);
            result.Updates = updateData;
            if (updateData == null) return result;

            var update = GetLatest(updateData, context);
            result.LastUpdate = update;
            context.LastChecked = DateTime.UtcNow;

            return result;
        } // CheckForUpdatesUnattendedAsync

        public static async Task<PushUpdateResult> CheckForUpdatesUiAsync(Form owner, IPushUpdateContext context, TimeSpan checkEvery)
        {
            var elapsed = DateTime.UtcNow - context.LastChecked;
            if (elapsed < checkEvery) return null;

            var result = await CheckForUpdatesAsync(context);
            if (result.LastUpdate != null)
            {
                CommonBaseForm.BeginInvoke(owner, DisplayUpdate, owner, result);
            } // if

            return result;
        } // CheckForUpdatesUiAsync

        public static void CheckForUpdates(Form owner, IPushUpdateContext context)
        {
            if (owner == null) throw new ArgumentNullException(nameof(owner));
            if (context == null) throw new ArgumentNullException(nameof(context));

            using var form = new UpdatesForm
            {
                UpdateContext = context
            };

            if (form.ShowDialog(owner) != DialogResult.OK) return;
            if (form.DoNotShowAgain) context.AddHidden(form.UpdateData.Id);
        } // CheckForUpdates

        public static void DisplayUpdate(Form owner, PushUpdateResult result)
        {
            if (owner == null) throw new ArgumentNullException(nameof(owner));
            if (result == null) throw new ArgumentNullException(nameof(result));
            if (result.LastUpdate == null) return;

            using var form = new UpdatesForm
            {
                UpdateContext = result.Context,
                UpdateData = result.LastUpdate
            };

            if (form.ShowDialog(owner) != DialogResult.OK) return;
            if (form.DoNotShowAgain) result.Context.AddHidden(result.LastUpdate.Id);
        } // DisplayUpdate

        public static PushUpdate GetLatest(PushUpdates data, IPushUpdateContext context)
        {
            if ((data == null) || !data.UpdatesSpecified) return null;

            var appVersion = context.GetAppVersion();
            foreach (var update in data.Updates)
            {
                if (!Version.TryParse(update.Version, out var version)) continue;

                // we assume the latest version is ALWAYS at the top of the list
                // meaning: the list MUST be sorted by descending version

                if (version <= appVersion) break;
                if (!context.IsHidden(update.Id)) return update;
            } // foreach

            return null;
        } // GetLatest

        private static async Task<T> Deserialize<T>(string file) where T : class
        {
            HttpClient client = null;
            T data = null;

            var tries = 1;
            var main = true;
            var alt = true;

            while (true)
            {
                // first-try
                try
                {
                    client = new HttpClient();
                    if (main)
                    {
                        (data, main) = await Deserialize<T>(client, BaseUrl + file);
                    } // if

                    if ((data == null) && alt)
                    {
                        (data, alt) = await Deserialize<T>(client, BaseUrlAlt + file);
                    } // if

                    if ((data != null) || (tries >= 3) || (!main && !alt)) break;
                }
                catch
                {
                    break;
                } // try-catch

                tries++;
                Thread.Sleep(1000);
            } // while

            client?.Dispose();
            return data;
        } // Deserialize

        private static async Task<(T data, bool canBeRetried)> Deserialize<T>(HttpClient client, string url) where T : class
        {
            try
            {
                var uriBuilder = new UriBuilder(url) { Query = $"cache={DateTime.UtcNow.Ticks:D}" };
                var response = await client.GetAsync(uriBuilder.Uri);

                if (response.StatusCode == HttpStatusCode.NotFound) return (null, false);
                if (!response.IsSuccessStatusCode) return (null, true);

                var stream = await response.Content.ReadAsStreamAsync();
                var data = await Task.Run(() => XmlSerialization.Deserialize<T>(stream));

                return (data, true);
            }
            catch (HttpRequestException)
            {
                // Error may be a transient condition
                return (null, true);
            }
            catch
            {
                return (null, false);
            } // catch
        } // Deserialize
    } // PushManager
} // class PushManager
