// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Project.DvbIpTv.Common;
using Project.DvbIpTv.Common.Serialization;
using Project.DvbIpTv.DvbStp.Client;
using Project.DvbIpTv.Services.EPG;
using Project.DvbIpTv.Services.EPG.Serialization;
using Project.DvbIpTv.Services.EPG.TvAnytime;
using Project.DvbIpTv.Services.SqlServerCE;
using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Project.DvbIpTv.Internal.ConsoleEPGLoader
{
    class Program
    {
        internal static AutoResetEvent MainEvent;
        internal static string DbFile;
        internal static string XmlFilesPath;
        internal static Exception Exception;

        static int Main(string[] args)
        {
            try
            {
                var StartTime = DateTime.Now;

                // Set console icon
                using (var icon = Properties.Resources.ConsoleEpgLoader)
                {
                    UnsafeNativeMethods.SetConsoleIcon(icon.Handle);
                } // using icon

                Console.Title = "TV-Anytime EPG loader utility";
                DisplayLogo();

                if (!ProcessArguments(args))
                {
                    return 1;
                } // if

                PrepareDatabase();
                CompactDatabase();

                ProcessEpgSource(new List<KeyValuePair<IPAddress, short>>()
                    {
                        {new KeyValuePair<IPAddress, short>(IPAddress.Parse("239.0.2.130"), 3937) },
                        {new KeyValuePair<IPAddress, short>(IPAddress.Parse("239.0.2.131"), 3937) },
                        {new KeyValuePair<IPAddress, short>(IPAddress.Parse("239.0.2.132"), 3937) },
                        {new KeyValuePair<IPAddress, short>(IPAddress.Parse("239.0.2.133"), 3937) },
                        {new KeyValuePair<IPAddress, short>(IPAddress.Parse("239.0.2.134"), 3937) },
                        {new KeyValuePair<IPAddress, short>(IPAddress.Parse("239.0.2.135"), 3937) },
                        {new KeyValuePair<IPAddress, short>(IPAddress.Parse("239.0.2.136"), 3937) },
                    });

                Log("Main thread {0} waiting for processing threads to end...", Thread.CurrentThread.ManagedThreadId);
                MainEvent = new AutoResetEvent(false);
                MainEvent.WaitOne();

                UpdateDbStatus(0);
                CompactDatabase();

                Console.WriteLine();
                Console.WriteLine("=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");
                Console.WriteLine("Ellapsed time: {0}", FormatString.TimeSpanTotalMinutes(DateTime.Now - StartTime, FormatString.Format.Extended));
                Console.WriteLine();

                return 0;
            }
            catch (Exception ex)
            {
                Exception = ex;
            } // try-catch

            if (Exception != null)
            {
                try
                {
                    UpdateDbStatus(-1);
                }
                catch
                {
                    // ignore
                } // try-catch

                Console.WriteLine();
                Console.WriteLine("UNEXPECTED EXCEPTION!");
                Console.WriteLine(Exception.GetType().FullName);
                Console.WriteLine(Exception.Message);
                Console.WriteLine();
                MyApplication.HandleException(null, Exception);

                return -1;
            } // if

            return 0;
        } // Main

        static void DisplayLogo()
        {
            string copyright;

            // get copyright text
            object[] attributes = Assembly.GetEntryAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
            if (attributes.Length == 0)
            {
                copyright = "Copyright (C) http://movistartv.codeplex.com";
            }
            copyright = ((AssemblyCopyrightAttribute)attributes[0]).Copyright;

            Console.WriteLine(Properties.Texts.StartLogo, Assembly.GetEntryAssembly().GetName().Version, copyright);
            Console.WriteLine();
        } // DisplayLogo

        static bool ProcessArguments(string[] args)
        {
            if ((args == null) || (args.Length == 0))
            {
                DisplayLogo();
                Console.WriteLine("ERROR: No arguments have been specified.");
                return false;
            } // if

            var parser = new CommandLineArguments()
            {
                SpecialHelpArgument = true
            };

            var arguments = parser.Parse(args);
            if (!parser.IsOk)
            {
                DisplayLogo();
                Console.WriteLine("ERROR: Invalid argument format.");
                return false;
            } // if

            return ProcessArguments(arguments);
        } // ProcessArguments

        static bool ProcessArguments(IDictionary<string, string> arguments)
        {
            string value;

            if (arguments.TryGetValue("Database", out value))
            {
                if (!File.Exists(value))
                {
                    Console.WriteLine("Database file not found: {0}", value);
                    return false;
                } // if

                DbFile = value;
#if DEBUG
                XmlFilesPath = System.IO.Path.GetDirectoryName(DbFile) + "\\EPG";
                System.IO.Directory.CreateDirectory(XmlFilesPath);
#else
                XmlFilesPath = null;
#endif
            } // if

            if (arguments.TryGetValue("ForceUiCulture", out value))
            {
                ForceUiCulture(value);
            } // if

            return true;
        } // ProcessArguments

        private static void ForceUiCulture(string culture)
        {
            if (culture == null) return;
            culture = culture.Trim();
            if (culture == string.Empty) return;

            try
            {
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(culture);
            }
            catch (Exception ex)
            {
                MyApplication.HandleException(null, string.Format("Invalid argument value for /ForceUiCulture:{0}", culture), ex);
            } // try-catch
        } // ForceUiCulture

        static void PrepareDatabase()
        {
            Console.Write("Conecting to database...");
            if (EpgDbQuery.GetStatus(DbFile).IsNew)
            {
                var box = new Microsoft.SqlServer.MessageBox.ExceptionMessageBox()
                {
                    Caption = "Utilidad de descarga y actualización de EPG TV-Anytime",
                    Text = "Se va a proceder a descargar la información de la guía electrónica de programas (EPG) por primera vez. " +
                    "La descarga de la EPG en la primera ejecución es un proceso muy lento (entre 10 y 15 minutos).\r\n\r\n" +
                    "Durante el proceso de descarga la información de EPG o bien no estará disponible o será incompleta.\r\n" +
                    "En las siguientes ejecuciones del programa, la información EPG se actualizará en segundo plano de manera automática (si han transcurrido más de 24 horas).",
                    Beep = true,
                    Symbol = Microsoft.SqlServer.MessageBox.ExceptionMessageBoxSymbol.Warning
                };
                box.Show(null);
            } // if
            UpdateDbStatus(1);
            Console.WriteLine(" ok");

            Console.Write("Deleting old EPG data...");
            EpgDbSerialization.DeleteAllEvents(DbFile, null, DateTime.Now - new TimeSpan(6, 0, 0));
            Console.WriteLine(" ok");

            if (XmlFilesPath != null)
            {
                Console.Write("Deleting old EPG XML data...");
                foreach (var file in Directory.GetFiles(XmlFilesPath, "*.xml"))
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch
                    {
                         // ignore
                    }
                } // foreach
                Console.WriteLine(" ok");
            } // if
        } // PrepareDatabase

        static void CompactDatabase()
        {
            Console.Write("Compacting EPG database...");

            try
            {
                using (var engine = new SqlCeEngine())
                {
                    engine.LocalConnectionString = "Data source=\"" + DbFile + "\";Password=\"movistartv.codeplex.com\"";
                    engine.Compact("Data source=;Password=\"movistartv.codeplex.com\"");
                } // using engine
            }
            catch (Exception ex)
            {
                Console.WriteLine(" error");
                Console.WriteLine(ex.Message);
            } // try-catch

            Console.WriteLine(" ok");
        } // CompactDatabase

        private static void ProcessEpgSource(IList<KeyValuePair<IPAddress, short>> addresses)
        {
            var list = addresses;

            ThreadPool.QueueUserWorkItem(delegate(object state) { ProcessEpgSourceAsync(list); });
        } // ProcessEpgSource

        private static void ProcessEpgSourceAsync(IList<KeyValuePair<IPAddress, short>> list)
        {
            var events = new AutoResetEvent[list.Count];

            for (int index = 0; index < list.Count; index++)
            {
                var myIndex = index;
                var address = list[myIndex];
                events[myIndex] = new AutoResetEvent(false);

                ThreadPool.QueueUserWorkItem(delegate(object o)
                {
                    try
                    {
                        Log("=== Loading EPG data from {0}:{1} ===", address.Key, address.Value);

                        var client = new DvbStp.Client.DvbStpClient(address.Key, address.Value);
                        client.NoDataTimeout = -1;
                        client.OperationTimeout = (60 * 60) * 1000; // 60 minutes
                        client.DatagramReceived += Client_DatagramReceived;
                        client.SegmentDataDownloaded += Client_SegmentDataDownloaded;
                        client.SegmentReceived += Client_SegmentReceived;

                        client.DownloadSegments(null);

                        Log("=== EPG data from {0}:{1} downloaded ===", address.Key, address.Value);
                    }
                    catch (Exception ex)
                    {
                        MyApplication.HandleException(null, "Unexpected error while downloading EPG data", ex);
                        Exception = ex;
                    } // try-catch
                    events[myIndex].Set();
                });
            } // foreach

            foreach (var autoEvent in events)
            {
                autoEvent.WaitOne();
            } // foreach

            MainEvent.Set();
        } // ProcessEpgSourceAsync

        static int progressCount;

        static void Client_DatagramReceived(DvbStpClient client, byte payloadId, byte segmentIdNetworkHi, byte segmentIdNetworkLo, byte segmentVersion, bool filtered)
        {
            progressCount = (progressCount + 1) % 25;
            Console.Title = string.Format("TV-Anytime EPG loader utility - Receiving EPG data {0}", new string('#', progressCount));
        } // Client_DatagramReceived

        static void Client_SegmentDataDownloaded(DvbStpClient client, SegmentAssembler segmentData)
        {
            Log("[{0}] Received {1}: {2:N0} bytes", client.MulticastIpAddress, segmentData.SegmentIdentity, segmentData.ReceivedBytes);
            ThreadPool.QueueUserWorkItem(delegate(object state) { ProcessEpgPayload(client.MulticastIpAddress, segmentData, DbFile); });
        } // Client_SegmentDataDownloaded

        static void Client_SegmentReceived(DvbStpClient client, DvbStpSegmentIdentity segmentIdentity, int round)
        {
            Console.WriteLine("[{0}] {1} round {2}", client.MulticastIpAddress, segmentIdentity, round);
        } // Client_SegmentReceived

        static void ProcessEpgPayload(IPAddress ipAddress, SegmentAssembler segmentData, string dbFile)
        {
            var data = segmentData.GetPayload();

            if (XmlFilesPath != null)
            {
                var file = string.Format("{0}_{1}.xml", ipAddress.ToString().Replace('.', '-'), segmentData.SegmentIdentity);
                System.IO.File.WriteAllBytes(System.IO.Path.Combine(XmlFilesPath, file), data);
            } // if

            using (var cn = DbServices.GetConnection(dbFile))
            {
                ProcessEpgPayload(data, cn);
                cn.Close();
            } // using cn
        } // ProcessFile

        static void ProcessEpgPayload(byte[] data, SqlCeConnection cn)
        {
            var tvaMain = XmlSerialization.Deserialize<TvaMain>(data, true);
            if ((tvaMain == null) ||
                (tvaMain.ProgramDescription == null) ||
                (tvaMain.ProgramDescription.LocationTable == null) ||
                (tvaMain.ProgramDescription.LocationTable.Schedule == null))
            {
                Console.WriteLine("Empty or invalid data!");
                // there's nothing to process
                return;
            } // if
            var tvaSchedule = tvaMain.ProgramDescription.LocationTable.Schedule;

            var epgService = EpgService.FromItem(tvaSchedule);
            epgService.GetDatabaseId(cn);

            Log(">> Service {0} (db {1}): {2} events (t{3})", epgService.ServiceId, epgService.ServiceDatabaseId, epgService.Events.Length, Thread.CurrentThread.ManagedThreadId);

            // TODO: Provide fallback mechanism for deleting events pertaining to this service
            if (epgService.Events.Length == 0) return;

            var startTime = epgService.Events[0].StartTime;
            var today = new DateTime(startTime.Year, startTime.Month, startTime.Day, 0, 0, 0).ToUniversalTime();
            var tomorrow = today + new TimeSpan(1, 0, 0, 0);
            EpgDbSerialization.DeleteEvents(cn, epgService.ServiceDatabaseId, today, tomorrow);

            epgService.Save(cn);
        } // ProcessFile

        static void Log(string text)
        {
            lock (DbFile)
            {
                Console.WriteLine(text);
            } // lock
        } // Log

        static void Log(string text, params object[] args)
        {
            Log(string.Format(text, args));
        } // Log

        static void UpdateDbStatus(int status)
        {
            using (var cmd = new SqlCeCommand())
            {
                cmd.CommandText = "INSERT INTO [Status] ([Status], [Timestamp]) VALUES (?, ?)";
                cmd.Parameters.Add("@Status", System.Data.SqlDbType.Int).Value = status;
                cmd.Parameters.Add("@Timestamp", System.Data.SqlDbType.DateTime).Value = DateTime.UtcNow;
                DbServices.Execute(DbFile, cmd);
            } // using cmd
        } // UpdateDbStatus

    } // class Program
} // namespace
