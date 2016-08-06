// Copyright (C) 2014, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Project.DvbIpTv.Tools.FirstTimeConfig
{
    internal class Installation
    {
        public static bool IsNetInstalled(out string message)
        {
            RegistryKey key;
            object value;
            int intValue;

            string[] keys = new string[]
            {
                "SOFTWARE",
                "Microsoft",
                "NET Framework Setup",
                "NDP",
                "v3.5"
            };

            try
            {
                key = Microsoft.Win32.Registry.LocalMachine;
                foreach (var keyName in keys)
                {
                    var newKey = key.OpenSubKey(keyName);
                    key.Close();
                    key = newKey;
                    if (key == null) { message = Properties.Texts.IsNetInstalledNotInstalled; return false; }
                } // foreach

                value = key.GetValue("Install");
                if (value == null) { message = string.Format(Properties.Texts.IsNetInstalledKeyValueNotFound, "Install"); return false; }
                intValue = 0;
                if ((!int.TryParse(value.ToString(), out intValue)) || (intValue < 0))
                {
                    message = string.Format(Properties.Texts.IsNetInstalledKeyValueLessThan, "Install", "1"); return false;
                } // if

                value = key.GetValue("SP");
                if (value == null) { message = string.Format(Properties.Texts.IsNetInstalledKeyValueNotFound, "SP"); return false; }
                intValue = 0;
                if ((!int.TryParse(value.ToString(), out intValue)) || (intValue < 0))
                {
                    message = string.Format(Properties.Texts.IsNetInstalledKeyValueLessThan, "SP", "1"); return false;
                } // if

                message = Properties.Texts.IsNetInstalledOk;
                return true;
            }
            catch (Exception ex)
            {
                message = string.Format(Properties.Texts.IsNetInstalledException, ex.ToString());
                return false;
            } // try-catch
        } // IsNetInstalled

        public static bool IsEmbInstalled(out string message)
        {
            try
            {
#pragma warning disable 618 // CS0618: 'member' is obsolete (Assembly.LoadWithPartialName)
                // Assembly.Load() is not an alternative to LoadWithPartialName in this case
                // We don't know the assembly version. In fact, where trying to QUERY the version of the installed assembly
                var assembly = Assembly.LoadWithPartialName("Microsoft.ExceptionMessageBox");
                if (assembly == null) { message = Properties.Texts.IsEmbInstalledNotInstalled; return false; }
#pragma warning restore 618

                var targetVersion = new Version(9, 0, 242, 0);
                var version = assembly.GetName().Version;

                if ((version.Major < targetVersion.Major) || (version.Minor < targetVersion.Minor) ||
                    (version.Build < targetVersion.Build) || (version.Revision < version.Revision))
                {
                    message = string.Format(Properties.Texts.IsEmbInstalledWrongVersion, targetVersion, version);
                    return false;
                } // if

                message = string.Format(Properties.Texts.IsEmbInstalledOk, version);
                return true;
            }
            catch (Exception ex)
            {
                message = string.Format(Properties.Texts.IsEmbInstalledException, ex.ToString());
                return false;
            } // try-catch
        }  // IsEmbNotInstalled

        public static bool IsVlcInstalled(out string message, string path, string testVideoPath)
        {
            if (!File.Exists(path))
            {
                message = string.Format("VLC executable file not found at '{0}'.", path);
                return false;
            } // if

            try
            {
                using (var process = new Process())
                {
                    process.StartInfo = new ProcessStartInfo()
                    {
                        FileName = path,
                        Arguments = (testVideoPath != null) ? string.Format("\"{0}\"", testVideoPath) : null,
                        UseShellExecute = false,
                    };
                    process.Start();
                } // using process
            }
            catch (Exception ex)
            {
                message = string.Format(Properties.Texts.IsVlcInstalledException, ex.ToString());
                return false;
            } // try-catch

            message = Properties.Texts.IsVlcInstalledOk;
            return true;
        } // IsVlcInstalled

        public static string GetProgramFilesx86Folder()
        {
            var folder = KnownFolders.GetKnownFolder(KnownFolders.System.ProgramFiles_x86, KnownFolders.Flags.None);
            return System.Environment.ExpandEnvironmentVariables(folder);
        } // GetProgramFilesx86Folder

        public static string GetCurrentUserVideosFolder()
        {
            var folder = KnownFolders.GetKnownFolder(KnownFolders.CurrentUser.Videos, KnownFolders.Flags.None);
            return System.Environment.ExpandEnvironmentVariables(folder);
        } // GetCurrentUserVideosFolder

        public static string GetTestMedia()
        {
            string folder;
            int step;

            step = 1;
            folder = null;
            while (true)
            {
                try
                {
                    switch (step)
                    {
                        case 1: folder = KnownFolders.GetKnownFolder(KnownFolders.Common.SampleVideos, KnownFolders.Flags.None); break;
                        case 2: folder = KnownFolders.GetKnownFolder(KnownFolders.CurrentUser.Videos, KnownFolders.Flags.None); break;
                        case 3: folder = KnownFolders.GetKnownFolder(KnownFolders.Common.SampleMusic, KnownFolders.Flags.None); break;
                        case 4: folder = KnownFolders.GetKnownFolder(KnownFolders.CurrentUser.Music, KnownFolders.Flags.None); break;
                        default:
                            return null;
                    } // switch

                    var files = Directory.GetFiles(folder);
                    var q = from file in files
                            let ext = Path.GetExtension(file).ToLowerInvariant()
                            where ((ext == ".wmv") || (ext == ".mp4") || (ext == ".mkv") || (ext == ".avi") ||
                                (ext == ".wma") || (ext == ".mp3") || (ext == ".aac") || (ext == ".wav"))
                            select file;
                    var media = q.FirstOrDefault();
                    if (media != null) return media;
                }
                catch
                {
                    // ignore
                } // try-catch
                step++;
            } // while
        } // GetTestMedia
    } // class Installation
} // namespace
