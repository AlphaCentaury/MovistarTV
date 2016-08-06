using Project.DvbIpTv.Common;
using Project.DvbIpTv.UiServices.Configuration;
using Project.DvbIpTv.UiServices.Configuration.Schema2014.Config;
using Project.DvbIpTv.UiServices.Discovery;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;

namespace Project.DvbIpTv.ChannelList
{
    public static class ExternalPlayer
    {
        private static string[] LaunchParamKeys;
        public const string OpenBrace = "{param:";
        public const string CloseBrace = "}";

        public static void Launch(PlayerConfig player, UiBroadcastService service, bool throughShortcut)
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
            };
            } // if

            var paramValues = new string[]
            {
                service.LocationUrl,
                service.DisplayName,
                service.DisplayDescription,
            };

            var parameters = ArgumentsManager.CreateParameters(LaunchParamKeys, paramValues, false);
            var arguments = ArgumentsManager.ExpandArguments(player.Arguments, parameters, OpenBrace, CloseBrace, StringComparison.CurrentCultureIgnoreCase);

            if (throughShortcut)
            {
                LaunchShortcut(player, service, arguments);
            }
            else
            {
                LaunchProcess(player, arguments);
            } // if-else
        } // Launch

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        private static void LaunchShortcut(PlayerConfig player, UiBroadcastService service, string[] arguments)
        {
            var shortcutPath = Path.Combine(AppUiConfiguration.Current.Folders.Cache, service.FullServiceName);
            var shortcut = new ShellLink.ShellLink();
            shortcut.TargetPath = player.Path;
            shortcut.Arguments = ArgumentsManager.JoinArguments(arguments);
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
        private static void LaunchProcess(PlayerConfig player, string[] arguments)
        {
            var start = new ProcessStartInfo()
            {
                UseShellExecute = false,
                ErrorDialog = true,
                FileName = player.Path,
                Arguments = ArgumentsManager.JoinArguments(arguments),
            };
            using (var process = Process.Start(start))
            {
                // no op
            } // using process
        } // LaunchProcess
    } // ExternalPlayer
} // namespace
