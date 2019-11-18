// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using IpTviewr.UiServices.Configuration;
using IpTviewr.UiServices.Configuration.Schema2014.Config;
using IpTviewr.UiServices.Configuration.Settings.Network;
using IpTviewr.UiServices.Configuration.Settings.TvPlayers;
using IpTviewr.UiServices.Discovery.BroadcastList;
using System;
using System.Collections.Generic;
using System.IO;
using IpTViewr.Core;
using IpTviewr.IpTvServices.MovistarPlus;

namespace IpTviewr.Tools.FirstTimeConfig
{
    internal class Configuration
    {
        public static bool Create(string vlcPath, string rootSaveLocation, TelemetryConfiguration analytics, EpgConfig epg, bool sdPriority, string xmlConfigPath, out string message)
        {
            UserConfig user;

            try
            {
                user = new UserConfig()
                {
                    Telemetry = analytics,
                    PreferredLanguages = Properties.Texts.DvbIpTv_PreferredLanguages,
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
                                Name = Properties.Texts.SaveLocationSeriesName,
                                Path = Path.Combine(rootSaveLocation, Properties.Texts.SaveLocationSeriesFolder)
                            }, // RecordSaveLocation
                            new RecordSaveLocation()
                            {
                                Name = Properties.Texts.SaveLocationMoviesName,
                                Path = Path.Combine(rootSaveLocation, Properties.Texts.SaveLocationMoviesFolder)
                            } // RecordSaveLocation
                        }, // SaveLocations
                        TaskSchedulerFolders = new[]
                        {
                            new RecordTaskSchedulerFolder()
                            {
                                Name = Properties.Texts.TaskSchedulerFolderDvbIpTv,
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
                }; // user

                foreach (var location in user.Record.SaveLocations)
                {
                    Directory.CreateDirectory(location.Path);
                } // foreach

                var tvPlayers = GetTvPlayers(vlcPath);
                var movistarPlusIpTvProviderSettings = new IpTvProviderSettings()
                {
                    ProviderClass = typeof(ServiceMovistarPlus).AssemblyQualifiedName
                };

                var config = AppUiConfiguration.CreateForUserConfig(user);
                config.RegisterConfiguration(new UiBroadcastListSettingsRegistration(), null, true);
                config.RegisterConfiguration(new TvPlayersSettingsRegistration(), tvPlayers);
                config.RegisterConfiguration(new NetworkSettingsRegistration(), null, true);
                config.RegisterConfiguration(new IpTvProviderSettingsRegistration(), movistarPlusIpTvProviderSettings);

                config.Save(xmlConfigPath);
                message = Properties.Texts.ConfigurationCreateOk;

                return true;
            }
            catch (Exception ex)
            {
                message = string.Format(Properties.Texts.ConfigurationCreateException, ex.ToString());
                return false;
            } // try-catch
        } // Create

        private static TvPlayersSettings GetTvPlayers(string vlcPath)
        {
            List<TvPlayer> players;
            TvPlayer player;
            string path;

            players = new List<TvPlayer>(3);
            var programFilesFolder86 = Installation.GetProgramFilesAnyFolder();

            // VLC
            player = new TvPlayer()
            {
                Name = "VLC",
                Id = new Guid("{C12055FC-315A-47C4-B9CC-48D2E6ECD8FA}"),
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
                Name = Properties.Texts.GetTvPlayersVlcSameWindow,
                Id = new Guid("{4154BC96-5FE0-45C2-9895-083C4FB4C8CE}"),
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
            path = Path.Combine(programFilesFolder86, "K-Lite Codec Pack\\Media Player Classic\\mpc-hc.exe");
            if (File.Exists(path))
            {
                player = new TvPlayer()
                {
                    Name = "K-Lite Media Player Classic",
                    Id = new Guid("{8FFA2EE6-8823-40B1-B20F-F962389D4B07}"),
                    Path = path,
                    Arguments = new[]
                    {
                        "{param:Channel.Url}",
                        "/play",
                    } // Arguments
                }; // TvPlayer

                players.Add(player);
            } // if

            var tvPlayers = new TvPlayersSettings()
            {
                DirectLaunch = false,
                DefaultPlayerId = new Guid("{C12055FC-315A-47C4-B9CC-48D2E6ECD8FA}"),
                Players = players.ToArray()
            };

            return tvPlayers;
        } // GetTvPlayers
    } // class Configuration
} // namespace
