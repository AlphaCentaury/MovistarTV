// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using IpTviewr.Common;
using IpTviewr.UiServices.Configuration;
using IpTviewr.UiServices.Configuration.Schema2014.Config;
using IpTviewr.UiServices.Configuration.Settings.TvPlayers;
using IpTviewr.UiServices.Discovery;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;
using IpTviewr.Native;
using Microsoft.SqlServer.MessageBox;

namespace IpTviewr.Core.IpTvProvider
{
    public static class ExternalTvPlayer
    {
        private static string[] LaunchParamKeys;

        public static void ShowTvChannel(Form owner, UiBroadcastService service, bool defaultPlayer = true)
        {
            TvPlayer player;

            if (service == null) return;
            if (service.IsHidden) return;

            if (service.IsInactive)
            {
                var box = new ExceptionMessageBox()
                {
                    Caption = owner.Text,
                    Text = string.Format(Properties.Texts.ShowTvChannelInactiveService, service.DisplayName),
                    Beep = true,
                    Symbol = ExceptionMessageBoxSymbol.Question,
                    Buttons = ExceptionMessageBoxButtons.YesNo,
                    DefaultButton = ExceptionMessageBoxDefaultButton.Button2,
                };
                if (box.Show(owner) != System.Windows.Forms.DialogResult.Yes) return;
            } // if

            var tvPlayerSettings = TvPlayersSettingsRegistration.Settings;
            if (defaultPlayer)
            {
                player = tvPlayerSettings.GetDefaultPlayer();
            }
            else
            {
                using (var dialog = new SelectTvPlayerDialog())
                {
                    if (dialog.ShowDialog(owner) != DialogResult.OK)
                    {
                        return;
                    } // if

                    player = dialog.SelectedPlayer;
                } // using
            } // if-else

            ExternalTvPlayer.Launch(player, service, !tvPlayerSettings.DirectLaunch);
        } // ShowTvChannel

        public static void Launch(TvPlayer player, UiBroadcastService service, bool throughShortcut)
        {
            if (!File.Exists(player.Path))
            {
                var ex = new FileNotFoundException();
                throw new FileNotFoundException(ex.Message + "\r\n" + player.Path);
            } // if

            if (LaunchParamKeys == null)
            {
                LaunchParamKeys = new string[]
                {
                    "Channel.Url",
                    "Channel.Name",
                    "Channel.Description",
                    "Channel.Icon.Path",
                };
            } // if

            var paramValues = new string[]
            {
                service.LocationUrl,
                service.DisplayName,
                service.DisplayDescription,
                service.Logo.GetLogoIconPath(),
            };

            var parameters = ArgumentsManager.CreateParameters(LaunchParamKeys, paramValues, false);
            var arguments = ArgumentsManager.ExpandArguments(player.Arguments, parameters, TvPlayer.ParameterOpenBrace, TvPlayer.ParameterCloseBrace, StringComparison.CurrentCultureIgnoreCase);
            var launchArguments = ArgumentsManager.JoinArguments(arguments);

            if (throughShortcut)
            {
                LaunchShortcut(player, service, launchArguments);
            }
            else
            {
                LaunchProcess(player, launchArguments);
            } // if-else
        } // Launch

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        private static void LaunchShortcut(TvPlayer player, UiBroadcastService service, string arguments)
        {
            var shortcutPath = Path.Combine(AppUiConfiguration.Current.Folders.Cache, service.FullServiceName) + ".lnk";

            // delete exising shortcut
            if (File.Exists(shortcutPath))
            {
                File.SetAttributes(shortcutPath, FileAttributes.Normal);
                File.Delete(shortcutPath);
            } // if

            var shortcut = new ShellLink();
            shortcut.TargetPath = player.Path;
            shortcut.Arguments = arguments;
            shortcut.Description = string.Format(Properties.Texts.ExternalPlayerShortcutDescription, player.Name, service.DisplayName);
            shortcut.IconLocation = service.Logo.GetLogoIconPath();
            shortcutPath = shortcut.CreateShortcut(shortcutPath);

            var start = new ProcessStartInfo()
            {
                UseShellExecute = true,
                FileName = shortcutPath,
                ErrorDialog = true,
            };
            using (var process = Process.Start(start))
            {
                // no op
            } // using process
        } // LaunchShortcut

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        private static void LaunchProcess(TvPlayer player, string arguments)
        {
            var start = new ProcessStartInfo()
            {
                UseShellExecute = false,
                ErrorDialog = true,
                FileName = player.Path,
                Arguments = arguments,
            };
            using (var process = Process.Start(start))
            {
                // no op
            } // using process
        } // LaunchProcess
    } // ExternalTvPlayer
} // namespace
