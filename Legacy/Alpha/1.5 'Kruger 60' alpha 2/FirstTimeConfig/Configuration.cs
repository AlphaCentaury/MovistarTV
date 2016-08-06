// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Project.DvbIpTv.UiServices.Configuration;
using Project.DvbIpTv.UiServices.Configuration.Schema2014.Config;
using Project.DvbIpTv.UiServices.Discovery.BroadcastList;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace Project.DvbIpTv.Tools.FirstTimeConfig
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
                    TvViewer = new TvViewerConfig()
                    {
                        DefaultPlayer = "VLC",
                        Players = new PlayerConfig[]
                        {
                            new PlayerConfig()
                            {
                                Name = "VLC",
                                Path = vlcPath,
                                Arguments = new string[]
                                {
                                    "{param:Channel.Url}",
                                    ":meta-title={param:Channel.Name}"
                                } // Arguments
                            } // PlayerConfig
                        }, // Players
                    }, // TvViewer
                    Record = new RecordConfig()
                    {
                        SaveLocations = new RecordSaveLocation[]
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
                        TaskSchedulerFolders = new RecordTaskSchedulerFolder[]
                        {
                            new RecordTaskSchedulerFolder()
                            {
                                Name = Properties.Texts.TaskSchedulerFolderDvbIpTv,
                                Path = "\\DVB-IPTV"
                            } // RecordTaskSchedulerFolder
                        }, // TaskSchedulerFolders
                        Recorders = new RecorderConfig[]
                        {
                            new RecorderConfig()
                            {
                                Name = "VLC",
                                Path = vlcPath,
                                Arguments = new string[]
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

                var config = AppUiConfiguration.CreateForUserConfig(user);
                config.RegisterConfiguration(new UiBroadcastListSettingsConfigurationRegistration(), null, true);
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
    } // class Configuration
} // namespace
