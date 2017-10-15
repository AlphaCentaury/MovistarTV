// Copyright (C) 2014-2017, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://movistartv.alphacentaury.org/ https://github.com/AlphaCentaury

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Project.IpTv.Services.EPG;
using Project.IpTv.Services.EPG.Serialization;
using Project.IpTv.Services.SqlServerCE;
using Project.IpTv.UiServices.EPG;
using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Project.IpTv.MovistarPlus;

namespace Project.IpTv.Internal.Tools.ConsoleExperiments
{
    static class EpgInfoDownload
    {
        static WebClient WebClient;
        static string BasePath = @"C:\Users\Developer\Documents\IPTV\movistar+ (v1.5 Kruger-60)\Cache";

        public static void Experiment()
        {
            string dbFile = BasePath + @"\EPG.sdf";
            string baseDumpPath = BasePath + @"\ExtendedEPG";
            Directory.CreateDirectory(baseDumpPath);

            Console.Write("Connecting to DB...");
            using (SqlCeConnection cn = DbServices.GetConnection(dbFile))
            {
                Console.WriteLine(" ok");

                using (var webClient = new WebClient())
                {
                    WebClient = webClient;
                    using (SqlCeCommand cmd = new SqlCeCommand())
                    {
                        cmd.CommandText = "SELECT [DbId], [ServiceId] FROM [ServiceId]";
                        cmd.Connection = cn;
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.Write("Loading events for {0}...", reader.GetString(1));

                                var serviceDbId = reader.GetInt32(0);
                                var epgEvents = EpgDbQuery.GetDateRange(cn, serviceDbId);
                                Console.WriteLine(" {0} events", epgEvents.Count);

                                foreach (var epgEvent in epgEvents)
                                {
                                    DownloadEpgExtendedInfo(epgEvent, baseDumpPath);
                                } // foreach
                            } // while
                        } // using reader
                    } // using
                } // using webClient
                WebClient = null;
            } // using cn
        } // Experiment

        private static void DownloadEpgExtendedInfo(EpgEvent epgEvent, string basePath)
        {
            Console.WriteLine("{0} -> {1}: {2}", epgEvent.LocalStartTime, epgEvent.Duration, epgEvent.CRID);

            var crid = new Uri(epgEvent.CRID);
            var components = crid.AbsolutePath.Split('/');
            if (components.Length != 4) throw new FormatException();
            //if (components[1] != "0") throw new FormatException();
            if (components[2] != components[3]) throw new FormatException();
            if (components[3].Length < 5) throw new FormatException();

            var movistarSeriesId = components[1];
            var movistarContentIdRoot = components[3].Substring(0,4);
            var movistarContentId = components[3];

            var builder = new UriBuilder();
            builder.Host = "www-60.svc.imagenio.telefonica.net";
            builder.Port = 2001;
            builder.Scheme = "http";

            Console.Write("Downloading image...");
            builder.Path = string.Format("appclient/incoming/covers/programmeImages/landscape/big/{0}/{1}.jpg", movistarContentIdRoot, movistarContentId);
            var filename = string.Format("{0}-{1}.jpg", movistarSeriesId, movistarContentId);
            filename = Path.Combine(basePath, filename);
            try
            {
                WebClient.DownloadFile(builder.Uri, filename);
                Console.Write(" ok. Downloading JSON...");
            }
            catch (WebException)
            {
                Console.Write(" {0}. Downloading JSON...", "Error");
            }
            catch (Exception ex)
            {
                Console.Write(" {0}. Downloading JSON...", ex.GetType().Name);
            } // try-catch

            try
            {
                builder.Path = string.Format("appserver/mvtv.do");
                builder.Query = string.Format("action=getEpgInfo&extInfoID={0}&tvWholesaler=1", movistarContentId);
                filename = string.Format("{0}-{1}.json", movistarSeriesId, movistarContentId);
                filename = Path.Combine(basePath, filename);
                WebClient.DownloadFile(builder.Uri, filename);
                Console.WriteLine(" ok");
            }
            catch (WebException)
            {
                Console.WriteLine(" {0}", "Error");
            }
            catch (Exception ex)
            {
                Console.WriteLine(" {0}", ex.GetType().Name);
            } // try-catch
        } // DownloadEpgExtendedInfo

        public static void GetJsonSchema()
        {
            string baseDumpPath = BasePath + @"\ExtendedEPG";
            IDictionary<string, IList<Type>> result, resultData;
            int index;

            result = new Dictionary<string, IList<Type>>();
            resultData = new Dictionary<string, IList<Type>>();

            index = 0;
            var files = Directory.GetFiles(baseDumpPath, "*.json");
            foreach (var jsonFile in files)
            {
                Console.WriteLine("{0} ({1:N0} / {2:N0} = {3:P})", Path.GetFileName(jsonFile), index, files.Length, ((double)index) / files.Length);
                var json = File.ReadAllText(jsonFile);
                var entries = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

                foreach (var entry in entries)
                {
                    AddType(entry, result);
                } // foreach

                if (entries.ContainsKey("resultData"))
                {
                    var entriesData = JsonConvert.DeserializeObject<Dictionary<string, object>>(entries["resultData"].ToString());
                    foreach (var entry in entriesData)
                    {
                        AddType(entry, resultData);
                    } // foreach
                } // if

                index++;
            } // foreach

            foreach (var entry in result)
            {
                foreach (var type in entry.Value)
                {
                    Console.WriteLine("{0}: {1}", entry.Key, type.Name);
                } // foreach
            } // foreach

            Console.WriteLine();
            foreach (var entry in resultData)
            {
                foreach (var type in entry.Value)
                {
                    Console.WriteLine("{0}: {1}", entry.Key, type.Name);
                } // foreach
            } // foreach

            using (var output = new StreamWriter(baseDumpPath + "\\json-schema.txt"))
            {
                foreach (var entry in result)
                {
                    foreach (var type in entry.Value)
                    {
                        output.WriteLine("{0}: {1}", entry.Key, type.Name);
                    } // foreach
                } // foreach

                output.WriteLine();
                foreach (var entry in resultData)
                {
                    foreach (var type in entry.Value)
                    {
                        output.WriteLine("{0}: {1}", entry.Key, type.Name);
                    } // foreach
                } // foreach
            } // output
        } // GetJsonSchema

        private static void AddType(KeyValuePair<string, object> entry, IDictionary<string, IList<Type>> dict)
        {
            IList<Type> list;

            if (!dict.TryGetValue(entry.Key, out list))
            {
                list = new List<Type>();
                dict[entry.Key] = list;
            } // if

            if (!list.Contains(entry.Value.GetType()))
            {
                list.Add(entry.Value.GetType());
            } // if
        } // AddType

        public static void ExploreJsonValues()
        {
            string baseDumpPath = BasePath + @"\ExtendedEPG";
            IDictionary<string, IDictionary<string, int>> result, resultData;
            int index;

            result = new Dictionary<string, IDictionary<string, int>>();
            resultData = new Dictionary<string, IDictionary<string, int>>();

            index = 0;
            var files = Directory.GetFiles(baseDumpPath, "*.json");
            foreach (var jsonFile in files)
            {
                Console.WriteLine("{0} ({1:N0} / {2:N0} = {3:P})", Path.GetFileName(jsonFile), index, files.Length, ((double)index) / files.Length);
                var json = File.ReadAllText(jsonFile);
                var response = JsonConvert.DeserializeObject<MovistarJsonEpgInfoResponse>(json);

                AddValue("resultCode", response.Code.ToString(), result);
                AddValue("resultData", (response.Data != null).ToString(), result);

                var data = response.Data;
                if (data != null)
                {
                    //AddValue("languages", (data.Languages != null)? data.Languages.ToString() : null, resultData);
                    //AddArrayLength("languages", data.Languages, resultData);
                    AddValue("startOver", data.StartOver.ToString(), resultData);
                    //AddValue("endTime", data.EndTime.ToString(), resultData);
                    AddValue("ageRatingID", data.AgeRatingId, resultData);
                    AddValue("isHdtv", data.IsHdTv.ToString(), resultData);
                    AddValue("seriesID", data.SeriesId, resultData);
                    AddValue("subgenre", data.SubGenre, resultData);
                    AddValue("catchUp", data.CatchUp.ToString(), resultData);
                    AddValue("genre", data.Genre, resultData);
                    AddValue("genre + subgenre", data.Genre + ": " + data.SubGenre, resultData);
                    AddValue("hasDolby", data.HasDolby.ToString(), resultData);
                    AddArray("directors", data.Directors, resultData);
                    AddValue("preTime", data.PreTime.ToString(), resultData);
                    AddArray("originalLongTitle", data.OriginalLongTitle, resultData);
                    AddArray("countries", data.Countries, resultData);
                    AddArray("version", data.Version, resultData);
                    AddValue("exptime", data.ExpiryTime.ToString(), resultData);
                    AddValue("postTime", data.PostTime.ToString(), resultData);
                    AddArray("productionDate", data.ProductionDate, resultData);
                    AddArray("originalTitle", data.OriginalTitle, resultData);
                    AddArray("scriptwriter", data.ScriptWriter, resultData);
                    AddArray("mainActors", data.MainActors, resultData);
                    AddArray("producer", data.Producer, resultData);
                    AddArray("audio", data.Audio, resultData);
                    AddArray("soundtrack", data.Soundtrack, resultData);
                } // if

                index++;
            } // foreach

            using (var output = new StreamWriter(baseDumpPath + "\\json-values.txt"))
            {
                output.WriteLine(">>>>>>>>>>>>>>>>>>>>");
                output.WriteLine("MovistarJsonEpgInfoResponse");
                output.WriteLine("<<<<<<<<<<<<<<<<<<<<");
                DumpValues(output, result);

                output.WriteLine();
                output.WriteLine(">>>>>>>>>>>>>>>>>>>>");
                output.WriteLine("MovistarEpgInfo");
                output.WriteLine("<<<<<<<<<<<<<<<<<<<<");
                DumpValues(output, resultData);
            } // output
        } // ExploreJsonValues

        private static void AddValue(string name, string value, IDictionary<string, IDictionary<string, int>> dict)
        {
            IDictionary<string, int> values;
            int count;

            if (!dict.TryGetValue(name, out values))
            {
                values = new Dictionary<string, int>();
                dict[name] = values;
            } // if

            if (value == null) value = "<NULL>";
            if (!values.TryGetValue(value, out count))
            {
                values[value] = 1;
            }
            else
            {
                values[value] = count + 1;
            } // if-else
        } // AddValue

        private static void AddArray(string name, string[] values, IDictionary<string, IDictionary<string, int>> dict)
        {
            AddArrayLength(name, values, dict);
            if (values != null)
            {
                foreach (var value in values)
                {
                    AddValue(name, value, dict);
                } // foreach
            } // if
        } // AddArray

        private static void AddArrayLength(string name, string[] values, IDictionary<string, IDictionary<string, int>> dict)
        {
            name = name + ".Length";
            if (values == null)
            {
                AddValue(name, "<null>", dict);
            }
            else
            {
                AddValue(name, values.Length.ToString(), dict);
            } // if-else
        } // AddArrayLength

        private static void AddArrayLength(string name, JArray values, IDictionary<string, IDictionary<string, int>> dict)
        {
            name = name + ".JArray.Length";
            if (values == null)
            {
                AddValue(name, "<null>", dict);
            }
            else
            {
                AddValue(name, values.Count.ToString(), dict);
            } // if-else
        } // AddValue

        private static void DumpValues(TextWriter output, IDictionary<string, IDictionary<string, int>> dict)
        {
            var q = from entry in dict
                    orderby entry.Key
                    select entry;

            foreach (var entry in q)
            {
                output.WriteLine("********************");
                output.WriteLine(entry.Key);
                output.WriteLine("********************");

                foreach (var value in entry.Value)
                {
                    output.WriteLine("{1} ~ {0}", value.Key, value.Value);
                } // foreach
            } // foreach
        } // DumpValues

        internal static void DisplayJsonData()
        {
            string baseDumpPath = BasePath + @"\ExtendedEPG";

            var index = 0;
            var files = Directory.GetFiles(baseDumpPath, "*.json");

            var q = from jsonFile in files.Skip(51)
                    let json = File.ReadAllText(jsonFile)
                    let response = JsonConvert.DeserializeObject<MovistarJsonEpgInfoResponse>(json)
                    where response.Code == 0
                    select response.Data;
        } // DisplayJsonData
    } // static class EpgInfoDownload
} // namespace
