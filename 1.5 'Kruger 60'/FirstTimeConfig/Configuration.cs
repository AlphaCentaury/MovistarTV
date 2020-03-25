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

using IpTviewr.Core;
using IpTviewr.UiServices.Configuration;
using IpTviewr.UiServices.Configuration.Schema2014.Config;
using IpTviewr.UiServices.Configuration.Settings.Network;
using IpTviewr.UiServices.Configuration.Settings.TvPlayers;
using IpTviewr.UiServices.Discovery.BroadcastList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Mime;
using System.Security.AccessControl;
using System.Windows.Forms;
using IpTviewr.Common.Telemetry;
using IpTviewr.Tools.FirstTimeConfig.Properties;
using IpTviewr.UiServices.Configuration.Properties;
using IpTviewr.UiServices.Configuration.Settings;
using Microsoft.Win32;

namespace IpTviewr.Tools.FirstTimeConfig
{
    internal class Configuration
    {
        public static bool Create(string vlcPath, bool vlcIsX86OnX64, string rootSaveLocation, TelemetryConfiguration telemetry, EpgConfig epg, bool sdPriority, string xmlConfigPath, out string message)
        {
            try
            {
                Settings.Default.Telemetry = telemetry;
                Settings.Default.Save();
                JsonSettingsProvider.Close(Settings.Default);

                var user = new UserConfig()
                {
                    PreferredLanguages = Texts.DvbIpTv_PreferredLanguages,
                    Record = new RecordConfig()
                    {
                        SaveLocations = new[]
                        {
                            new RecordSaveLocation()
                            {
                                Path = rootSaveLocation
                            }, // RecordSaveLocation
                            new RecordSaveLocation()
                            {
                                Name = Texts.SaveLocationSeriesName,
                                Path = Path.Combine(rootSaveLocation, Texts.SaveLocationSeriesFolder)
                            }, // RecordSaveLocation
                            new RecordSaveLocation()
                            {
                                Name = Texts.SaveLocationMoviesName,
                                Path = Path.Combine(rootSaveLocation, Texts.SaveLocationMoviesFolder)
                            } // RecordSaveLocation
                        }, // SaveLocations
                        TaskSchedulerFolders = new[]
                        {
                            new RecordTaskSchedulerFolder()
                            {
                                Name = Texts.TaskSchedulerFolderDvbIpTv,
                                Path = "\\IPTViewr"
                            } // RecordTaskSchedulerFolder
                        }, // TaskSchedulerFolders
                        Recorders = new[]
                        {
                            new RecorderConfig()
                            {
                                Name = "VLC",
                                Path = vlcPath,
                                Arguments = new[]
                                {
                                    "--intf=dummy",
                                    "--dummy-quiet",
                                    "--demux=dump",
                                    "--demuxdump-file={param:OutputFile}",
                                    "--meta-title={param:Description.Name}",
                                    "{param:Channel.Url}",
                                    ":run-time={param:Duration.TotalSeconds}",
                                    "vlc://quit",
                                } // Arguments
                            } // RecorderConfig
                        } // Recorders
                    }, // Record
                    Epg = epg,
                    ChannelNumberStandardDefinitionPriority = sdPriority,
                };

                foreach (var location in user.Record.SaveLocations)
                {
                    Directory.CreateDirectory(location.Path);
                } // foreach

                var tvPlayers = GetTvPlayers(vlcPath, vlcIsX86OnX64);
                var movistarPlusIpTvProviderSettings = new IpTvProviderSettings();

                var config = AppConfig.CreateForUserConfig(user);
                config.RegisterConfiguration(new UiBroadcastListSettingsRegistration(), null, true);
                config.RegisterConfiguration(new TvPlayersSettingsRegistration(), tvPlayers);
                config.RegisterConfiguration(new NetworkSettingsRegistration(), null, true);
                config.RegisterConfiguration(new IpTvProviderSettingsRegistration(), movistarPlusIpTvProviderSettings);

                message = CreateRegistryEntries();
                if (message != null) return false;

                config.Save(xmlConfigPath);
                message = Texts.ConfigurationCreateOk;

                return true;
            }
            catch (Exception ex)
            {
                message = string.Format(Texts.ConfigurationCreateException, ex);
                return false;
            } // try-catch
        } // Create

