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

using System;
using System.Diagnostics;
using System.IO;
using System.Security.Permissions;
using System.Windows.Forms;
using IpTviewr.Common;
using IpTviewr.Core.Properties;
using IpTviewr.Native;
using IpTviewr.UiServices.Configuration;
using IpTviewr.UiServices.Configuration.Settings.TvPlayers;
using IpTviewr.UiServices.Discovery;
using Microsoft.SqlServer.MessageBox;

namespace IpTviewr.Core
{
    public static class ExternalTvPlayer
    {
        private static string[] _launchParamKeys;

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
                    Text = string.Format(Texts.ShowTvChannelInactiveService, service.DisplayName),
                    Beep = true,
                    Symbol = ExceptionMessageBoxSymbol.Question,
                    Buttons = ExceptionMessageBoxButtons.YesNo,
                    DefaultButton = ExceptionMessageBoxDefaultButton.Button2,
                };
                if (box.Show(owner) != DialogResult.Yes) return;
            } // if

            var tvPlayerSettings = TvPlayersSettingsRegistration.Settings;
            if (defaultPlayer)
            {
                player = tvPlayerSettings.GetDefaultPlayer();
                if (player == null)
                {
                    new ExceptionMessageBox
                    {
                        Caption = owner.Text,
                        Text = Texts.NoDefaultTvPlayer,
                        Symbol = ExceptionMessageBoxSymbol.Warning,
                        Buttons = ExceptionMessageBoxButtons.OK,
                    }.Show(owner);
                    return;
                } // if
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

            try
            {
                Launch(player, service, !tvPlayerSettings.DirectLaunch);
            }
            catch (Exception e)
            {
                var box = new ExceptionMessageBox()
                {
                    Caption = owner.Text,
                    Text = string.Format(Texts.ExternalPlayerLaunchError, player.Name, service.DisplayName),
                    InnerException = e,
                    Beep = true,
                    Symbol = ExceptionMessageBoxSymbol.Warning,
                    Buttons = ExceptionMessageBoxButtons.OK,
                };
                
                box.Show(owner);
            } // try-catch
        } // ShowTvChannel

        public static void Launch(TvPlayer player, UiBroadcastService service, bool throughShortcut)
        {
            if (!File.Exists(player.Path))
            {
                var ex = new FileNotFoundException();
                throw new FileNotFoundException($"{ex.Message}\r\n{player.Path}");
            } // if

            if (_launchParamKeys == null)
            {
                _launchParamKeys = new[]
                {
                    "Channel.Url",
                    "Channel.Name",
                    "Channel.Description",
                    "Channel.Icon.Path",
                };
            } // if

            var paramValues = new[]
            {
                service.LocationUrl,
                service.DisplayName,
                service.DisplayDescription,
                throughShortcut? service.Logo.GetLogoIconPath() : null,
            };

            var parameters = ArgumentsManager.CreateParameters(_launchParamKeys, paramValues, false);
            var arguments = ArgumentsManager.ExpandArguments(player.Arguments, parameters, TvPlayer.ParameterOpenBrace, TvPlayer.ParameterCloseBrace, StringComparison.CurrentCultureIgnoreCase);
            var launchArguments = ArgumentsManager.JoinArguments(arguments);

            if (throughShortcut)
            {
                LaunchShortcut(player, service, launchArguments, paramValues[3]);
            }
            else
            {
                LaunchProcess(player, launchArguments);
            } // if-else
        } // Launch

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        private static void LaunchShortcut(TvPlayer player, UiBroadcastService service, string arguments, string iconLocation)
        {
            var shortcutPath = Path.Combine(AppConfig.Current.Folders.Cache, service.FullServiceName) + ".lnk";

            // delete existing shortcut
            if (File.Exists(shortcutPath))
            {
                File.SetAttributes(shortcutPath, FileAttributes.Normal);
                File.Delete(shortcutPath);
            } // if

            var shortcut = new ShellLink
            {
                TargetPath = player.Path,
                Arguments = arguments,
                Description = string.Format(Texts.ExternalPlayerShortcutDescription, player.Name, service.DisplayName),
                IconLocation = iconLocation
            };
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