        private static TvPlayersSettings GetTvPlayers(string vlcPath, bool isX86OnX64)
        {
            const string vlcGuid = "{C12055FC-315A-47C4-B9CC-48D2E6ECD8FA}";
            const string vlcX86Guid = "{364A5B10-6895-438F-8FBE-405E0D816721}";
            const string vlcSameGuid = "{4154BC96-5FE0-45C2-9895-083C4FB4C8CE}";
            const string vlcX86SameGuid = "{076D734D-D8F2-4CBB-8AAE-B26237D99BCD}";
            const string mpcGuid = "{8FFA2EE6-8823-40B1-B20F-F962389D4B07}";
            const string mpcX86Guid = "{289A17CA-2A6D-4F3C-96DA-9BC91DCB4489}";

            var players = new List<TvPlayer>(3);

            // VLC
            var player = new TvPlayer
            {
                Name = isX86OnX64? Texts.GetTvPlayersVlcX86 : Texts.GetTvPlayersVlc,
                Id = new Guid(isX86OnX64? vlcX86Guid : vlcGuid),
                Path = vlcPath,
                Arguments = new[]
                {
                    "{param:Channel.Url}",
                    ":meta-title={param:Channel.Name}",
                } // Arguments
            }; // TvPlayer
            players.Add(player);

            // VLC (new window)
            player = new TvPlayer()
            {
                Name = isX86OnX64 ? Texts.GetTvPlayersVlcSameWindowX86 : Texts.GetTvPlayersVlcSameWindow,
                Id = new Guid(isX86OnX64? vlcX86SameGuid : vlcSameGuid),
                Path = vlcPath,
                Arguments = new[]
                {
                    "{param:Channel.Url}",
                    ":meta-title={param:Channel.Name}",
                    "--one-instance",
                    "--no-playlist-enqueue",
                } // Arguments
            }; // TvPlayer
            players.Add(player);

            // locate K-Lite Codec Pack Media Player Classic
            Installation.GetProgramFilesFolder(out var programFilesFolder, out var programFilesFolder86);

            var path = Path.Combine(programFilesFolder, Resources.MpcDefaultLocation);
            if (File.Exists(path))
            {
                AddMpcPlayer(path, false, mpcGuid);
            } // if

            if (programFilesFolder86 != null)
            {
                path = Path.Combine(programFilesFolder86, Resources.MpcDefaultLocation);
                if (File.Exists(path))
                {
                    AddMpcPlayer(path, true, mpcX86Guid);
                } // if
            } // if

            var tvPlayers = new TvPlayersSettings()
            {
                DirectLaunch = false,
                DefaultPlayerId = (players.Count > 0)? players[0].Id : Guid.Empty,
                Players = players.ToArray()
            };

            return tvPlayers;

            void AddMpcPlayer(string playerPath, bool isX86OnX64Player, string guid)
            {
                player = new TvPlayer()
                {
                    Name = isX86OnX64Player ? Texts.GetTvPlayersMpcX86 : Texts.GetTvPlayersMpc,
                    Id = new Guid(guid),
                    Path = playerPath,
                    Arguments = new[]
                    {
                        "{param:Channel.Url}",
                        "/play",
                    } // Arguments
                }; // TvPlayer

                players.Add(player);
            } // local AddMpcPlayer
        } // GetTvPlayers

        private static string CreateRegistryEntries()
        {
            using var keyCurrentUser = Registry.CurrentUser;
            var fullKeyPath = AppConfig.RegistryKey_Root;
            var rootKey = string.Format(fullKeyPath, Application.ProductVersion);
            using var root = keyCurrentUser.OpenSubKey(rootKey, true);
            if (root == null) return string.Format(AppConfig.RegistryMissingKey, rootKey);

            root.SetValue(AppConfig.RegistryValue_FirstTimeConfig, "1");
            return null;
        } // CreateRegistryEntries
    } // class Configuration
} // namespace
